using CobaltFrame.Core.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Core.Progress
{
    public interface ITimeProgress<T>:IProgress<T>
    {
        
        TimeSpan Duration{ get; }
        
        float CurrentProgress { get; set; }
        bool IsLoop { get; set; }

        T EndValue { get; set; }
        

    }
}
