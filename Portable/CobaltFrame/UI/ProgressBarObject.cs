using CobaltFrame.Common;
using CobaltFrame.Context;
using CobaltFrame.Object;
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
    public class ProgressBarObject:GameObject
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
                var innerBox = this.InnerObject.Rect;
				var width = (int)((float)(innerBox.Width) * value);

                 
                this.InnerObject.SetRect(new Rectangle(
                    this.InnerMargin.Left,
                    this.InnerMargin.Top,
					width-InnerMargin.Right,
					this.FrameObject.Rect.Height-InnerMargin.Bottom*2
                    ));

            }
        }
        public ProgressBarObject(Margin innerMargin,string frameTexturePath,string innerTexturePath)
        {
			this.InnerMargin = innerMargin;
            this.FrameObject = new Texture2DObject(frameTexturePath);

            var innerBox = new Rectangle(
                this.InnerMargin.Left,
                this.InnerMargin.Top,
                this.FrameObject.Rect.Width-this.InnerMargin.Right,
                this.FrameObject.Rect.Height - this.InnerMargin.Bottom*2
                );
            
            this.InnerObject = new Texture2DObject(innerTexturePath);
            this.SetRect(innerBox);
            this.AddChild(this.FrameObject);
            this.AddChild(this.InnerObject);
            this.CurrentProgress = 1.0f;
        }

        
    }
}
