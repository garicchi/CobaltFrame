using CobaltFrame.Core.Common;
using CobaltFrame.Mono.Input;
using CobaltFrame.Mono.Position;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Mono.UI
{
    public class SlidePadObject:UIObject
    {
        protected Texture2DObject PadObject { get; set; }
        protected Texture2DObject BackObject { get; set; }

        public Vector2 CurrentValue { get; private set; }
        private int _padTouchId { get; set; }

        public SlidePadObject(IBox2 box,string padTexturePath,string backTexturePath)
            :base(box)
        {
            this.BackObject = new Texture2DObject(box, backTexturePath);
            this.AddDrawableObject(this.BackObject);
            
            this.PadObject = new Texture2DObject(new RelativeBox2(GetPadRect(box.GetRect()),box), padTexturePath);

            this.AddDrawableObject(this.PadObject);
            this.CurrentValue = Vector2.Zero;
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
                if (GameInput.TouchCollection.IsTouch)
                {
                    var touchEnum = GameInput.TouchCollection.Where(q=>this.PadObject.Box.Contains(q.Position));
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

        public override void Update(Core.Context.IFrameContext context)
        {
            base.Update(context);
            

            if (Inputs.IsInput("_PadSlide")&&this.Box.Intersects(this.PadObject.Box))
            {
                var location = GameInput.TouchCollection.Where(q => q.Id == _padTouchId).First();

                var box = this.PadObject.Box as RelativeBox2;
                
                box.SetAbsoluteLocation(new Vector2(
                    location.Position.X - box.GetRect().Width / 2,
                    location.Position.Y - box.GetRect().Height/ 2));
                var delta = box.GetCenter()- this.BackObject.Box.GetCenter();
                
                this.CurrentValue = delta;
            }
            else
            {
                this.PadObject.Box = new RelativeBox2(GetPadRect(this.BackObject.Box.GetRect()), this.BackObject.Box);
                this.CurrentValue = Vector2.Zero;
            }
            
        }
    }
}
