using CobaltFrame.Mono.Animation;
using CobaltFrame.Mono.Context;
using CobaltFrame.Core.Animation;
using CobaltFrame.Core.Object;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CobaltFrame.Mono.Input;
using CobaltFrame.Mono.Position;

namespace CobaltFrame.Mono.Object
{
    public abstract class DrawableGameObject:DrawableObject,IGameObject
    {
        protected Game _game;
        protected SpriteBatch _spriteBatch;
        protected IBox2 _box;


        public IBox2 Box
        {
            get { return _box; }
            set { _box = value; }
        }


        protected float _rotation;

        public float Rotation
        {
            get { return _rotation; }
            set { _rotation = value; }
        }

        protected Vector2 _origin;

        public Vector2 Origin
        {
            get { return _origin; }
            set { _origin = value; }
        }

        protected Color _drawColor;

        public Color DrawColor
        {
            get { return _drawColor; }
            set { _drawColor = value; }
        }

        private GameInputCollection _inputs;

        public GameInputCollection Inputs
        {
            get { return _inputs; }
            set { _inputs = value; }
        }
        
        public DrawableGameObject(IBox2 box)
            : base()
        {
            this._game = GameContext.Game;
            
            this._box = box;
            this._rotation = 0.0f;
            this._origin = Vector2.Zero;
            
            this._drawColor = Color.White;

            this._inputs = new GameInputCollection();
        }

        public override void Load()
        {
            base.Load();

			this._spriteBatch = new SpriteBatch(this._game.GraphicsDevice);

        }

        public override void Unload()
        {
            base.Unload();
            this._inputs.UnregisterAllInput();
            this._spriteBatch.Dispose();
        }

        public override void Update(Core.Context.IFrameContext context)
        {
            this._inputs.Update();
            base.Update(context);

        }
        public override void Draw(Core.Context.IFrameContext context)
        {
            
            base.Draw(context);
        }

    }
}
