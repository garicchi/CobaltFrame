using CobaltFrame.Animation;
using CobaltFrame.Context;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Transition
{
    public class FadeColorTransition:ScreenTransition
    {
        private Texture2D _texture;

        public Texture2D Texture
        {
            get { return _texture; }
            set { _texture = value; }
        }

        private InstantTimeAnimation<int> _animation;

        public InstantTimeAnimation<int> Animation
        {
            get { return _animation; }
            set { _animation = value; }
        }
        public Color TextureColor { get; private set; }

        protected SpriteBatch _spriteBatch;

        public FadeColorTransition(Color color,int alphaFrom,int alphaTo,TimeSpan duration)
        {
            this.TextureColor = color;
            this._animation = new InstantTimeAnimation<int>(alphaFrom, alphaTo, duration, (from, to, progress) =>
            {
                if (to > from)
                {
                    return (int)((to - from) * progress);
                }
                else
                {
                    return (int)((from - to) *(1.0 - progress));
                }

                
            });
            this._layerDepth = 0.0f;
            this.AddChild(this._animation);
        }

        public override void Load()
        {
            base.Load();
            int width = GameContext.DefaultResolution.X;
            int height = GameContext.DefaultResolution.Y;
            this.Texture = new Texture2D(this._game.GraphicsDevice,width,height);
            this.Texture.SetData<Color>(
                Enumerable.Repeat<Color>(TextureColor, width * height).ToArray()
                );

            this._spriteBatch = new SpriteBatch(this._game.GraphicsDevice);
        }

        public override void Unload()
        {
            base.Unload();
            this.RemoveChild(this._animation);
            Texture.Dispose();
        }

        public override void Update(FrameContext context)
        {
            base.Update(context);
            
        }

        public override void Draw(FrameContext context)
        {
            
            base.Draw(context);
            this._spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, context.GetScreenTrans());
            this._spriteBatch.Draw(this.Texture, new Rectangle(0, 0, GameContext.DefaultResolution.X,GameContext.DefaultResolution.Y),
                new Color(TextureColor.R, TextureColor.G, TextureColor.B, Animation.CurrentValue));
            this._spriteBatch.End();
            
        }

        public override void Start()
        {
            this._animation.OnCompleted += () => this.Translate();
            this._animation.Start();
        }
    }
}
