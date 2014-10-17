using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoguePoleDisplay.Routines;
using RoguePoleDisplay.Models;
using System.Data.Entity;

namespace RoguePoleDisplay.Repositories
{
    public class MemoryContext : DbContext
    {
        public MemoryContext()
            : base("RoguePoleDisplayDB")
        { }
        public DbSet<Player> Players { get; set; }
        public DbSet<Interaction> Interactions { get; set; }
        public DbSet<RoutineResult> RoutineResults { get; set; }
    }

    public class Memory : IDisposable
    {
        private static Random _rand = new Random();
        private MemoryContext _session;
        static Memory() 
        {
            CurrentState = ConsciousnessState.AsleepState;
            LastState = ConsciousnessState.AsleepState;
        }

        public static Dictionary<string, Player> Players = new Dictionary<string, Player>()
        {
            {"Digit", new Player { Name = "Digit", QuestionLine1 = "What's the", QuestionLine2 = "secret number?", Answer=5 }},
            {"Foom", new Player { Name = "Foom", QuestionLine1 = "What's the velocity", QuestionLine2="of a laden swallow?", Answer=5 }},
            {"Sparky", new Player { Name = "Sparky", QuestionLine1 = "How many stairs must", QuestionLine2="a man fall down?", Answer=5 }},
            {"Mr Pibb", new Player { Name = "Mr Pibb", QuestionLine1 = "What time do we", QuestionLine2="let the dogs out?", Answer=0 }},
            {"Pillfred", new Player { Name = "Pillfred", QuestionLine1 = "What is", QuestionLine2="your quest?", Answer=0 }},
            {"Swiper", new Player { Name = "Swiper", QuestionLine1="How many fingers", QuestionLine2="are you holding up?", Answer=0 }},
            {"Father Time", new Player { Name = "Father Time", QuestionLine1="How old are you?", QuestionLine2="", Answer=0 }},
            {"West Wind", new Player { Name = "West Wind", QuestionLine1="How much wind", QuestionLine2="has broken today?", Answer=0 }},
            {"Doc Nasty", new Player { Name = "Doc Nasty", QuestionLine1="How many sick bugs", QuestionLine2="have you healed?", Answer=0 }},
        };

        public MemoryContext DBContext
        {
            get
            {
                if (null == _session)
                {
                    _session = new MemoryContext();
                    if (_session.Players.Count() == 0)
                    {
                        foreach (var pair in Memory.Players)
                        {
                            _session.Players.Add(pair.Value);
                        }
                        _session.SaveChanges();
                    }
                }
                return _session;
            }
        }

        public static ConsciousnessState CurrentState;
        public static ConsciousnessState LastState;

        private static long _playerID = -1;
        public static bool PlayerLoggedIn()
        {
            return _playerID >= 0;
        }

        private static List<Interaction> _shortTerm = new List<Interaction>();
        private static Dictionary<MemoryKey, Interaction> _textMemory = new Dictionary<MemoryKey, Interaction>();

        public void SaveChanges()
        {
            _session.SaveChanges();
        }

        public Player GetCurrentPlayer()
        {
            if (!PlayerLoggedIn()) return null;
            return DBContext.Players.Where(p => p.id == _playerID).Single();
        }

        public void SetCurrentPlayer(Player player)
        {
            _playerID = player.id;
        }

        public Interaction LastInteraction()
        {
            if (_shortTerm.Count > 1)
                return _shortTerm[_shortTerm.Count - 1];
            else 
            {
                var query = from i in DBContext.Interactions
                            orderby i.timestamp descending
                            select i;
                if (query.Count() > 0)
                {
                    return query.First();
                }
                else
                {
                    return new Interaction();
                }
            }
        }

        public int PlayerInteractionsSince(DateTime timestamp)
        {
            int shortTermTotal = _shortTerm.Count(i => i.timestamp >= timestamp);
            int longTermTotal = 0;
            var query = from i in DBContext.Interactions
                        where i.timestamp >= timestamp
                        select i;
            // have to do this craziness because linq to entity doesn't like enums or something
            List<Interaction> interactions = query.ToList();
            longTermTotal = interactions.Where(i => i.playerAnswer != Interaction.Answer.DidNotAnswer).Count();
            return shortTermTotal + longTermTotal;
        }

        private static MemoryKey MakeMemoryKey(Player player, string line1, string line2)
        {
            return new MemoryKey() { player = player, line1 = line1, line2 = line2 };
        }

        private static MemoryKey MakeMemoryKey(Interaction i)
        {
            return new MemoryKey() { player = i.player, line1 = i.displayText };
        }

        public void AddToMemory(Interaction interaction, bool longTerm = false)
        {
            interaction.timestamp = DateTime.Now;
            _shortTerm.Add(interaction);
            if (longTerm)
            {
                DBContext.Interactions.Add(interaction);
            }

            _textMemory[MakeMemoryKey(interaction)] = interaction;
        }

        internal void AddToMemory(RoutineResult routineResult)
        {
            routineResult.Timestamp = DateTime.Now;
            DBContext.RoutineResults.Add(routineResult);
        }

        public bool LastRoutineAbandoned() 
        {
            var query = from r in DBContext.RoutineResults
                        orderby r.Timestamp descending
                        select r;
            return query.First().FinalState == RoutineFinalState.Abandoned;
        }

        public bool LastRoutineCompleted()
        { 
            return !LastRoutineAbandoned(); 
        }

        public int RoutinesCompletedSinceStateBegan()
        {
            var query = from r in DBContext.RoutineResults
                        where r.Timestamp >= CurrentState.StateEntered && r.FinalState != RoutineFinalState.Abandoned
                        orderby r.Timestamp descending
                        select r;
            return query.Count();
        }

        public Interaction Remember(string line1, string line2, bool longTerm = false)
        {
            Interaction remembered;
            if (longTerm)
            {
                string text = (line1 + " " + line2).Trim();
                var query = from i in DBContext.Interactions
                            where i.displayText == text 
                            orderby i.timestamp descending
                            select i;
                List<Interaction> interactions = query.ToList();
                if (null == GetCurrentPlayer())
                {
                    remembered = interactions.Where(i => i.player == null).FirstOrDefault();
                }
                else
                {
                    remembered = interactions.Where(i => i.player != null).Where(i => i.player.Equals(GetCurrentPlayer())).FirstOrDefault();
                }
                if (null != remembered) return remembered;
            }

            MemoryKey key = MakeMemoryKey(GetCurrentPlayer(), line1, line2);
            if (!_textMemory.ContainsKey(key)) return null;
            return _textMemory[key];
        }

        public List<Player> GetKnownPlayers()
        {
            var query = from p in DBContext.Players
                        where p.Answer != null && p.Answer != 0
                        select p;
            return query.ToList();
        }

        public Player GetPlayerWithNoAnswer()
        {
            var query = from p in DBContext.Players
                        where p.Answer == null || p.Answer == 0
                        select p;
            if (query.Count() == 0) return null;
            return query.First();
        }

        public bool cancelChanges = false;
        public void Dispose()
        {
            if (_session != null && !cancelChanges)
            {
                _session.SaveChanges();
                _session.Dispose();
                _session = null;
            }
        }
        
    }
}
