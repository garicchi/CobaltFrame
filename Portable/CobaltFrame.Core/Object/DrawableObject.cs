using CobaltFrame.Core.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Core.Object
{
    public abstract class DrawableObject:UpdatableObject,IDrawableObject,IComparable
    {
        public DrawableObject(IGameContext context)
            :base(context)
        {
            this._isVisible = true;
            this._layerDepth = 0.5f;
            this._drawableObjects = new List<IDrawableObject>();
            this._isObjectLayerChanged = false;
        }
        protected bool _isVisible;
        public bool IsVisible
        {
            get
            {
                return this._isVisible;
            }
            set
            {
                this._isVisible = value;
            }
        }

        private List<IDrawableObject> _drawableObjects;
        public List<IDrawableObject> DrawableObjects
        {
            get { return _drawableObjects; }
        }

        protected float _layerDepth;
        public float LayerDepth
        {
            get { return this._layerDepth; }
        }
        private bool _isObjectLayerChanged;

        public virtual void Draw(IFrameContext context)
        {
            if (this._isObjectLayerChanged)
                this.SortObject();

            foreach (var obj in this._drawableObjects)
            {
                if (obj.IsVisible)
                    obj.Draw(context);
            }

            this._isObjectLayerChanged = false;
        }

        public void ChangeChildDrawableObjectLayer(IDrawableObject obj, float layer)
        {
            if (this._drawableObjects.Contains(obj))
            {
                if (layer >= 0.0f && layer <= 1.0f)
                {
                    this._layerDepth = layer;
                    this._isObjectLayerChanged = true;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("layer depth range is 0.0f~1.0f");
                }

            }
        }

        public int CompareTo(object obj)
        {
            var gObj = (IDrawableObject)obj;
            if (gObj.LayerDepth < this.LayerDepth) { return -1; }
            if (gObj.LayerDepth > this.LayerDepth) { return 1; }
            if (gObj.LayerDepth == this.LayerDepth) { return 0; }

            return 0;
        }

        protected void SortObject()
        {
            this._drawableObjects.Sort();
        }
    }
}
