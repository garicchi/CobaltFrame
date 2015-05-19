using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Core.Context
{
	/// <summary>
	/// フレーム単位でゲームオブジェクトに渡されるデータ
	/// </summary>
    public interface IFrameContext
    {
        TimeSpan TotalGameTime { get; }

        TimeSpan ElapsedGameTime { get; }
    }
}
