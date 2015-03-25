using CobaltFrame.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Object
{
    public abstract class GameObject:IGameObject
    {
        protected ObjectContext _objectContext;
        public GameObject(ObjectContext objectContext)
        {
            this._objectContext = objectContext;
        }
        public void Initialize()
        {
            
        }

        public void LoadObject()
        {
            
        }

        public void UnloadObject()
        {
            
        }

        public void Update(Common.ObjectFrameContext frameContext)
        {
            
        }

        public void Draw(Common.ObjectFrameContext frameContext)
        {
            
        }
    }
}
