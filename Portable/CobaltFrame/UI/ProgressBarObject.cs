using CobaltFrame.Common;
using CobaltFrame.Context;
using CobaltFrame.Object;
using CobaltFrame.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.UI
{
    public class ProgressBarObject:GameObject2D
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
                var innerBox = this.InnerObject.GetRect();
				var width = (int)((float)(innerBox.Width) * value);

                 
                this.InnerObject.SetRect(new Rectangle(
                    this.InnerMargin.Left,
                    this.InnerMargin.Top,
					width-InnerMargin.Right,
					this.FrameObject.GetRect().Height-InnerMargin.Bottom*2
                    ));

            }
        }
        public ProgressBarObject(Margin innerMargin,string frameTexturePath,string innerTexturePath)
        {
			this.InnerMargin = innerMargin;
            this.FrameObject = new Texture2DObject(frameTexturePath);
            this.InnerObject = new Texture2DObject(innerTexturePath);
            this.AddChild(this.FrameObject);
            this.FrameObject.AddChild(this.InnerObject);
            this.CurrentProgress = 1.0f;
        }

        public override void SetRect(Rectangle rect)
        {
            this.FrameObject.SetRect(rect);
            var innerBox = new Rectangle(
                this.InnerMargin.Left,
                this.InnerMargin.Top,
                this.FrameObject.GetRect().Width - this.InnerMargin.Right,
                this.FrameObject.GetRect().Height - this.InnerMargin.Bottom * 2
                );
            this.InnerObject.SetRect(innerBox);
            base.SetRect(rect);
            
        }

        public override void Load()
        {
            base.Load();
        }

        public override void Update(FrameContext context)
        {
            base.Update(context);
        }
        
    }
}
