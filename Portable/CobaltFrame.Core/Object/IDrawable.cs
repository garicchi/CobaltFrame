using CobaltFrame.Core.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Core.Object
{
    public interface IDrawable:IUpdatable
    {
        bool IsVisible { get; set; }
        float LayerDepth { get; set; }

        bool IsObjectLayerChanged { get; }
        /// <summary>
        /// このオブジェクトにぶら下がる子オブジェクト
        /// 描画される
        /// </summary>
        List<IDrawable> DrawableObjects { get; }
        void Draw(IFrameContext context);
    }
}
