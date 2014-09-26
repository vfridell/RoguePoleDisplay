using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoguePoleDisplay.Routines;
using RoguePoleDisplay.Models;

namespace RoguePoleDisplay.Repositories
{
    public class Memory
    {
        private Random _rand = new Random();
        private static Memory _instance;
        protected Memory() 
        {
            CurrentState = ConsciousnessState.AwakeState;
            LastState = ConsciousnessState.AwakeState;
        }

        public static Dictionary<string, Player> Players = new Dictionary<string, Player>()
        {
            {"Digit", new Player { Name = "Digit", Question = "What's the secret number?", Answer=5 }},
            {"Foom", new Player { Name = "Foom", Question = "What's the air speed velocity of a laden swallow?", Answer=5 }},
            {"Sparky", new Player { Name = "Sparky", Question = "How many stairs must a man fall down?", Answer=5 }},
            {"Mr Pibb", new Player { Name = "Mr Pibb", Question = "What time do we let the dogs out?", Answer=0 }},
            {"Pillfred", new Player { Name = "Pillfred", Question = "What is your quest?", Answer=0 }},
            {"Swiper", new Player { Name = "Swiper", Question="How many fingers are you holding up?", Answer=0 }},
            {"Father Time", new Player { Name = "Father Time", Question="How old are you?", Answer=0 }},
            {"West Wind", new Player { Name = "West Wind", Question="Which way is the wind blowing?", Answer=0 }},
            {"Doc Nasty", new Player { Name = "Doc Nasty", Question="How many sick bugs have you healed?", Answer=0 }},
        };

        public static Memory GetInstance()
        {
            if (null == _instance)
            {
                _instance = new Memory();
            }
            return _instance;
        }

        public ConsciousnessState CurrentState;
        public ConsciousnessState LastState;

        public Player CurrentPlayer { get; set; }
        public bool PlayerLoggedIn()
        {
            return null != CurrentPlayer;
        }

        private List<Interaction> _shortTerm = new List<Interaction>();
        private List<Interaction> _longTerm = new List<Interaction>();
        private Dictionary<MemoryKey, Interaction> _textMemory = new Dictionary<MemoryKey, Interaction>();
        public Interaction LastInteraction
        {
            get
            {
                if (_shortTerm.Count > 1)
                    return _shortTerm[_shortTerm.Count - 1];
                else if (_longTerm.Count > 1)
                    return _longTerm[_longTerm.Count - 1];
                else
                    return new Interaction();
            }
        }

        private List<RoutineResult> _routineResults = new List<RoutineResult>();

        public int InteractionsSince(DateTime timestamp)
        {
            return _shortTerm.Count(i => i.timestamp >= timestamp) + _longTerm.Count(i => i.timestamp >= timestamp);
        }

        private MemoryKey MakeMemoryKey(Player player, string text)
        {
            return new MemoryKey() { player = player, text = text };
        }

        private MemoryKey MakeMemoryKey(Interaction i)
        {
            return new MemoryKey() { player = i.player, text = i.displayText };
        }

        public void AddToMemory(Interaction interaction, bool longTerm = false)
        {
            interaction.timestamp = DateTime.Now;
            _shortTerm.Add(interaction);
            if (longTerm) _longTerm.Add(interaction);

            _textMemory[MakeMemoryKey(interaction)] = interaction;
        }

        internal void AddToMemory(RoutineResult routineResult)
        {
            routineResult.Timestamp = DateTime.Now;
            _routineResults.Add(routineResult);
        }

        public bool LastRoutineAbandoned { get { return _routineResults.Last().FinalState == RoutineFinalState.Abandoned; } }
        public bool LastRoutineCompleted { get { return !LastRoutineAbandoned; } }
        public int RoutinesCompletedSinceStateBegan { get { return _routineResults.Count(rr => rr.Timestamp >= CurrentState.StateEntered); } }

        public Interaction Remember(string text, Player player, bool longTerm = false)
        {
            Interaction remembered;
            if (longTerm)
            {
                remembered = _longTerm.FirstOrDefault(i => i.displayText == text);
                if (null != remembered) return remembered;
            }

            MemoryKey key = MakeMemoryKey(player, text);
            if (!_textMemory.ContainsKey(key)) return null;
            return _textMemory[key];
        }

        public List<Player> GetKnownPlayers()
        {
            return Players.Values.Where(p => p.Answer != 0).ToList();
        }

        public Player GetRandomPlayerWithNoAnswer()
        {
            if (GetKnownPlayers().Count == Players.Count)
                return null;

            Player player;
            do
            {
                player = Players.Values.FirstOrDefault(p => p.Answer == 0 && _rand.Next(0, 10) > 5);
            } while (null != player);
            return player;
        }
    }
}
