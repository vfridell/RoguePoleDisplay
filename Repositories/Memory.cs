using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoguePoleDisplay.Routines;
using RoguePoleDisplay.Models;
using NHibernate;
using System.Runtime.InteropServices;

namespace RoguePoleDisplay.Repositories
{
    public class Memory : IDisposable
    {
        private static Random _rand = new Random();
        private static TimeSpan _shortTermMemoryLimit = new TimeSpan(0, 2, 0); // 90 minutes
        //private static TimeSpan _shortTermMemoryLimit = new TimeSpan(1, 30, 0); // 90 minutes
        static Memory() 
        {
            CurrentState = ConsciousnessState.AsleepState;
            LastState = ConsciousnessState.AsleepState;
        }

        //public static Dictionary<string, Player> Players = new Dictionary<string, Player>()
        //{
        //    {"Digit", new Player { Name = "Digit", QuestionLine1 = "What's the", QuestionLine2 = "secret number?", Answer=5 }},
        //    {"Foom", new Player { Name = "Foom", QuestionLine1 = "What's the velocity", QuestionLine2="of a laden swallow?", Answer=5 }},
        //    {"Sparky", new Player { Name = "Sparky", QuestionLine1 = "How many stairs must", QuestionLine2="a man fall down?", Answer=5 }},
        //    {"Mr Pibb", new Player { Name = "Mr Pibb", QuestionLine1 = "What time do we", QuestionLine2="let the dogs out?", Answer=0 }},
        //    {"Pillfred", new Player { Name = "Pillfred", QuestionLine1 = "What is", QuestionLine2="your quest?", Answer=0 }},
        //    {"Swiper", new Player { Name = "Swiper", QuestionLine1="How many fingers", QuestionLine2="are you holding up?", Answer=0 }},
        //    {"Father Time", new Player { Name = "Father Time", QuestionLine1="How old are you?", QuestionLine2="", Answer=0 }},
        //    {"West Wind", new Player { Name = "West Wind", QuestionLine1="How much wind", QuestionLine2="has broken today?", Answer=0 }},
        //    {"Doc Nasty", new Player { Name = "Doc Nasty", QuestionLine1="How many sick bugs", QuestionLine2="have you healed?", Answer=0 }},
        //};

        public static int RoutinesCompleted { get; set; }
        public static ConsciousnessState CurrentState;
        public static ConsciousnessState LastState;

        private static long _playerID = -1;
        public static bool PlayerLoggedIn()
        {
            return _playerID >= 0;
        }

        private static List<Interaction> _shortTerm = new List<Interaction>();
        private static Dictionary<MemoryKey, Interaction> _textMemory = new Dictionary<MemoryKey, Interaction>();
        private static Interaction _lastInteraction;
        private static Dictionary<MemoryKey, RoutineResult> _routineMemory = new Dictionary<MemoryKey, RoutineResult>();
        private static RoutineResult _lastRoutineResult;

        /**************************
         * END static
         ************************** */

        protected readonly ISession _session;
        protected ITransaction _trans;

        public Memory()
        {
            _session = SessionFactory.Get().OpenSession();
            _session.FlushMode = FlushMode.Commit;
            _trans = _session.BeginTransaction();
        }

        public Player GetCurrentPlayer()
        {
            if (!PlayerLoggedIn()) return null;
            ISession session = SessionFactory.Get().OpenSession();
            Player currentPlayer = session.Load<Player>(_playerID);
            session.Close();
            return currentPlayer;
        }

        public void SetCurrentPlayer(Player player)
        {
            _playerID = player.Id;
        }

        public Interaction LastInteraction() => _lastInteraction ?? new Interaction();

        public int PlayerInteractionsSince(DateTime timestamp)
        {
            int shortTermTotal = _shortTerm.Count(i => i.Timestamp >= timestamp);
            return shortTermTotal;
        }

        private static MemoryKey MakeMemoryKey(Player player, string line1, string line2)
        {
            return new MemoryKey() { player = player, line1 = line1, line2 = line2 };
        }

        private static MemoryKey MakeMemoryKey(Interaction i)
        {
            return new MemoryKey() { player = i.Player, line1 = i.DisplayText };
        }

        private static MemoryKey MakeMemoryKey(RoutineResult r)
        {
            return MakeMemoryKey(r.FinalInteraction);
        }

        public void AddToMemory(Interaction interaction, bool longTerm = false)
        {
            interaction.Timestamp = DateTime.Now;
            _shortTerm.Add(interaction);
            _lastInteraction = interaction;
            if (longTerm)
            {
                _session.Save(interaction);
            }
            _textMemory[MakeMemoryKey(interaction)] = interaction;

            // time to forget
            _shortTerm.RemoveAll(i => i.Timestamp < interaction.Timestamp - _shortTermMemoryLimit);
            List<MemoryKey> keysToRemove = _textMemory.Values
                                                    .Where(t => t.Timestamp < interaction.Timestamp - _shortTermMemoryLimit)
                                                    .Select(i => MakeMemoryKey(i))
                                                    .ToList();
            foreach (MemoryKey k in keysToRemove) _textMemory.Remove(k);
        }

        internal void AddToMemory(RoutineResult routineResult)
        {
            routineResult.Timestamp = DateTime.Now;
            _lastRoutineResult = routineResult;
            if (routineResult.FinalState != RoutineFinalState.Abandoned) RoutinesCompleted++;
        }

        public bool LastRoutineAbandoned() 
        {
            return (_lastRoutineResult?.FinalState ?? RoutineFinalState.Abandoned) == RoutineFinalState.Abandoned;
        }

        public bool LastRoutineCompleted()
        { 
            return !LastRoutineAbandoned(); 
        }

        public int RoutinesCompletedSinceStateBegan() => RoutinesCompleted;

        public Interaction Remember(string line1, string line2, bool longTerm = false)
        {
            Interaction remembered;
            Player player = GetCurrentPlayer();
            if (longTerm)
            {
                string text = (line1 + " " + line2).Trim();
                IList<Interaction> interactions;
                if (player != null)
                {
                    interactions = _session.CreateQuery("select i from Interaction i join i.Player p where i.DisplayText = :displayText and p.Id = :playerId order by Timestamp desc")
                            .SetString("displayText", text)
                            .SetInt64("playerId", player.Id)
                            .List<Interaction>();
                }
                else
                {
                    interactions = _session.CreateQuery("select i from Interaction i where i.DisplayText = :displayText and p.Id is null or p.Id=0 order by Timestamp desc")
                            .SetString("displayText", text)
                            .List<Interaction>();

                }
                remembered = interactions.FirstOrDefault();
                if (null != remembered) return remembered;
            }

            MemoryKey key = MakeMemoryKey(player, line1, line2);
            if (!_textMemory.ContainsKey(key)) return null;
            return _textMemory[key];
        }

        public IList<Player> GetKnownPlayers()
        {
            IList<Player> players = _session.CreateQuery("from Player p where p.Answer is not null and p.Answer != 0 order by p.Id")
                                    .List<Player>();
            return players;
        }

        public Player GetPlayerWithNoAnswer()
        {
            Player player = _session.CreateQuery("from Player p where p.Answer is null or p.Answer = 0 order by p.Id")
                                    .List<Player>()
                                    .FirstOrDefault();
            return player;
        }

        public void Dispose()
        {
            try
            {
                if (_session != null)
                {
                    if (Marshal.GetExceptionCode() == 0)
                    {
                        if (_session.Transaction.IsActive)
                        {
                            _session.Transaction.Commit();
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _session?.Dispose();
            }
        }

    }

}
