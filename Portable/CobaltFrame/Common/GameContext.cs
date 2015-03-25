using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Common
{
    public class GameContext
    {
        private Game _game;

        public GameContext(Game game)
        {
            this._game = game;
        }
    }
}
