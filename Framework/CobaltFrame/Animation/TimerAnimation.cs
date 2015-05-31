using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Animation
{
	/// <summary>
	/// 指定時間だけ計るタイマーとなるアニメーション
	/// </summary>
    public class TimerAnimation:TimeAnimation<float>
    {
        public TimerAnimation(TimeSpan duration)
            : base(duration, 0.0f, 1.0f)
        {

        }

        protected override float UpdateExpression(float begin, float end, float currentProgress)
        {
            //とりあえず0.0~1.0になるような式だけどタイマーなので関係ない1
            return (end - begin) * currentProgress + begin;
        }

        
    }
}
