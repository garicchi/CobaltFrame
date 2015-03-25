using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Common
{
    public abstract class FrameContextBase
    {
        Game _game;

        public Game Game { get { return this._game; } }
        public FrameContextBase(Game game)
        {
            this._game = game;
        }
    }
}
