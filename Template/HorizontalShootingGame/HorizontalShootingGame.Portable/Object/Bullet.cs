using CobaltFrame.Mono.Animation;
using CobaltFrame.Mono.Context;
using CobaltFrame.Mono.Object;
using CobaltFrame.Mono.Position;
using CobaltFrame.Mono.UI;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorizontalShootingGame.Portable.Object
{
    public class Bullet:Texture2DObject
    {
        protected Box2ConditionAnimation Animation { get; set; }


        public Bullet(IBox2 pos,string texturePath)
            :base(pos,texturePath)
        {
            this.Animation = new Box2ConditionAnimation(pos, (current, time) =>
            {
                current.MoveRect(0,0,0,40);
                return current;
            });
            this.Animation.StopTriggers.Add((current) =>
            {
                int width = this._game.GraphicsDevice.Viewport.Width;
                return (current.GetLocation().X > width);
                
            });
            this.Animation.OnCompleted += () =>
            {
                this.IsVisible = false;
            };
            this.AddObject(Animation);
        }

        public override void Init()
        {
            base.Init();
            this.IsVisible = false;
        }
        public void Shot()
        {
            this.IsVisible = true;
            this.Animation.Start();
        }

        public override void Update(CobaltFrame.Core.Context.IFrameContext context)
        {
            base.Update(context);
            this.Box.SetLocation(this.Animation.CurrentValue.GetLocation());
        }
    }
}
