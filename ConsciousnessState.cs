using RoguePoleDisplay.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RoguePoleDisplay.Routines;

namespace RoguePoleDisplay
{
    public abstract class ConsciousnessState
    {
        public static readonly ConsciousnessState AwakeState = new Awake();
        public static readonly ConsciousnessState AsleepState = new Asleep();
        public static readonly ConsciousnessState HalfState = new Half();
        public static readonly ConsciousnessState FriendlyState = new Friendly();

        public abstract bool CheckForStateChange();
        public abstract RoutineType GetNextRoutineType();

        protected DateTime _stateEntered = DateTime.Now;
        public DateTime StateEntered { get { return _stateEntered; } }

        public static string StateChangeReason { get; protected set; }

        protected virtual void ChangeState(ConsciousnessState newState)
        {
            Memory.CurrentState = newState;
            Memory.LastState = this;
            newState.EnterState();
        }

        protected virtual void EnterState()
        {
            _stateEntered = DateTime.Now;
        }
    }

    public class Asleep : ConsciousnessState
    {
        public override bool CheckForStateChange()
        {
            int interactionsNeededToBeginHalf;
            TimeSpan elapsed = DateTime.Now - _stateEntered;
            if(elapsed.Minutes >= 30)
            {
                interactionsNeededToBeginHalf = 3;
            }
            else if(elapsed.Minutes >= 15)
            {
                interactionsNeededToBeginHalf = 2;
            }
            else
            {
                interactionsNeededToBeginHalf = 1;
            }

            using (var memory = new Memory())
            if (memory.PlayerInteractionsSince(_stateEntered) >= interactionsNeededToBeginHalf)
            {
                StateChangeReason = string.Format("Number of interactions ({0}) since {1} meets the threshold value of {2}", 
                                                   memory.PlayerInteractionsSince(_stateEntered),
                                                   _stateEntered.ToShortTimeString(),
                                                   interactionsNeededToBeginHalf);
                ChangeState(HalfState);
                return true;
            }
            else
            {
                return false;
            }
        }

        public override RoutineType GetNextRoutineType()
        {
            return RoutineType.Asleep;
        }
}

    public class Awake : ConsciousnessState
    {
        public override bool CheckForStateChange()
        {
            using (var memory = new Memory())
            {
                if (!Memory.PlayerLoggedIn())
                {
                    Random random = new Random();
                    int num = random.Next(1, 5);
                    if (num > 0)
                    {
                        StateChangeReason = string.Format("Player not logged in.  Random chance."); 
                        ChangeState(FriendlyState);
                        return true;
                    }
                }

                double threshold = new Random().NextDouble() * 10;
                if (memory.LastRoutineAbandoned() ||
                   memory.RoutinesCompletedSinceStateBegan() > threshold )
                {
                    StateChangeReason = string.Format("Routine abandoned ({0}) or routines completed ({1}) greater than threshold value {2}", 
                                   memory.LastRoutineAbandoned(),
                                   memory.RoutinesCompletedSinceStateBegan(),
                                   threshold);
                    ChangeState(HalfState);
                    return true;
                }

                return false;
            }
        }

        public override RoutineType GetNextRoutineType()
        {
            return RoutineType.Awake;
        }
    }

    public class Friendly : Awake
    {
        public override bool CheckForStateChange()
        {
            if (Memory.PlayerLoggedIn())
            {
                StateChangeReason = "Player logged in";
                ChangeState(AwakeState);
                return true;
            }
            return base.CheckForStateChange();
        }

        public override RoutineType GetNextRoutineType()
        {
            return RoutineType.Login;
        }
    }

    public class Half : ConsciousnessState
    {
        public override bool CheckForStateChange()
        {
            using (var memory = new Memory())
            {
                if (Memory.LastState is Asleep &&
                    memory.LastRoutineCompleted())
                {
                    StateChangeReason = string.Format("Was asleep and last routine was completed"); 
                    ChangeState(AwakeState);
                    return true;
                }

                if (memory.LastRoutineAbandoned())
                {
                    StateChangeReason = string.Format("Last routine was abandoned"); 
                    ChangeState(AsleepState);
                    return true;
                }

                return false;
            }
        }

        public override RoutineType GetNextRoutineType()
        {
            using (var memory = new Memory())
            if (Memory.LastState is Awake) 
            {
                return RoutineType.Dusk;
            }
            else if(Memory.LastState is Asleep)
            {
                return RoutineType.Dawn;
            }
            else
            {
                throw new Exception(string.Format("Last Consciousness State not recognized: {0}", Memory.LastState.ToString()));
            }
        }
    }
}
