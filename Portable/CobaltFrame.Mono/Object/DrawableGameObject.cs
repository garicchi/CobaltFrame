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
        protected Position2DAnimation _attachedAnimation;
        protected PositionUpdateMode PositionUpdateMode
        {
            get { return _positionUpdateMode; }
        }

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
            this._positionUpdateMode = PositionUpdateMode.Manual;
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

        public void AttachAnimation(Position2DAnimation animation)
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
