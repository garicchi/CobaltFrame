using CobaltFrame.Context;
using CobaltFrame.Core.Object;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Object
{
    public class DrawableGameObject:DrawableObject
    {
        protected Game _game;
        protected SpriteBatch _spriteBatch;
        public DrawableGameObject(GameContext context)
            : base(context)
        {
            this._game = context.Game;
            this._spriteBatch = new SpriteBatch(_game.GraphicsDevice);
            
        }

        public override void LoadObject()
        {
            base.LoadObject();
            
        }

        public override void Draw(Core.Context.IFrameContext context)
        {
            
            base.Draw(context);
        }
    }
}
