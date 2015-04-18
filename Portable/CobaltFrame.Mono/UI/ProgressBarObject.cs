using CobaltFrame.Core.Common;
using CobaltFrame.Mono.Context;
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
    public class ProgressBarObject:UIObject
    {

        protected Texture2DObject FrameObject { get; private set; }
        protected Texture2DObject InnerObject { get; private set; }
        public Margin InnerMargin { get; set; }

        private float _currentProgress;
        public float CurrentProgress 
        {
            get { return this._currentProgress; }
            set
            {
                this._currentProgress = value;
                var innerBox = this.InnerObject.Box as RelativeBox2;
                var width = (int)((float)innerBox.GetRect().Width * value);

                this.InnerObject.Box.SetRect(new Rectangle(
                    innerBox.GetRelativeRect().X,
                    innerBox.GetRelativeRect().Y,
                    width,
                    innerBox.GetRect().Height
                    ));
            }
        }
        public ProgressBarObject(IBox2 box,string frameTexturePath,string innerTexturePath)
            :base(box)
        {
            this.InnerMargin = new Margin(5, 5, 5, 5);
            this.FrameObject = new Texture2DObject(box,frameTexturePath);

            var innerBox = new RelativeBox2(new Rectangle(
                this.InnerMargin.Left,
                this.InnerMargin.Top,
                box.GetRect().Width-this.InnerMargin.Right,
                box.GetRect().Height - this.InnerMargin.Bottom
                ),box);
            
            this.InnerObject = new Texture2DObject(innerBox,innerTexturePath);
            this.AddDrawableObject(this.FrameObject);
            this.AddDrawableObject(this.InnerObject);
            this.CurrentProgress = 0.5f;
        }

        public override void Load()
        {
            base.Load();
            
        }
        public override void Update(Core.Context.IFrameContext context)
        {
            base.Update(context);

            
            /*
            Box2 innerBox = this.InnerObject.Box;
            innerBox = this.Box.GetBox2WithMargin(this.InnerMargin);
            int marginX = innerBox.GetRect().Width - (int)(innerBox.GetRect().Width * this.CurrentProgress);
            innerBox.SetRect(new Rectangle(
                innerBox.GetRect().X-marginX/2,innerBox.GetRect().Y,(int)(innerBox.GetRect().Width*this.CurrentProgress),innerBox.GetRect().Height
                ));
             
            this.InnerObject.Box = innerBox;
            */
        }
        public override void Draw(Core.Context.IFrameContext context)
        {
            base.Draw(context);

        }
    }
}
