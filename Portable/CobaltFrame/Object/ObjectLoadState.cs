using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Common
{
	/// <summary>
	/// ゲームオブジェクトがどこまでロードされているかの列挙体
	/// </summary>
    public enum ObjectLoadState
    {
        Created = 0,Initialized = 1,Loaded = 2,Unloaded = 3
    }
}
