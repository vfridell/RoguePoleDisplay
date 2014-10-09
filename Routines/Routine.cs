using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoguePoleDisplay.Repositories;
using System.Threading;
using RoguePoleDisplay.Models;

namespace RoguePoleDisplay.Routines
{
    public enum RoutineType { Asleep, Dawn, Awake, Dusk, Login, CreateLogin };

    public abstract class Routine 
    {
        public RoutineType routineType {get { return RoutineFactory.GetRoutineTypeEnum(this.GetType());}}
        
        public Routine() { }

        protected abstract RoutineResult RunConsciousRoutine();

        public virtual void Init() { }

        public RoutineResult Run()
        {
            RoutineResult result = RunConsciousRoutine();
            return result;
        }

        protected virtual RoutineResult MakeRoutineResult(RoutineFinalState finalState, Interaction finalInteraction)
        {
            RoutineResult result = new RoutineResult()
            {
                FinalState = finalState,
                FinalInteraction = finalInteraction,
                RoutineType = this.GetType()
            };
            Memory.GetInstance().AddToMemory(result);
            return result;
        }

        protected virtual RoutineResult MakeRoutineResult(Interaction finalInteraction)
        {
            RoutineResult result = new RoutineResult()
            {
                FinalState = RoutineFinalState.Abandoned,
                FinalInteraction = finalInteraction,
                RoutineType = this.GetType()
            };
            if (finalInteraction.playerAnswer != Interaction.Answer.DidNotAnswer)
            {
                result.FinalState = finalInteraction.success ? RoutineFinalState.Success : RoutineFinalState.Failure;
            }
            Memory.GetInstance().AddToMemory(result);
            return result;
        }
    }
}
