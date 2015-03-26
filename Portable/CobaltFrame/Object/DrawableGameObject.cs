using CobaltFrame.Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Object
{
    public abstract class DrawableGameObject:DrawableObjectUpdater
    {

        protected SpriteBatch _spriteBatch;

        protected Game _game;

        public DrawableGameObject(ObjectContext objectContext)
            :base()
        {
            this._game = objectContext.Game;

        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void LoadObject()
        {
            base.LoadObject();
            this._spriteBatch = new SpriteBatch(this._game.GraphicsDevice);
        }

        public override void UnloadObject()
        {
            base.UnloadObject();
        }


        public override void Update(ObjectFrameContext frameContext)
        {
            base.Update(frameContext);
        }

        public override void Draw(ObjectFrameContext frameContext)
        {
            base.Draw(frameContext);
        }

    }
}
