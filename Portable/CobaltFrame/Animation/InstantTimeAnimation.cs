using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Animation
{
	/// <summary>
	/// 簡易タイムアニメーション(更新式を指定してアニメーションを作成可能)
	/// </summary>
    public class InstantTimeAnimation<T>:TimeAnimation<T>
    {

        private Func<T, T,float, T> _expression;


        public InstantTimeAnimation(T begin,T end,TimeSpan duration,Func<T,T,float,T> expression)
            : base(duration,begin,end)
        {

            this._expression = expression;
        }

		/// <summary>
		/// 時間更新式
		/// </summary>
		/// <returns>更新後の値</returns>
		/// <param name="begin">初期値</param>
		/// <param name="end">終了値</param>
		/// <param name="currentProgress">現在の値</param>
        protected override T UpdateExpression(T begin, T end, float currentProgress)
        {
            return _expression(begin, end, currentProgress);
        }
    }
}
