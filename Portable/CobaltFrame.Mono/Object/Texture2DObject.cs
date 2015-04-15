using CobaltFrame.Mono.Context;
using CobaltFrame.Position;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Mono.Object
{
    public class Texture2DObject:DrawableGameObject
    {
        private string _texturePath;

        protected string TexturePath
        {
            get { return _texturePath; }
        }

        private Texture2D _texture;

        protected Texture2D Texture
        {
            get { return _texture; }
        }

        private Vector2 _textureScale;

        private Vector2 TextureScale
        {
            get { return _textureScale; }
        }

        public Texture2DObject(GameContext context,Box2 position,string texturePath)
            :base(context,position)
        {
            this._texturePath = texturePath;
        }

        public override void Load()
        {
            base.Load();
            this._texture = this._game.Content.Load<Texture2D>(this._texturePath);
            this._origin = new Vector2(this._texture.Width/2.0f,this._texture.Height/2.0f);
            this._textureScale = new Vector2((float)this._position.GetRect().Width / (float)this._texture.Width, (float)this._position.GetRect().Height / (float)this._texture.Height);
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
            
            this._spriteBatch.Begin(SpriteSortMode.Deferred,null,null,null,null,null,(context as FrameContext).ScreenTrans);
            this._spriteBatch.Draw(this._texture, null, this._position.GetRect(this._origin*this._textureScale), null, this._origin, this._rotation, null, this._drawColor, SpriteEffects.None, 0.0f);
            
            this._spriteBatch.End();
        }

        
    }
}
