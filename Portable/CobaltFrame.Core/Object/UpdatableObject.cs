using CobaltFrame.Core.Common;
using CobaltFrame.Core.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Core.Object
{
	/// <summary>
	/// 非描画オブジェクト
	/// </summary>
    public abstract class UpdatableObject : IUpdatable
    {
        public UpdatableObject()
        {
            this._isActive = true;
            this._loadState = ObjectLoadState.Created;
            this._gameObjects = new List<IUpdatable>();
        }
        protected bool _isActive;

		/// <summary>
		/// 更新するかどうか
		/// </summary>
		/// <value>true</value>
		/// <c>false</c>
        public bool IsActive
        {
            get
            {
                return _isActive;
            }
            set
            {
                this._isActive = value;
            }
        }
        
        protected ObjectLoadState _loadState;

		/// <summary>
		/// どこまでロードされたか
		/// </summary>
		/// <value>The state of the load.</value>
        public ObjectLoadState LoadState
        {
            get { return this._loadState; }
        }
        private List<IUpdatable> _gameObjects;

		/// <summary>
		/// このObjectにぶら下がる子オブジェクトのリスト
		/// 描画はされない
		/// </summary>
		/// <value>The game objects.</value>
        public List<IUpdatable> GameObjects
        {
            get { return _gameObjects; }
        }

		/// <summary>
		/// 初期化関数
		/// </summary>
        public virtual void Init()
        {
            for (int i = 0; i < this._gameObjects.Count; i++)
            {
                this._gameObjects[i].Init();
            }
            this._loadState = ObjectLoadState.Initialized;
        }

		/// <summary>
		/// リソース確保関数
		/// </summary>
        public virtual void Load()
        {
            for (int i = 0; i < this._gameObjects.Count; i++)
            {
                this._gameObjects[i].Load();
            }
            this._loadState = ObjectLoadState.Loaded;
        }

		/// <summary>
		/// リソース解放関数
		/// </summary>
        public virtual void Unload()
        {
            for (int i = 0; i < this._gameObjects.Count; i++)
            {
                this._gameObjects[i].Unload();
            }
            this._loadState = ObjectLoadState.Unloaded;
        }

		/// <summary>
		/// 更新関数
		/// </summary>
		/// <param name="context">Context.</param>
        public virtual void Update(IFrameContext context)
        {
            int beforeObjectCount = this._gameObjects.Count;
            for (int i = 0; i < this._gameObjects.Count; i++)
            {
                if (this._isActive && this._gameObjects[i].LoadState >= ObjectLoadState.Loaded)
                    this._gameObjects[i].Update(context);

                if (beforeObjectCount != this._gameObjects.Count)
                {
                    break;
                }
            }
        }

		/// <summary>
		/// 非描画子要素追加
		/// </summary>
		/// <param name="obj">Object.</param>
        protected void AddObject(IUpdatable obj)
        {
            if (this._loadState >= ObjectLoadState.Initialized)
            {
                obj.Init();
            }
            if (this._loadState >= ObjectLoadState.Loaded)
            {
                obj.Load();
            }
            this._gameObjects.Add(obj);
        }

		/// <summary>
		/// 非描画子要素削除
		/// </summary>
		/// <param name="obj">Object.</param>
        protected void RemoveObject(IUpdatable obj)
        {
            if (this._gameObjects.Contains(obj))
            {
                obj.Unload();

                this._gameObjects.Remove(obj);
            }
        }

        

    }
}
