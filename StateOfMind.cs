using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using RoguePoleDisplay.Routines;
using RoguePoleDisplay.Repositories;
using RoguePoleDisplay.Models;
using log4net;
using RoguePoleDisplay.Helpers;

namespace RoguePoleDisplay
{
    public class StateOfMind 
    {
        private ILog _log;
        private static object _stateChangeLock = new object();

        public StateOfMind()
        {
        }

        public void BecomeSelfAware()
        {
            _log = LogManager.GetLogger("RoguePoleDisplay");

            do
            {
                RoutineType routineType = Memory.CurrentState.GetNextRoutineType();
                Routine currentRoutine = RoutineFactory.GetRoutine(routineType);
                RoutineResult result = null;
                try
                {
                    _log.Debug($"Starting Routine: {currentRoutine.GetType().Name}");
                    result = currentRoutine.Run();
                    _log.Debug("Routine result was " + result.FinalState.ToString());
                }
                catch (Exception ex)
                {
                    _log.Error($"Exception in routine {currentRoutine.GetType().Name}");
                    LogHelper.LogAll(_log, ex);
                }

                try
                {
                    // do we need to create a new login for this user?
                    if (routineType == RoutineType.Login
                        && !Memory.PlayerLoggedIn()
                        && result.FinalState != RoutineFinalState.Abandoned)
                    {
                        _log.Info($"A new player wants to create a login...");
                        currentRoutine = RoutineFactory.GetCreateLoginRoutine();
                        _log.Debug($"Starting Routine: {currentRoutine.GetType().Name}");
                        result = currentRoutine.Run();
                        _log.Debug("Create login routine result was " + result.FinalState.ToString());
                    }
                }
                catch (Exception ex)
                {
                    _log.Error($"Exception in create login routine {currentRoutine.GetType().Name}");
                    LogHelper.LogAll(_log, ex);
                }

                try
                {
                    _log.Debug("Checking for state change");
                    if (Memory.CurrentState.CheckForStateChange())
                    {
                        _log.Debug($"state changed to {Memory.CurrentState.GetType().Name}");
                        _log.Debug($"Reason: {ConsciousnessState.StateChangeReason}");
                    }
                }
                catch (Exception ex)
                {
                    _log.Error($"Exception when checking for state change");
                    LogHelper.LogAll(_log, ex);
                }

            } while (true);
        }

    }
}
