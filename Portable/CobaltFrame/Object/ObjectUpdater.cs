using CobaltFrame.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Object
{
    public abstract class ObjectUpdater:IObjectUpdater,IGameObject
    {
        protected ComponentState _componentState;

        protected List<ObjectUpdater> _gameObjects;

        public List<ObjectUpdater> GameObjects
        {
            get { return this._gameObjects; }
        }

        public ObjectUpdater()
        {
            this._componentState = ComponentState.Non;
            this._gameObjects = new List<ObjectUpdater>();
            this._componentState = ComponentState.ConstractorCalled;
        }

        public void AddObject(ObjectUpdater obj)
        {
            if (this._componentState >= ComponentState.Initialized)
            {
                obj.Initialize();
            }
            if (this._componentState >= ComponentState.Loaded)
            {
                obj.LoadObject();
            }

            this._gameObjects.Add(obj);
        }

        public void RemoveObject(ObjectUpdater obj)
        {
            if (this._gameObjects.Contains(obj))
            {
                if (this._componentState >= ComponentState.Unloaded)
                {
                    obj.UnloadObject();
                }

                this._gameObjects.Remove(obj);
            }
        }

        public bool HasObject(ObjectUpdater obj)
        {
            return this._gameObjects.Contains(obj);
        }

        public virtual void Initialize()
        {
            foreach (var obj in this._gameObjects)
            {
                obj.Initialize();
            }
            this._componentState = ComponentState.Initialized;
        }

        public virtual void LoadObject()
        {
            foreach (var obj in this._gameObjects)
            {
                obj.LoadObject();
            }

            this._componentState = ComponentState.Loaded;
        }

        public virtual void UnloadObject()
        {
            foreach (var obj in this._gameObjects)
            {
                obj.UnloadObject();
            }
            this._componentState = ComponentState.Unloaded;
        }

        public virtual void Update(ObjectFrameContext frameContext)
        {
            foreach (var obj in this._gameObjects)
            {
                obj.Update(new ObjectFrameContext(frameContext.GameTime));
            }
        }
    }
}
