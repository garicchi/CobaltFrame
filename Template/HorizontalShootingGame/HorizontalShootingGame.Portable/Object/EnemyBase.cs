using CobaltFrame.Mono.Animation;
using CobaltFrame.Mono.Context;
using CobaltFrame.Mono.Position;
using CobaltFrame.Mono.UI;
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

        protected Box2TimeAnimation Animation { get; set; }
        public EnemyBase(IBox2 box,string texture,Box2TimeAnimation animation,TimeSpan startTime)
            :base(box,texture)
        {
            IsVisible = false;
            _isStart = false;
            _startTime = startTime;
            this.Animation = animation;
            this.Animation.OnCompleted += () =>
            {
                this._isStart = false;
                this.IsVisible = false;
                
            };
            this.AddObject(animation);
        }

        public override void Update(CobaltFrame.Core.Context.IFrameContext context)
        {
            base.Update(context);
            var fContext = context as FrameContext;
            if (fContext.ElapsedScreenTime > StartTime && IsStart == false)
            {
                this.IsVisible = true;
                _isStart = true;
                Animation.Start();
            }

            if (IsStart)
            {
                this.Box.SetRect(Animation.CurrentValue.GetRect());
                
            }
        }
    }
}
