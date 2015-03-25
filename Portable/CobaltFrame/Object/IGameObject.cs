using CobaltFrame.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Object
{
    public interface IGameObject
    {
        public void Initialize();
        public void LoadContent();
        public void UnloadContent();
        public void Update(ObjectFrameContext frameContext);
        public void Draw(ObjectFrameContext frameContext);
        
    }
}
