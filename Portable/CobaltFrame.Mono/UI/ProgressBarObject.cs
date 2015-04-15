using CobaltFrame.Core.Common;
using CobaltFrame.Mono.Context;
using CobaltFrame.Position;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Mono.UI
{
    public class ProgressBarObject:UIObject
    {

        protected Texture2DObject FrameObject { get; private set; }
        protected Texture2DObject InnerObject { get; private set; }
        public Margin InnerMargin { get; set; }

        public float CurrentProgress { get; set; }
        public ProgressBarObject(Box2 box,string frameTexturePath,string innerTexturePath)
            :base(box)
        {
            this.InnerMargin = new Margin(5, 5, 5, 5);
            this.FrameObject = new Texture2DObject(box,frameTexturePath);
            this.InnerObject = new Texture2DObject(box.GetBox2WithMargin(this.InnerMargin),innerTexturePath);
            this.AddDrawableObject(this.FrameObject);
            this.AddDrawableObject(this.InnerObject);
            this.CurrentProgress = 0.0f;
        }

        public override void Load()
        {
            base.Load();
            
        }
        public override void Update(Core.Context.IFrameContext context)
        {
            base.Update(context);
            Box2 innerBox = this.InnerObject.Box;
            innerBox = this.Box.GetBox2WithMargin(this.InnerMargin);
            int marginX = innerBox.GetRect().Width - (int)(innerBox.GetRect().Width * this.CurrentProgress);
            innerBox.SetRect(new Rectangle(
                innerBox.GetRect().X-marginX/2,innerBox.GetRect().Y,(int)(innerBox.GetRect().Width*this.CurrentProgress),innerBox.GetRect().Height
                ));
            this.InnerObject.Box = innerBox;
        }
        public override void Draw(Core.Context.IFrameContext context)
        {
            base.Draw(context);

        }
    }
}
