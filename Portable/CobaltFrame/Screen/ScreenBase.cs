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

        private List<GameObject> _gameObjects;

        public List<GameObject> GameObjects
        {
            get { return this._gameObjects; }
        }

        private bool isObjectDrawDepthChanged;

        public event Action<ScreenBase,object> OnNavigate;

        public ScreenBase(ScreenContext screenContext)
        {
            this._screenContext = screenContext;
            this._gameObjects = new List<GameObject>();
            this.isObjectDrawDepthChanged = false;
        }

        public void Initialize(object navigationParameter)
        {
            //AddObjectで呼ぶ
        }

        public void LoadScreen()
        {
            //AddObjectで呼ぶ
        }

        public void UnloadScreen()
        {
            //RemoveObjectで呼ぶ
        }

        public void Update(ScreenFrameContext frameContext)
        {
            foreach (GameObject obj in this._gameObjects)
            {
                obj.Update(new ObjectFrameContext(frameContext.GameTime));
            }
        }

        public void Draw(ScreenFrameContext frameContext)
        {
            foreach (GameObject obj in this._gameObjects)
            {
                if(obj.IsVisible)
                obj.Draw(new ObjectFrameContext(frameContext.GameTime));
            }
            this.isObjectDrawDepthChanged = false;
        }

        public void AddObject(GameObject gameObject)
        {
            gameObject.Initialize();
            gameObject.LoadObject();
            this._gameObjects.Add(gameObject);
        }

        public void RemoveObject(GameObject gameObject)
        {
            if (this._gameObjects.Contains(gameObject))
            {
                gameObject.UnloadObject();
                this._gameObjects.Remove(gameObject);
            }
        }

        public bool HasObject(GameObject gameObject)
        {
            return this._gameObjects.Contains(gameObject);
        }


        protected virtual void Navigate(ScreenBase screen,object parameter)
        {
            OnNavigate(screen,parameter);
        }

        public void ChangeObjectDrawDepth(GameObject obj,float depth)
        {
            if (this._gameObjects.Contains(obj))
            {
                obj.SetDrawDepth(depth);
                this._gameObjects.Sort();
                this.isObjectDrawDepthChanged = true;
            }
        }
    }
}
