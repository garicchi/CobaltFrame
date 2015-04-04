using CobaltFrame.Animation;
using CobaltFrame.Context;
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

namespace CobaltFrame.Object
{
    public abstract class DrawableGameObject:DrawableObject
    {
        protected Game _game;
        protected SpriteBatch _spriteBatch;
        protected Position2D _position;
        private PositionUpdateMode _positionUpdateMode;
        protected Position2DTimeAnimation _attachedAnimation;
        protected PositionUpdateMode PositionUpdateMode
        {
            get { return _positionUpdateMode; }
        }

        public Position2D Position
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

        
        public DrawableGameObject(GameContext context,Position2D position)
            : base(context)
        {
            this._game = context.Game;
            
            this._position = position;
            this._positionUpdateMode = PositionUpdateMode.Manual;
            this._rotation = 0.0f;
            this._origin = Vector2.Zero;
            
            this._drawColor = Color.White;
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

        public override void Update(Core.Context.IFrameContext context)
        {
            base.Update(context);
            switch (this._positionUpdateMode)
            {
                case PositionUpdateMode.AttachedAnimation:
                    this.Position.SetPosition(this._attachedAnimation.CurrentValue.GetPosition());
                    
                    break;
            }
        }
        public override void Draw(Core.Context.IFrameContext context)
        {
            
            base.Draw(context);
        }

        public void AttachAnimation(Position2DTimeAnimation animation)
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

        
    }
}
