using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Core.Screen
{
    public interface IScreenTransition
    {
        event Action OnCompleted;
        void Start();
    }
}
