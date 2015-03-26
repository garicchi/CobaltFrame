using CobaltFrame.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Object
{
    public abstract class GameObject:IGameObject,IComparable
    {
        protected ObjectContext _objectContext;

        /// <summary>
        /// 0.0f～1.0f
        /// </summary>
        public float DrawDepth { get; private set; }

        public bool IsVisible { get; set; }

        public GameObject(ObjectContext objectContext)
        {
            this._objectContext = objectContext;
            this.IsVisible = true;
            this.DrawDepth = 0.0f;
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

        public int CompareTo(object obj)
        {
            GameObject gObj = obj as GameObject;
            if (gObj.DrawDepth < this.DrawDepth) { return -1; }
            if (gObj.DrawDepth > this.DrawDepth) { return 1; }
            if (gObj.DrawDepth == this.DrawDepth) { return 0; }

            return 0;
        }

        /// <summary>
        /// このメソッドを呼ばないでくださいScreenBaseから内部的に呼ばれます
        /// </summary>
        /// <param name="depth"></param>
        public void SetDrawDepth(float depth)
        {
            this.DrawDepth = depth;
        }
    }
}
