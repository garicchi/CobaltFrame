using CobaltFrame.Context;
using CobaltFrame.Core.Object;
using CobaltFrame.Position;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Object
{
    public abstract class DrawableGameObject:DrawableObject
    {
        protected Game _game;
        protected SpriteBatch _spriteBatch;
        protected Position2D _position;

        protected Position2D Position
        {
            get { return _position; }
            set { _position = value; }
        }
        public DrawableGameObject(GameContext context,Position2D position)
            : base(context)
        {
            this._game = context.Game;
            
            this._position = position;
        }

        public override void LoadObject()
        {
            base.LoadObject();
            this._spriteBatch = new SpriteBatch(_game.GraphicsDevice);
        }

        public override void UnloadObject()
        {
            base.UnloadObject();
            this._spriteBatch.Dispose();
        }

        
        public override void Draw(Core.Context.IFrameContext context)
        {
            
            base.Draw(context);
        }
    }
}
