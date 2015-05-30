using CobaltFrame.Animation;
using CobaltFrame.Context;
using CobaltFrame.UI;
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
        protected PointConditionAnimation Animation { get; set; }


        public Bullet(string texturePath)
            :base(texturePath)
        {
            this.Animation = new PointConditionAnimation(this.GetPosition(),(current, time) =>
            {
                current.X += 40;
                return current;
            });
            this.Animation.StopTriggers.Add((current) =>
            {
                int width = GameContext.DefaultResolution.X;
                return (current.X > width);
                
            });
            this.Animation.OnCompleted += () =>
            {
                this.IsVisible = false;
            };
            this.AddChild(Animation);
        }

        public override void Init()
        {
            base.Init();
            this.IsVisible = false;
        }
        public void Shot(Point init)
        {
            this.IsVisible = true;
            this.Animation.BeginValue = init;
            this.Animation.Start();
        }

		public void Hit()
		{
			this.IsVisible = false;
		}

        public override void Update(FrameContext context)
        {
            base.Update(context);
            if (this.Animation.State != AnimationState.Stop)
            {
                this.SetPosition(this.Animation.CurrentValue);
            }
        }
    }
}
