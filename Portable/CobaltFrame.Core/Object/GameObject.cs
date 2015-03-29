using CobaltFrame.Core.Common;
using CobaltFrame.Core.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Core.Object
{
    public abstract class GameObject:IGameObject
    {
        public GameObject(IGameContext context)
        {
            this._gameContext = context;
            this._isActive = true;
            this._loadState = ObjectLoadState.Created;
            this._gameObjects = new List<IGameObject>();
        }
        protected bool _isActive;
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
        protected IGameContext _gameContext;
        public IGameContext GameContext
        {
            get { return this._gameContext; }
        }
        protected ObjectLoadState _loadState;
        public ObjectLoadState LoadState
        {
            get { return this._loadState; }
        }
        private List<IGameObject> _gameObjects;
        public List<IGameObject> GameObjects
        {
            get { return _gameObjects; }
        }

        
        public virtual void Initialize()
        {
            foreach (var obj in this._gameObjects)
            {
                obj.Initialize();
            }
            this._loadState = ObjectLoadState.Initialized;
        }

        public virtual void LoadObject()
        {
            foreach (var obj in this._gameObjects)
            {
                obj.LoadObject();
            }
            this._loadState = ObjectLoadState.Loaded;
        }

        public virtual void UnloadObject()
        {
            foreach (var obj in this._gameObjects)
            {
                obj.UnloadObject();
            }
            this._loadState = ObjectLoadState.Unloaded;
        }

        public virtual void Update(IFrameContext context)
        {
            foreach (var obj in this._gameObjects)
            {
                if (this._isActive)
                    obj.Update(context);
            }
        }

        protected void AddObject(IGameObject obj)
        {
            if (this._loadState >= ObjectLoadState.Initialized)
            {
                obj.Initialize();
            }
            if (this._loadState >= ObjectLoadState.Loaded)
            {
                obj.LoadObject();
            }
        }

        protected void RemoveObject(IGameObject obj)
        {
            if (this._gameObjects.Contains(obj))
            {
                if (this._loadState >= ObjectLoadState.Unloaded)
                {
                    obj.UnloadObject();
                }
                this._gameObjects.Remove(obj);
            }
        }


    }
}
