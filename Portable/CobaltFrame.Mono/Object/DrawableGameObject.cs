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

namespace CobaltFrame.Mono.Object
{
    public abstract class DrawableGameObject:DrawableObject
    {
        protected Game _game;
        protected SpriteBatch _spriteBatch;
        protected Box2 _position;
        private PositionUpdateMode _positionUpdateMode;
        protected Box2TimeAnimation _attachedAnimation;
        protected PositionUpdateMode PositionUpdateMode
        {
            get { return _positionUpdateMode; }
        }

        public Box2 Position
        {
            get { return _position; }
            set { _position = value; }
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

        
        public DrawableGameObject(GameContext context,Box2 position)
            : base(context)
        {
            this._game = context.Game;
            
            this._position = position;
            this._positionUpdateMode = PositionUpdateMode.Manual;
            this._rotation = 0.0f;
            this._origin = Vector2.Zero;
            
            this._drawColor = Color.White;
        }

        public override void Load()
        {
            base.Load();
            this._spriteBatch = new SpriteBatch(_game.GraphicsDevice);
        }

        public override void Unload()
        {
            base.Unload();
            this._spriteBatch.Dispose();
        }

        public override void Update(Core.Context.IFrameContext context)
        {
            base.Update(context);
            switch (this._positionUpdateMode)
            {
                case PositionUpdateMode.AttachedAnimation:
                    this.Position.SetRect(this._attachedAnimation.CurrentValue.GetRect());
                    
                    break;
            }
        }
        public override void Draw(Core.Context.IFrameContext context)
        {
            
            base.Draw(context);
        }

        public void AttachAnimation(Box2TimeAnimation animation)
        {
            this._attachedAnimation = animation;
            this._positionUpdateMode=PositionUpdateMode.AttachedAnimation;
            this.AddObject(this._attachedAnimation);
        }

        public void DetachAnimation()
        {
            this._attachedAnimation = null;
            this._positionUpdateMode = PositionUpdateMode.Manual;
            this.RemoveObject(this._attachedAnimation);
        }

        public void MovePosition(int up = 0, int down = 0, int right = 0, int left = 0)
        {
            this.Position.SetLocation(new Vector2(this.Position.GetLocation().X+right-left, this.Position.GetLocation().Y +down-up));
        }
    }
}
