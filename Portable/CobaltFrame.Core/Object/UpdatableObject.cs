using CobaltFrame.Core.Common;
using CobaltFrame.Core.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Core.Object
{
    public abstract class UpdatableObject : IUpdatableObject
    {
        public UpdatableObject(IGameContext context)
        {
            this._gameContext = context;
            this._isActive = true;
            this._loadState = ObjectLoadState.Created;
            this._gameObjects = new List<IUpdatableObject>();
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
        private List<IUpdatableObject> _gameObjects;
        public List<IUpdatableObject> GameObjects
        {
            get { return _gameObjects; }
        }


        public virtual void Initialize()
        {
            for (int i = 0; i < this._gameObjects.Count; i++)
            {
                this._gameObjects[i].Initialize();
            }
            this._loadState = ObjectLoadState.Initialized;
        }

        public virtual void LoadObject()
        {
            for (int i = 0; i < this._gameObjects.Count; i++)
            {
                this._gameObjects[i].LoadObject();
            }
            this._loadState = ObjectLoadState.Loaded;
        }

        public virtual void UnloadObject()
        {
            for (int i = 0; i < this._gameObjects.Count; i++)
            {
                this._gameObjects[i].UnloadObject();
            }
            this._loadState = ObjectLoadState.Unloaded;
        }

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

        protected void AddObject(IUpdatableObject obj)
        {
            if (this._loadState >= ObjectLoadState.Initialized)
            {
                obj.Initialize();
            }
            if (this._loadState >= ObjectLoadState.Loaded)
            {
                obj.LoadObject();
            }
            this._gameObjects.Add(obj);
        }

        protected void RemoveObject(IUpdatableObject obj)
        {
            if (this._gameObjects.Contains(obj))
            {
                obj.UnloadObject();

                this._gameObjects.Remove(obj);
            }
        }


    }
}
