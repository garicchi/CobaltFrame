using CobaltFrame.Core.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Core.Object
{
	/// <summary>
	/// 描画オブジェクトのインターフェース
	/// </summary>
    public interface IDrawable:IUpdatable
    {
		/// <summary>
		/// 表示するかどうか
		/// </summary>
		/// <value><c>true</c> if this instance is visible; otherwise, <c>false</c>.</value>
        bool IsVisible { get; set; }
        /// <summary>
        /// レイヤーの深度
        /// </summary>
        /// <value>The layer depth.</value>
		float LayerDepth { get; set; }

		/// <summary>
		/// オブジェクトのレイヤーが変更されたかどうか
		/// </summary>
		/// <value><c>true</c> if this instance is object layer changed; otherwise, <c>false</c>.</value>
        bool IsObjectLayerChanged { get; }
        
		/// <summary>
        /// このオブジェクトにぶら下がる子オブジェクト
        /// 描画される
        /// </summary>
        List<IDrawable> DrawableObjects { get; }
        
		/// <summary>
		/// 描画関数
		/// </summary>
		/// <param name="context">Context.</param>
		void Draw(IFrameContext context);
    }
}
