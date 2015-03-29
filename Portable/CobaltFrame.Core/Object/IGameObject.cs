using CobaltFrame.Core.Common;
using CobaltFrame.Core.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Core.Object
{
    public interface IGameObject
    {
        IGameContext GameContext { get; }
        ObjectLoadState LoadState { get; }
        /// <summary>
        /// このObjectにぶら下がる子オブジェクトのリスト
        /// 描画はされない
        /// </summary>
        List<IGameObject> GameObjects { get; }
        bool IsActive { get; set; }
        void Initialize();
        void LoadObject();
        void UnloadObject();
        void Update(IFrameContext context);
    }
}
