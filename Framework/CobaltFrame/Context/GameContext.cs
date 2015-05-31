using CobaltFrame.Screen;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Context
{
    public static class GameContext
    {
        public static Game Game { get; set; }
		public static GraphicsDeviceManager  GraphicsManager{ get; set; }
		public static Point DefaultResolution{get;set;}

        public static Rectangle ClientBounds { get; set; }

        public static ScaleMode ScreenScaleMode { get; set; }
    }
}
