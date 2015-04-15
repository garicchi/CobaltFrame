using CobaltFrame.Core.Common;
using CobaltFrame.Core.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Core.Object
{
    public interface IObject
    {
        IGameContext GameContext { get; }
        ObjectLoadState LoadState { get; }

        void Initialize();
        void LoadObject();
        void UnloadObject();
    }
}
