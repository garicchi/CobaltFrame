﻿using CobaltFrame.Context;
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
using CobaltFrame.UI;
using Microsoft.Xna.Framework.Input;

namespace CobaltFrame.UI
{
    public class SlidePadObject:GameObject2D
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
            Inputs.RegisterInput("_PadTouch", (current,prev) =>
            {
                if (current.IsTouch)
                {
                    var touchEnum = current.Where(q=>this.PadObject.GetRect().Contains(q.Position));
                    
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

            Inputs.RegisterInput("_PadClick", null,(current, prev) =>
            {
                if (current.LeftButton==ButtonState.Pressed)
                {
                    return this.PadObject.GetRect().Contains(current.Position);

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
            

            if (Inputs.IsInput("_PadTouch")&&this.BackObject.GetRect().Intersects(this.PadObject.GetRect()))
            {
                var location = InputContext.TouchCollection.Where(q => q.Id == _padTouchId).First();
                
                this.PadObject.SetAbsolutePosition(
                    new Point(
                        (int)(location.Position.X - this.PadObject.GetRect().Width / 2),
                        (int)(location.Position.Y - this.PadObject.GetRect().Height / 2)
                        )
                    );
                var delta = this.PadObject.GetCenter()- this.BackObject.GetCenter();
                
                this.CurrentValue = delta;
                
            }else if (Inputs.IsInput("_PadClick") && this.BackObject.GetRect().Intersects(this.PadObject.GetRect()))
            {
                var location = InputContext.MouseState.Position;

                this.PadObject.SetAbsolutePosition(
                    new Point(
                        (int)(location.X - this.PadObject.GetRect().Width / 2),
                        (int)(location.Y - this.PadObject.GetRect().Height / 2)
                        )
                    );
                var delta = this.PadObject.GetCenter() - this.BackObject.GetCenter();

                this.CurrentValue = delta;

            }
            else
            {
                this.PadObject.SetRect(this.GetPadRect(this.BackObject.GetRect()));
                this.CurrentValue = Point.Zero;
            }
            
        }
    }
}
