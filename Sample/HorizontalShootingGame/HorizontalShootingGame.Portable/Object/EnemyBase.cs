using CobaltFrame.Animation;
using CobaltFrame.Context;
using CobaltFrame.UI;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorizontalShootingGame.Portable.Object
{
    public class EnemyBase:Texture2DObject
    {
        protected TimeSpan _startTime;

        public TimeSpan StartTime
        {
            get { return _startTime; }
        }

        protected bool _isStart;

        public bool IsStart
        {
            get { return _isStart; }
        }

        protected IAnimation<Point> Animation { get; set; }
        public EnemyBase(string texture,IAnimation<Point> animation,TimeSpan startTime)
            :base(texture)
        {
            IsVisible = false;
            _isStart = false;
            _startTime = startTime;
            this.Animation = animation;
            
            this.Animation.OnCompleted += () =>
            {
                this.IsVisible = false;
                
            };
            this.AddChild(animation);
        }

        public override void Update(FrameContext context)
        {
            base.Update(context);
            if (context.ElapsedScreenTime > StartTime && IsStart == false)
            {
                this.IsVisible = true;
                _isStart = true;
                Animation.Start();
            }

            if (IsStart)
            {
                this.SetPosition(Animation.CurrentValue);
            }
            
        }

		public void Die()
		{
			this.IsVisible = false;
		}
    }
}
