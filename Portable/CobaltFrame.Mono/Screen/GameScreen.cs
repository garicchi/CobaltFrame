using CobaltFrame.Context;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Screen
{
    public class GameScreen:CobaltFrame.Core.Screen.Screen
    {
        protected Game _game;
        public GameScreen(GameContext context)
            : base(context)
        {
            this._game = context.Game;
        }

        public override void Draw(Core.Context.IFrameContext context)
        {
            this._game.GraphicsDevice.Clear(Color.Cyan);
            
            base.Draw(context);
        }
    }
}
