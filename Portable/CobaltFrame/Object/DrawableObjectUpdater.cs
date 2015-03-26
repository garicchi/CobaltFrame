using CobaltFrame.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Object
{
    public abstract class DrawableObjectUpdater:ObjectUpdater,IDrawableGameObject,IComparable
    {
        protected bool isObjectDrawDepthChanged;

        protected float _layerDepth;

        public bool IsVisible { get; set; }

        public float LayerDepth
        {
            get { return this._layerDepth; }
        }

        public DrawableObjectUpdater()
        {
            this.isObjectDrawDepthChanged = false;
            this.IsVisible = true;
            
        }


        public virtual void Draw(Common.ObjectFrameContext frameContext)
        {
            //レイヤー変更時のみソートする
            if (this.isObjectDrawDepthChanged)
                this._gameObjects.Sort();

            foreach (DrawableObjectUpdater obj in this._gameObjects.Where(q => q is DrawableObjectUpdater))
            {
                if (obj.IsVisible)
                    obj.Draw(new ObjectFrameContext(frameContext.GameTime));
            }

            this.isObjectDrawDepthChanged = false;
        }

        /// <summary>
        /// GameObjectの描画レイヤーを変更する
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="depth"></param>
        public void ChangeObjectDrawDepth(DrawableObjectUpdater obj, float depth)
        {
            //オブジェクトのレイヤー変更時にオブジェクトコレクションのソートを行うが
            //頻繁にソートを行えないのでレイヤ変更時のみソートのフラグをたててソート
            if (this._gameObjects.Contains(obj))
            {
                obj.SetDrawDepth(depth);
                this.isObjectDrawDepthChanged = true;
            }
        }

        public int CompareTo(object obj)
        {
            var gObj = obj as DrawableObjectUpdater;
            if (gObj.LayerDepth < this.LayerDepth) { return -1; }
            if (gObj.LayerDepth > this.LayerDepth) { return 1; }
            if (gObj.LayerDepth == this.LayerDepth) { return 0; }

            return 0;
        }

        private void SetDrawDepth(float depth)
        {
            if (depth >= 0.0f && depth <= 1.0f)
            {
                this._layerDepth = depth;
            }
            else
            {
                throw new ArgumentOutOfRangeException("レイヤーの深度は0.0f～1.0fの範囲で指定してください");
            }
        }

        
    }
}
