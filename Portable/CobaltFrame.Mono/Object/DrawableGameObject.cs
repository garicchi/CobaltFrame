using CobaltFrame.Mono.Animation;
using CobaltFrame.Mono.Context;
using CobaltFrame.Core.Animation;
using CobaltFrame.Core.Object;
using CobaltFrame.Position;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CobaltFrame.Mono.Input;

namespace CobaltFrame.Mono.Object
{
    public abstract class DrawableGameObject:DrawableObject,IGameObject
    {
        protected Game _game;
        protected SpriteBatch _spriteBatch;
        protected Box2 _box;


        public Box2 Box
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

        public GameInputCollection Inputs { get; set; }
        
        public DrawableGameObject(GameContext context,Box2 box)
            : base(context)
        {
            this._game = context.Game;
            
            this._box = box;
            this._rotation = 0.0f;
            this._origin = Vector2.Zero;
            
            this._drawColor = Color.White;

            this.Inputs = new GameInputCollection();
        }

        public override void Load()
        {
            base.Load();
            this._spriteBatch = new SpriteBatch(_game.GraphicsDevice);
        }

        public override void Unload()
        {
            base.Unload();
            this.Inputs.UnregisterAllInput();
            this._spriteBatch.Dispose();
        }

        public override void Update(Core.Context.IFrameContext context)
        {
            this.Inputs.Update();
            base.Update(context);

        }
        public override void Draw(Core.Context.IFrameContext context)
        {
            
            base.Draw(context);
        }


        public void MovePosition(int up = 0, int down = 0, int right = 0, int left = 0)
        {
            this._box.SetLocation(new Vector2(this._box.GetLocation().X+right-left, this._box.GetLocation().Y +down-up));
        }


        
    }
}
