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
        public UpdatableGameObject()
            : base()
        {
            this._game = GameContext.Game;
            this._inputs = new GameInputCollection();
        }

        public override void Unload()
        {
            this._inputs.UnregisterAllInput();
            base.Unload();
        }

        public override void Update(Core.Context.IFrameContext context)
        {
            this._inputs.Update();
            base.Update(context);
        }
        private GameInputCollection _inputs;

        public GameInputCollection Inputs
        {
            get { return _inputs; }
            set { _inputs = value; }
        }
    }
}
