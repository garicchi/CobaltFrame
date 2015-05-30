using CobaltFrame.Animation;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Animation
{
    /// <summary>
    /// Box2型の条件指定アニメーション
    /// </summary>
    public class PointConditionAnimation : ConditionAnimation<Point>
    {
        /// <summary>
        /// 更新関数のデリゲート
        /// </summary>
        protected Func<Point, TimeSpan, Point> Expression { get; private set; }

        /// <summary>
        /// Box2型の条件指定アニメーション
        /// </summary>
        /// <param name="beginValue">初期値</param>
        /// <param name="expression">更新関数</param>
        public PointConditionAnimation(Point beginValue, Func<Point, TimeSpan, Point> expression)
            : base(beginValue)
        {
            this.Expression = expression;
        }

        protected override Point UpdateExpression(Point currentValue, TimeSpan elapsedTime)
        {
            return this.Expression(currentValue, elapsedTime);
        }

    }
}
