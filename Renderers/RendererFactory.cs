using System;
using System.Collections.Generic;
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
                _renderer = new ConsoleRenderer();
                //_renderer = new PoleDisplayRenderer();
                _renderer.Init();
            }
            return _renderer;
        }
    }
}
