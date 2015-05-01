using CobaltFrame.Core.Context;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Mono.Context
{
    public static class GameContext
    {
        public static Game Game { get; set; }
		public static GraphicsDeviceManager  GraphicsManager{ get; set; }
		public static Vector2 DefaultResolution{get;set;}
    }
}
