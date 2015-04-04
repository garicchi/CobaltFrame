using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Core.Progress
{
    public interface IConditionProgress<T>:IProgress<T>
    {

        IList<Func<T, bool>> BeginTriggers { get; set; }
        IList<Func<T, bool>> StopTriggers { get; set; }
        
    }
}
