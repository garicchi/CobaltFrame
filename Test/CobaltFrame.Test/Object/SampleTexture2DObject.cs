using CobaltFrame.Common;
using CobaltFrame.Object;
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
        private Texture2D _texture;
        public SampleTexture2DObject(ObjectContext context)
            :base(context)
        {
            
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void LoadObject()
        {
            base.LoadObject();
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
        }
    }
}
