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
        private Game _game;

        public Game Game
        {
            get { return _game; }
        }
        public GameContext(Game game)
        {
            this._game = game;
        }
    }
}
