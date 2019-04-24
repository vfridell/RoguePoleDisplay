using RoguePoleDisplay.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RoguePoleDisplay.Routines;
using RoguePoleDisplay.ConsciousnessStates;

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
            Memory.RoutinesCompleted = 0;
        }
    }
}
