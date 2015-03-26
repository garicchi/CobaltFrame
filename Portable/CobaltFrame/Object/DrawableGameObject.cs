using CobaltFrame.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Object
{
    public abstract class DrawableGameObject:GameObject,IDrawableGameObject,IComparable
    {
        
        /// <summary>
        /// 0.0f～1.0f
        /// </summary>
        public float DrawDepth { get; private set; }

        public bool IsVisible { get; set; }

        public DrawableGameObject(ObjectContext objectContext)
            :base(objectContext)
        {
            this.IsVisible = true;
            this.DrawDepth = 0.0f;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void LoadObject()
        {
            base.LoadObject();
        }

        public override void UnloadObject()
        {
            base.UnloadObject();
        }

        public override void Update(ObjectFrameContext frameContext)
        {
            base.Update(frameContext);
        }

        public virtual void Draw(ObjectFrameContext frameContext)
        {
            
        }

        public int CompareTo(object obj)
        {
            DrawableGameObject gObj = obj as DrawableGameObject;
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
