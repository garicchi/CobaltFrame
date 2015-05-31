using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace CobaltFrame.Animation
{
    /// <summary>
    /// Point型の時間指定アニメーション
    /// </summary>
    public class PointTimeAnimation : TimeAnimation<Point>
    {
        /// <summary>
        /// Point型の時間指定アニメーション
        /// </summary>
        /// <param name="duration">アニメーションの長さ</param>
        /// <param name="begin">初期値</param>
        /// <param name="end">終了地</param>
        public PointTimeAnimation(TimeSpan duration, Point begin, Point end)
            : base(duration, begin, end)
        {

        }


        protected override Point UpdateExpression(Point begin, Point end, float currentProgress)
        {
            var newValue = end - begin;
            newValue.X = (int)(newValue.X * currentProgress);
            newValue.Y = (int)(newValue.Y * currentProgress);
            return  newValue + begin;
        }
    }
}
