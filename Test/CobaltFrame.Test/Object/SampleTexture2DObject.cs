using CobaltFrame.Common;
using CobaltFrame.Object;
using CobaltFrame.Test.Animation;
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

        private SampleLinearAnimation _animation;

        protected string _texturePath;
        public SampleTexture2DObject(ObjectContext context,Vector2 pos,string texturePath)
            :base(context)
        {
            this._texturePath = texturePath;
            this._position = pos;
            this._animation = new SampleLinearAnimation(context,new Vector2(50,50),new Vector2(400,600));

            this.AddObject(this._animation);
            this._animation.Start(TimeSpan.FromSeconds(3),true);
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
            this._texture.Dispose();
        }
        

        public override void Update(ObjectFrameContext frameContext)
        {
            base.Update(frameContext);
            
        }

        public override void Draw(ObjectFrameContext frameContext)
        {
            base.Draw(frameContext);

            this._spriteBatch.Begin();

            this._spriteBatch.Draw(this._texture,this._animation.CurrentPosition,Color.White);

            this._spriteBatch.End();
        }
    }
}
