using CobaltFrame.Mono.Context;
using CobaltFrame.Core.Animation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CobaltFrame.Core.Progress;
using CobaltFrame.Mono.Position;

namespace CobaltFrame.Mono.Animation
{
    /// <summary>
    /// 指定時間Box2座標で待機するアニメーション
    /// </summary>
    public class WaitBox2Animation:TimeProgress<Box2>
    {
        public WaitBox2Animation(TimeSpan duration, Box2 begin)
            : base(duration, begin, begin)
        {

        }

        protected override Box2 UpdateExpression(Box2 begin, Box2 end, float currentProgress)
        {
            return begin;
        }
    }
}
