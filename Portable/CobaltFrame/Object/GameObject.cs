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
        public GameObject(ObjectContext context)
        {
            this._objectContext = context;
        }
        public virtual void Initialize()
        {
            
        }

        public virtual void LoadObject()
        {
            
        }

        public virtual void UnloadObject()
        {
            
        }

        public virtual void Update(Common.ObjectFrameContext frameContext)
        {
            
        }
    }
}
