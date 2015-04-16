using CobaltFrame.Core.Common;
using CobaltFrame.Mono.Input;
using CobaltFrame.Position;
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
        public SlidePadObject(Box2 box,string padTexturePath,string backTexturePath)
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
                    return GameInput.TouchCollection.First().State == TouchLocationState.Moved
                        &&this.PadObject.Box.Contains(GameInput.TouchCollection.First().Position);
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
            

            if (Inputs.IsInput("_PadSlide"))
            {
                
                var delta=GameInput.TouchCollection.First().Position-GameInput.TouchCollectionPrev.First().Position;
                var tryBox = this.PadObject.Box.TryMoveRect(0, (int)delta.Y, (int)delta.X, 0);
                if (this.Box.Intersects(tryBox))
                {
                    this.PadObject.Box.MoveRect(0, (int)delta.Y, (int)delta.X, 0);
                }
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
