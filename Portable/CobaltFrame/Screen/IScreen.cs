using CobaltFrame.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Screen
{
    public interface IScreen
    {
        public void Initialize(ScreenContext screenContext);
        public void LoadScreen();
        public void UnloadScreen();
        public IScreen Update(ScreenFrameContext frameContext);
        public void Draw(ScreenFrameContext frameContext);
    }
}
