using CobaltFrame.Mono.Context;
using CobaltFrame.Core.Object;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Mono.Object
{
    public class UpdatableGameObject:UpdatableObject
    {
        protected Game _game;
        public UpdatableGameObject(GameContext context)
            : base(context)
        {
            this._game = context.Game;
        }
    }
}
