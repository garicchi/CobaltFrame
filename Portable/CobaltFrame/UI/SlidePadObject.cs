using CobaltFrame.Context;
using CobaltFrame.Input;
using CobaltFrame.Object;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CobaltFrame.Common;

namespace CobaltFrame.Mono.UI
{
    public class SlidePadObject:GameObject
    {
        protected Texture2DObject PadObject { get; set; }
        protected Texture2DObject BackObject { get; set; }

        public Point CurrentValue { get; private set; }
        private int _padTouchId { get; set; }

        public SlidePadObject(string padTexturePath,string backTexturePath)
        {
            this.BackObject = new Texture2DObject(backTexturePath);
            this.AddChild(this.BackObject);
            
            this.PadObject = new Texture2DObject(padTexturePath);
            this.PadObject.SetRect(this.GetPadRect(this._rect));

            this.AddChild(this.PadObject);
            this.CurrentValue = Point.Zero;
        }

        private Rectangle GetPadRect(Rectangle backRect)
        {
            int backW = backRect.Width;
            int backH = backRect.Height;
            int padW = backRect.Width * 3 / 4;
            int padH = backRect.Height * 3 / 4;
            return new Rectangle((backW - padW) / 2, (backH - padH) / 2, padW, padH);

        }

        public override void Load()
        {
            base.Load();
            Inputs.RegisterInput("_PadSlide", () =>
            {
                if (InputContext.TouchCollection.IsTouch)
                {
                    var touchEnum = InputContext.TouchCollection.Where(q=>this.PadObject.Rect.Contains(q.Position));
                    if(touchEnum.Count()>0){
                        var location = touchEnum.First();
                        this._padTouchId = location.Id;
                        
                        return true;
                    }else{
                        return false;
                    }

                }
                else
                {
                    return false;
                }
            });
        }

        public override void Update(FrameContext context)
        {
            base.Update(context);
            

            if (Inputs.IsInput("_PadSlide")&&this.Rect.Intersects(this.PadObject.Rect))
            {
                var location = InputContext.TouchCollection.Where(q => q.Id == _padTouchId).First();

                this.PadObject.SetPosition(
                    new Point(
                        (int)(location.Position.X - this.PadObject.Rect.Width / 2),
                        (int)(location.Position.Y - this.PadObject.Rect.Height/ 2)
                        )
                    );
                
                var delta = this.PadObject.Rect.GetCenter()- this.BackObject.Rect.GetCenter();
                
                this.CurrentValue = delta;
                
            }
            else
            {
                this.PadObject.SetRect(this.GetPadRect(this.BackObject.Rect));
                this.CurrentValue = Point.Zero;
            }
            
        }
    }
}
