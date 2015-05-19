using CobaltFrame.Core.Common;
using CobaltFrame.Core.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Core.Object
{
	/// <summary>
	/// 非描画オブジェクト
	/// </summary>
    public interface IUpdatable:IObject
    {
        /// <summary>
        /// このObjectにぶら下がる子オブジェクトのリスト
        /// 描画はされない
        /// </summary>
        List<IUpdatable> GameObjects { get; }

		/// <summary>
		/// 更新するかどうか
		/// </summary>
		/// <value><c>true</c> if this instance is active; otherwise, <c>false</c>.</value>
        bool IsActive { get; set; }
        
		/// <summary>
		/// 更新関数
		/// </summary>
		/// <param name="context">Context.</param>
        void Update(IFrameContext context);
    }
}
