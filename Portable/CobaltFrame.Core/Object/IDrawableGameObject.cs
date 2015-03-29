using CobaltFrame.Core.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Core.Object
{
    public interface IDrawableGameObject:IGameObject
    {
        bool IsVisible { get; set; }
        float LayerDepth { get; }
        /// <summary>
        /// このオブジェクトにぶら下がる子オブジェクト
        /// 描画される
        /// </summary>
        List<IDrawableGameObject> DrawableObjects { get; }
        void Draw(IFrameContext context);
        void ChangeChildDrawableObjectLayer(IDrawableGameObject obj,float layer);
    }
}
