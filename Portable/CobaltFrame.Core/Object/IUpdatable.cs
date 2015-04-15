using CobaltFrame.Core.Common;
using CobaltFrame.Core.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Core.Object
{
    public interface IUpdatable:IObject
    {
        /// <summary>
        /// このObjectにぶら下がる子オブジェクトのリスト
        /// 描画はされない
        /// </summary>
        List<IUpdatable> GameObjects { get; }
        bool IsActive { get; set; }
        
        void Update(IFrameContext context);
    }
}
