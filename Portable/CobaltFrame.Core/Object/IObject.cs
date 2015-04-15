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
        ObjectLoadState LoadState { get; }

        void Init();
        void Load();
        void Unload();
    }
}
