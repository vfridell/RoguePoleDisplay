using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoguePoleDisplay.Input
{
    public class InputFactory
    {
        private static IGetInput _input;

        public static IGetInput GetPreferredInput()
        {
            if (null == _input)
            {
                string inputTypeName = ConfigurationManager.AppSettings["InputType"];
                if (inputTypeName == null) throw new Exception($"InputType not specified in AppConfig");
                Type inputType = typeof(InputFactory).Assembly.GetType(inputTypeName);
                if (inputType?.GetInterface("IGetInput") == null) throw new Exception($"InputType specified '{inputTypeName}' does not implement IGetInput");
                _input = (IGetInput)Activator.CreateInstance(inputType);
                _input.Init();
            }
            return _input;
        }
    }
}
