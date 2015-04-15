using CobaltFrame.Core.Common;
using CobaltFrame.Core.Context;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Core.Object
{
    public abstract class DrawableObject : UpdatableObject, IDrawable, IComparable
    {
        public DrawableObject(IGameContext context)
            : base(context)
        {
            this._isVisible = true;
            this._layerDepth = 0.5f;
            this._drawableObjects = new List<IDrawable>();
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

        private List<IDrawable> _drawableObjects;
        public List<IDrawable> DrawableObjects
        {
            get { return _drawableObjects; }
        }

        protected float _layerDepth;
        public float LayerDepth
        {
            get { return this._layerDepth; }
            set { this._layerDepth = value; _isObjectLayerChanged = true; }
        }

        private bool _isObjectLayerChanged;

        public bool IsObjectLayerChanged
        {
            get { return _isObjectLayerChanged; }
            
        }

        public override void Init()
        {
            for (int i = 0; i < this._drawableObjects.Count; i++)
            {
                this._drawableObjects[i].Init();
            }
            base.Init();

        }

        public override void Load()
        {
            for (int i = 0; i < this._drawableObjects.Count; i++)
            {
                this._drawableObjects[i].Load();
            }
            base.Load();
        }

        public override void Unload()
        {
            for (int i = 0; i < this._drawableObjects.Count; i++)
            {
                this._drawableObjects[i].Unload();
            }
            base.Unload();
        }

        public override void Update(IFrameContext context)
        {
            int beforeObjectCount = this._drawableObjects.Count;
            for (int i = 0; i < this._drawableObjects.Count; i++)
            {

                if (this._drawableObjects[i].IsActive
                    && this._drawableObjects[i].LoadState >= ObjectLoadState.Loaded
                     && this._drawableObjects[i].LoadState < ObjectLoadState.Unloaded
                    )
                {
                    this._drawableObjects[i].Update(context);
                }
                if (beforeObjectCount != this._drawableObjects.Count)
                {
                    break;
                }
            }
            base.Update(context);
        }

        public virtual void Draw(IFrameContext context)
        {
            if (this._drawableObjects.Any(q => q.IsObjectLayerChanged))
            {
                this.SortObject();
            }

            int beforeObjectCount = this._drawableObjects.Count;
            for (int i = 0; i < this._drawableObjects.Count; i++)
            {
                if (this._drawableObjects[i].IsVisible
                    && this._drawableObjects[i].LoadState >= ObjectLoadState.Loaded
                    && this._drawableObjects[i].LoadState < ObjectLoadState.Unloaded
                    )
                {
                    this._drawableObjects[i].Draw(context);
                }
                

                if (beforeObjectCount != this._drawableObjects.Count)
                {
                    break;
                }
            }

            this._isObjectLayerChanged = false;
        }

        

        public int CompareTo(object obj)
        {
            var gObj = (IDrawable)obj;
            if (gObj.LayerDepth < this.LayerDepth) { return -1; }
            if (gObj.LayerDepth > this.LayerDepth) { return 1; }
            if (gObj.LayerDepth == this.LayerDepth) { return 0; }

            return 0;
        }

        protected void SortObject()
        {

            this._drawableObjects.Sort();

        }

        protected void AddDrawableObject(IDrawable obj)
        {
            if (this._loadState >= ObjectLoadState.Initialized)
            {
                obj.Init();
            }
            if (this._loadState >= ObjectLoadState.Loaded)
            {
                obj.Load();
            }
            this._drawableObjects.Add(obj);
            
        }

        protected void RemoveDrawableObject(IDrawable obj)
        {
            if (this._drawableObjects.Contains(obj))
            {
                obj.Unload();
                this._drawableObjects.Remove(obj);
            }
        }


        

        

    }
}
