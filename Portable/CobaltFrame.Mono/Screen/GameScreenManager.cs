using CobaltFrame.Context;
using CobaltFrame.Core.Screen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Screen
{
    public class GameScreenManager:ScreenManager
    {
        public GameScreenManager(GameContext context,GameScreen firstScreen,object param)
            : base(context,firstScreen,param)
        {

        }
    }
}
