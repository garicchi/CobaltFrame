using CobaltFrame.Core.Context;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Context
{
    public class GameContext:IGameContext
    {
        protected Game _game;
        public GameContext(Game game)
        {
            this._game = game;
        }
    }
}
