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
	/// <summary>
	/// 描画ゲームオブジェクト
	/// </summary>
    public abstract class DrawableObject : UpdatableObject, IDrawable, IComparable
    {
        public DrawableObject()
            : base()
        {
            this._isVisible = true;
            this._layerDepth = 0.5f;
            this._drawableObjects = new List<IDrawable>();
            this._isObjectLayerChanged = false;
        }
        protected bool _isVisible;
		/// <summary>
		/// 描画するかどうか
		/// </summary>
		/// <value><c>true</c> if this instance is visible; otherwise, <c>false</c>.</value>
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
		/// <summary>
		/// 描画オブジェクトの子オブジェクト
		/// </summary>
		/// <value>The drawable objects.</value>
        public List<IDrawable> DrawableObjects
        {
            get { return _drawableObjects; }
        }

        protected float _layerDepth;
		/// <summary>
		/// 描画レイヤー(数値が大きいほど深度が深くなる)
		/// </summary>
		/// <value>The layer depth(0.0〜1.0)</value>
        public float LayerDepth
        {
            get { return this._layerDepth; }
            set { this._layerDepth = value; _isObjectLayerChanged = true; }
        }

        private bool _isObjectLayerChanged;
		/// <summary>
		/// オブジェクトのレイヤが変更されたかどうか
		/// </summary>
		/// <value><c>true</c> if this instance is object layer changed; otherwise, <c>false</c>.</value>
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

        
		/// <summary>
		/// オブジェクトのソートレイヤ比較関数
		/// </summary>
		/// <returns>The to.</returns>
		/// <param name="obj">Object.</param>
        public int CompareTo(object obj)
        {
            var gObj = (IDrawable)obj;
            if (gObj.LayerDepth < this.LayerDepth) { return -1; }
            if (gObj.LayerDepth > this.LayerDepth) { return 1; }
            if (gObj.LayerDepth == this.LayerDepth) { return 0; }

            return 0;
        }

		/// <summary>
		/// 子要素のオブジェクトのレイヤーをソートする
		/// </summary>
        protected void SortObject()
        {

            this._drawableObjects.Sort();

        }

		/// <summary>
		/// 子要素の描画オブジェクトを追加する
		/// </summary>
		/// <param name="obj">追加するオブジェクト</param>
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

		/// <summary>
		/// 子要素の描画オブジェクトを削除する
		/// </summary>
		/// <param name="obj">削除するオブジェクト</param>
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
