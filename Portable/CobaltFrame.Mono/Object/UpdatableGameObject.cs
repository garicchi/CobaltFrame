using CobaltFrame.Mono.Context;
using CobaltFrame.Core.Object;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CobaltFrame.Mono.Input;

namespace CobaltFrame.Mono.Object
{
    public class UpdatableGameObject:UpdatableObject,IGameObject
    {
        protected Game _game;
        public UpdatableGameObject(GameContext context)
            : base(context)
        {
            this._game = context.Game;
            this.Inputs = new GameInputCollection();
        }

        public override void Unload()
        {
            this.Inputs.UnregisterAllInput();
            base.Unload();
        }
        public GameInputCollection Inputs { get; set; }

    }
}
