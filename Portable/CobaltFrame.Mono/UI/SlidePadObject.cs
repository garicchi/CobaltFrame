using CobaltFrame.Core.Common;
using CobaltFrame.Mono.Input;
using CobaltFrame.Position;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Mono.UI
{
    public class SlidePadObject:UIObject
    {
        protected Texture2DObject PadObject { get; set; }
        protected Texture2DObject BackObject { get; set; }
        public Margin PadMargin { get; set; }
        public SlidePadObject(Box2 box,string padTexturePath,string backTexturePath)
            :base(box)
        {
            this.BackObject = new Texture2DObject(box, backTexturePath);
            this.AddDrawableObject(this.BackObject);

            this.PadObject = new Texture2DObject(box,padTexturePath);
            this.PadMargin = new Margin(30, 30, 30, 30);
            this.PadObject.Box = this.BackObject.Box.GetBox2WithMargin(this.PadMargin);
            this.AddDrawableObject(this.PadObject);
            
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
                this.PadObject.Box.SetLocation(new Vector2(
                    GameInput.TouchCollection.First().Position.X-this.PadObject.Box.GetRect().Width/2,
                    GameInput.TouchCollection.First().Position.Y - this.PadObject.Box.GetRect().Height / 2
                    ));
            }
            else
            {
                this.PadObject.Box = this.BackObject.Box.GetBox2WithMargin(this.PadMargin);
            }
        }
    }
}
