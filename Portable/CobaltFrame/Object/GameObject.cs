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

        private float _layerDepth;
        public float LayerDepth
        {
            get { return this._layerDepth; }
        }
        public GameObject(ObjectContext context)
        {
            this._objectContext = context;
            this._layerDepth = 0.0f;
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

        public int CompareTo(object obj)
        {
            DrawableGameObject gObj = obj as DrawableGameObject;
            if (gObj.LayerDepth < this.LayerDepth) { return -1; }
            if (gObj.LayerDepth > this.LayerDepth) { return 1; }
            if (gObj.LayerDepth == this.LayerDepth) { return 0; }
            
            return 0;
        }

        /// <summary>
        /// このメソッドを呼ばないでくださいScreenBaseから内部的に呼ばれます
        /// </summary>
        /// <param name="depth"></param>
        public void SetDrawDepth(float depth)
        {
            if (depth >= 0.0f && depth <= 1.0f)
            {
                this._layerDepth = depth;
            }
            else
            {
                throw new ArgumentOutOfRangeException("depth","レイヤーの深さは0.0f~1.0fの範囲で指定してください");
            }
            
        }
    }
}
