using CobaltFrame.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Animation
{
    public interface IAnimation:IGameObject
    {
        void Start(TimeSpan duration,bool isLoop);
        void Pause();

        void Resume();
        void Stop();
    }
}
