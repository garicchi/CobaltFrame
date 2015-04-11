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
        public MainScreenManager(GameContext context,GameScreen firstScreen,object param,Vector2 defaultResolution,ScaleMode screenScaleMode)
            : base(context, firstScreen, param, defaultResolution, screenScaleMode)
        {

        }
    }
}
