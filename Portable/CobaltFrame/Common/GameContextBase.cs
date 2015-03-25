using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Common
{
    public abstract class GameContextBase
    {
        private Game _game;

        public Game Game { get { return this._game; } }
        public GameContextBase(Game game)
        {
            this._game = game;
        }
    }
}
