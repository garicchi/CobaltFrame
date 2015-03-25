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
        public void Initialize();
        public void LoadScreen();
        public void UnloadScreen();
        public IScreen Update(ScreenFrameContext frameContext);
        public void Draw(ScreenFrameContext frameContext);

        public void AddObject(GameObject gameObject);

        public void RemoveObject(GameObject gameObject);
        public bool HasObject(GameObject gameObject);
    }
}
