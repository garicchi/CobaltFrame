using CobaltFrame.Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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

        protected Game _game;
        protected SpriteBatch _spriteBatch;

        public bool IsInitialized { get; private set; }
        public bool IsLoaded { get; private set; }
        public bool IsUnLoaded { get; private set; }

        public float LayerDepth
        {
            get { return this._layerDepth; }
        }
        public GameObject(ObjectContext context)
        {
            this._objectContext = context;
            this._layerDepth = 0.0f;
            
            this._game = context.Game;
            this.IsInitialized = false;
            this.IsLoaded = false;
            this.IsUnLoaded = false;
        }
        public virtual void Initialize()
        {
            if (!this.IsInitialized) { this.IsInitialized = true; }
        }

        public virtual void LoadObject()
        {
            if (!this.IsLoaded) { this.IsLoaded = true; }
            this._spriteBatch = new SpriteBatch(this._game.GraphicsDevice);
        }

        public virtual void UnloadObject()
        {
            if (!this.IsUnLoaded) { this.IsUnLoaded = true; }
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
