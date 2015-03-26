using CobaltFrame.Common;
using CobaltFrame.Object;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Test.Object
{
    public class SampleTexture2DObject:DrawableGameObject
    {
        protected Texture2D _texture;

        protected Vector2 _position;

        protected string _texturePath;
        public SampleTexture2DObject(ObjectContext context,Vector2 pos,string texturePath)
            :base(context)
        {
            this._texturePath = texturePath;
            this._position = pos;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void LoadObject()
        {
            base.LoadObject();
            
            this._texture = this._game.Content.Load<Texture2D>(this._texturePath);
            
        }

        public override void UnloadObject()
        {
            base.UnloadObject();
        }

        public override void Update(ObjectFrameContext frameContext)
        {
            base.Update(frameContext);
            
        }

        public override void Draw(ObjectFrameContext frameContext)
        {
            base.Draw(frameContext);

            this._spriteBatch.Begin();

            this._spriteBatch.Draw(this._texture,this._position,Color.White);

            this._spriteBatch.End();
        }
    }
}
