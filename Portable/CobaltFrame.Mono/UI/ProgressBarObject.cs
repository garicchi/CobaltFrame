using CobaltFrame.Core.Common;
using CobaltFrame.Mono.Context;
using CobaltFrame.Mono.Position;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
                var width = (int)((float)(this.Box.GetRect().Width - this.InnerMargin.Right) * value);

                 
                this.InnerObject.Box.SetRect(new Rectangle(
                    this.InnerMargin.Left,
                    this.InnerMargin.Top,
                    width,
                    this.InnerObject.Box.GetRect().Height
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
            this.CurrentProgress = 1.0f;
        }

        public override void Load()
        {
            base.Load();
            
        }
        public override void Update(Core.Context.IFrameContext context)
        {
            base.Update(context);

            
        }
        public override void Draw(Core.Context.IFrameContext context)
        {
            base.Draw(context);

        }
    }
}
