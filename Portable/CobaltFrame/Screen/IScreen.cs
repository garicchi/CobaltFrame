using CobaltFrame.Common;
using CobaltFrame.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Screen
{
    public interface IScreen
    {

        void Initialize(object navigationParameter);
        void LoadScreen();
        void UnloadScreen();
        void Update(ScreenFrameContext frameContext);
        void Draw(ScreenFrameContext frameContext);

        
    }
}
