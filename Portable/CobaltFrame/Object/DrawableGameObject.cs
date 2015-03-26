using CobaltFrame.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Object
{
    public abstract class DrawableGameObject:GameObject,IDrawableGameObject
    {

        public bool IsVisible { get; set; }

        public DrawableGameObject(ObjectContext objectContext)
            :base(objectContext)
        {
            this.IsVisible = true;
            
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

    }
}
