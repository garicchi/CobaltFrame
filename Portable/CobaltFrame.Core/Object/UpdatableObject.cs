using CobaltFrame.Core.Common;
using CobaltFrame.Core.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Core.Object
{
    public abstract class UpdatableObject : IUpdatable
    {
        public UpdatableObject()
        {
            this._isActive = true;
            this._loadState = ObjectLoadState.Created;
            this._gameObjects = new List<IUpdatable>();
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
        
        protected ObjectLoadState _loadState;
        public ObjectLoadState LoadState
        {
            get { return this._loadState; }
        }
        private List<IUpdatable> _gameObjects;
        public List<IUpdatable> GameObjects
        {
            get { return _gameObjects; }
        }


        public virtual void Init()
        {
            for (int i = 0; i < this._gameObjects.Count; i++)
            {
                this._gameObjects[i].Init();
            }
            this._loadState = ObjectLoadState.Initialized;
        }

        public virtual void Load()
        {
            for (int i = 0; i < this._gameObjects.Count; i++)
            {
                this._gameObjects[i].Load();
            }
            this._loadState = ObjectLoadState.Loaded;
        }

        public virtual void Unload()
        {
            for (int i = 0; i < this._gameObjects.Count; i++)
            {
                this._gameObjects[i].Unload();
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
