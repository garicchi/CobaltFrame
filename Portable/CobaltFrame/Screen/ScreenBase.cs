using CobaltFrame.Common;
using CobaltFrame.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Screen
{
    public abstract class ScreenBase:IScreen
    {
        protected ScreenContext _screenContext;

        protected IList<GameObject> _gameObjects;

        public ScreenBase(ScreenContext screenContext)
        {
            this._screenContext = screenContext;
            this._gameObjects = new List<GameObject>();
        }

        public void Initialize()
        {
           
        }

        public void LoadScreen()
        {
            
        }

        public void UnloadScreen()
        {
            
        }

        public IScreen Update(ScreenFrameContext frameContext)
        {
            return this;
        }

        public void Draw(ScreenFrameContext frameContext)
        {
           
        }


        public void AddObject(GameObject gameObject)
        {
            this._gameObjects.Add(gameObject);
        }

        public void RemoveObject(GameObject gameObject)
        {
            if (this._gameObjects.Contains(gameObject))
            {
                this._gameObjects.Remove(gameObject);
            }
        }

        public bool HasObject(GameObject gameObject)
        {
            return this._gameObjects.Contains(gameObject);
        }
    }
}
