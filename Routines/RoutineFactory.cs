using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoguePoleDisplay.Repositories;

namespace RoguePoleDisplay.Routines
{
    public static class RoutineFactory
    {
        private static Dictionary<Type, RoutineTypeAttribute> _routinesTypes = new Dictionary<Type, RoutineTypeAttribute>();
        private static Random rnd = new Random();

        static RoutineFactory()
        {
            System.Reflection.Assembly currentAssembly = System.Reflection.Assembly.GetExecutingAssembly();
            foreach (Type type in currentAssembly.GetTypes())
            {
                List<RoutineTypeAttribute> attributes = type.GetCustomAttributes(typeof(RoutineTypeAttribute), false)
                                                            .Cast<RoutineTypeAttribute>()
                                                            .ToList<RoutineTypeAttribute>();
                if (attributes.Count > 0)
                {
                    _routinesTypes.Add(type, (RoutineTypeAttribute)attributes[0]);
                }
            }
        }

        public static RoutineType GetRoutineTypeEnum(Type routineType)
        {
            if (!_routinesTypes.ContainsKey(routineType))
            {
                throw new Exception(string.Format("Invalid type {0} given to GetRoutineTypeEnum", routineType.Name));
            }
            return _routinesTypes[routineType].routineType;
        }

        public static Routine GetRoutine(RoutineType routineTypeEnum)
        {
            IList<Type> routineTypes = _routinesTypes.Where<KeyValuePair<Type, RoutineTypeAttribute>>(t => t.Value.routineType == routineTypeEnum)
                                                           .Select(kvp => kvp.Key).ToList();
            if(routineTypes.Count() == 0) throw new Exception(string.Format("No routines of type {0} are defined!", routineTypeEnum.ToString()));

            if(routineTypes.Count() == 1) return CreateAndInitRoutine(routineTypes[0]);

            int index = rnd.Next(0, routineTypes.Count);
            Type routineType = routineTypes[index];
            return CreateAndInitRoutine(routineType);
        }

        public static Routine GetLoginRoutine()
        {
            Type routineType = _routinesTypes.First<KeyValuePair<Type, RoutineTypeAttribute>>(t => t.Value.routineType == RoutineType.Login && t.Value.order == 0).Key;
            return CreateAndInitRoutine(routineType);
        }

        public static Routine GetCreateLoginRoutine()
        {
            Type routineType = _routinesTypes.First<KeyValuePair<Type, RoutineTypeAttribute>>(t => t.Value.routineType == RoutineType.CreateLogin && t.Value.order == 0).Key;
            return CreateAndInitRoutine(routineType);
        }

        public static Routine CreateAndInitRoutine(Type routineType)
        {
            Routine newRoutine = (Routine)Activator.CreateInstance(routineType);
            newRoutine.Init();
            return newRoutine;
        }
    }
}
