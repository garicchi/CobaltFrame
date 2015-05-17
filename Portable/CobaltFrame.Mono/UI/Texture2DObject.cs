using CobaltFrame.Mono.Context;
using CobaltFrame.Mono.Object;
using CobaltFrame.Mono.Position;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Mono.UI
{
    public class Texture2DObject : UIObject
    {
        private string _texturePath;

        protected string TexturePath
        {
            get { return _texturePath; }
        }

        private Texture2D _texture;

        public Texture2D Texture
        {
            get { return _texture; }
        }

        private Vector2 _textureScale;

        private Vector2 TextureScale
        {
            get { return _textureScale; }
        }

        private bool _isRepeat;

        public bool IsRepeat
        {
            get { return _isRepeat; }
            set { _isRepeat = value; }
        }

        private Rectangle _sourceRect;

        public Rectangle SourceRect
        {
            get { return _sourceRect; }
            set { _sourceRect = value; }
        }

        public Texture2DObject(IBox2 position, string texturePath, bool isRepeat = false)
            : base(position)
        {
            this._texturePath = texturePath;
            this._isRepeat = isRepeat;
            this.Box.OnChanged += () =>
            {
                if (Texture != null)
                {
                    this._textureScale = new Vector2((float)this._box.GetRect().Width / (float)this._texture.Width, (float)this._box.GetRect().Height / (float)this._texture.Height);
                }
            };

        }

        public override void Load()
        {
            base.Load();
            this._texture = ContentContext.Load<Texture2D>(this._texturePath);
            this._origin = new Vector2(this._texture.Width / 2.0f, this._texture.Height / 2.0f);
            this._textureScale = new Vector2((float)this._box.GetRect().Width / (float)this._texture.Width, (float)this._box.GetRect().Height / (float)this._texture.Height);
            this.SourceRect = this._texture.Bounds;
            
        }
        

        public override void Unload()
        {
            base.Unload();

        }

        public override void Update(Core.Context.IFrameContext context)
        {
            
            base.Update(context);
        }

        public override void Draw(Core.Context.IFrameContext context)
        {
            base.Draw(context);

            this._spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, (context as FrameContext).ScreenTrans);
            if (this._isRepeat)
            {
                int texW = (int)this._texture.Bounds.Width;
                int texH = (int)this._texture.Bounds.Height;

                for (int y = this.Box.GetRect().Y; y < this.Box.GetRect().Height + texH; y += texH)
                {
                    for (int x = this.Box.GetRect().X; x < this.Box.GetRect().Width + texW; x += texW)
                    {
                        this._spriteBatch.Draw(this._texture,new Vector2(x,y),null,this._texture.Bounds, this._origin, this._rotation, null, this._drawColor, SpriteEffects.None, 0.0f);
                    }
                }
            }
            else
            {
                this._spriteBatch.Draw(this._texture, null, this._box.GetRect(this._origin * this._textureScale),this._sourceRect, this._origin, this._rotation, null, this._drawColor, SpriteEffects.None, 0.0f);
            }
            this._spriteBatch.End();
        }


    }
}
