using CobaltFrame.Mono.Context;
using CobaltFrame.Core.Screen;
using CobaltFrame.Mono.Screen;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorizontalShootingGame.Portable.Screen
{
    public class MainScreenManager:GameScreenManager
    {
        public MainScreenManager(Game game,GameScreen firstScreen,object param,Vector2 defaultResolution,ScaleMode screenScaleMode)
            : base(game,firstScreen, param, defaultResolution, screenScaleMode)
        {

        }
    }
}
