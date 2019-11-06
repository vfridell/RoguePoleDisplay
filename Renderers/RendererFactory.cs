using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoguePoleDisplay.Renderers
{
    public class RendererFactory
    {
        private static IScreenRenderer _renderer;

        public static IScreenRenderer GetPreferredRenderer()
        {
            if (null == _renderer)
            {
                string rendererTypeName = ConfigurationManager.AppSettings["RendererType"];
                if(rendererTypeName == null) throw new Exception($"RendererType not specified in AppConfig");
                Type rendererType = typeof(RendererFactory).Assembly.GetType(rendererTypeName);
                if (rendererType?.GetInterface("IScreenRenderer") == null) throw new Exception($"RendererType specified '{rendererTypeName}' does not implement IScreenRenderer");
                _renderer = (IScreenRenderer)Activator.CreateInstance(rendererType);
                _renderer.Init();
            }
            return _renderer;
        }
    }
}
