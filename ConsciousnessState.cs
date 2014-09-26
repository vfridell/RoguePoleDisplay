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

        protected DateTime _stateEntered;
        public DateTime StateEntered { get { return _stateEntered; } }

        protected virtual void ChangeState(ConsciousnessState newState)
        {
            Memory memory = Memory.GetInstance();
            memory.CurrentState = newState;
            memory.LastState = this;
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

            Memory memory = Memory.GetInstance();
            if (memory.InteractionsSince(_stateEntered) >= interactionsNeededToBeginHalf)
            {
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
            Memory memory = Memory.GetInstance();
            if (!memory.PlayerLoggedIn())
            {
                Random random = new Random();
                int num = random.Next(1,5);
                if(num == 4)
                {
                    ChangeState(FriendlyState);
                    return true;
                }
            }

            if (memory.LastRoutineAbandoned ||
               (memory.RoutinesCompletedSinceStateBegan * 0.25) >= new Random().NextDouble())
            {
                ChangeState(HalfState);
                return true;
            }

            return false;
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
            Memory memory = Memory.GetInstance();
            if (memory.PlayerLoggedIn())
            {
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
            Memory memory = Memory.GetInstance();
            if(memory.LastState is Asleep && 
                memory.LastRoutineCompleted)
            {
                ChangeState(AwakeState);
                return true;
            }

            if(memory.LastRoutineAbandoned)
            {
                ChangeState(AsleepState);
                return true;
            }

            return false;
        }

        public override RoutineType GetNextRoutineType()
        {
            Memory memory = Memory.GetInstance();
            if (memory.LastState is Awake) 
            {
                return RoutineType.Dusk;
            }
            else if(memory.LastState is Asleep)
            {
                return RoutineType.Dawn;
            }
            else
            {
                throw new Exception(string.Format("Last Consciousness State not recognized: {0}", memory.LastState.ToString()));
            }
        }
    }
}
