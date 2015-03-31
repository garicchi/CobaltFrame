using CobaltFrame.Context;
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
        public Texture2DObject(GameContext context,Position2D position,string texturePath)
            :base(context,position)
        {
            this._texturePath = texturePath;
        }

        public override void LoadObject()
        {
            base.LoadObject();
            this._texture = this._game.Content.Load<Texture2D>(this._texturePath);
            
        }

        public override void UnloadObject()
        {
            base.UnloadObject();
            this._texture.Dispose();
        }

        public override void Update(Core.Context.IFrameContext context)
        {
            base.Update(context);
        }

        public override void Draw(Core.Context.IFrameContext context)
        {
            base.Draw(context);
            this._spriteBatch.Begin();
            this._spriteBatch.Draw(this._texture,this._position.GetPosition(),Color.White);
            this._spriteBatch.End();
        }

        
    }
}
