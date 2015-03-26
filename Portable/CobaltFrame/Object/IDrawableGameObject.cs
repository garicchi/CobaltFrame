using CobaltFrame.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Object
{
    public interface IDrawableGameObject:IGameObject
    {
        
        void Draw(ObjectFrameContext frameContext);
        void SetDrawDepth(float depth);
    }
}
