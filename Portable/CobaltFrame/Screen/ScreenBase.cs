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

        public virtual void Initialize(object navigationParameter)
        {
            //AddObjectで呼ぶ
        }

        public virtual void LoadScreen()
        {
            //AddObjectで呼ぶ
        }

        public virtual void UnloadScreen()
        {
            //RemoveObjectで呼ぶ
        }

        public virtual void Update(ScreenFrameContext frameContext)
        {
            foreach (GameObject obj in this._gameObjects)
            {
                obj.Update(new ObjectFrameContext(frameContext.GameTime));
            }
        }

        public virtual void Draw(ScreenFrameContext frameContext)
        {
            //レイヤー変更時のみソートする
            if (this.isObjectDrawDepthChanged)
                this._gameObjects.Sort();

            foreach (DrawableGameObject obj in this._gameObjects.Where(q=>q is DrawableGameObject))
            {
                if(obj.IsVisible)
                    obj.Draw(new ObjectFrameContext(frameContext.GameTime));
            }
            
        }

        protected void AddObject(GameObject gameObject)
        {
            gameObject.Initialize();
            gameObject.LoadObject();
            this._gameObjects.Add(gameObject);
        }

        protected void RemoveObject(GameObject gameObject)
        {
            if (this._gameObjects.Contains(gameObject))
            {
                gameObject.UnloadObject();
                this._gameObjects.Remove(gameObject);
            }
        }

        protected bool HasObject(GameObject gameObject)
        {
            return this._gameObjects.Contains(gameObject);
        }


        protected virtual void Navigate(ScreenBase screen,object parameter)
        {
            OnNavigate(screen,parameter);
        }

        /// <summary>
        /// GameObjectの描画レイヤーを変更する
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="depth"></param>
        public void ChangeObjectDrawDepth(GameObject obj,float depth)
        {
            //オブジェクトのレイヤー変更時にオブジェクトコレクションのソートを行うが
            //頻繁にソートを行えないのでレイヤ変更時のみソートのフラグをたててソート
            if (this._gameObjects.Contains(obj))
            {
                obj.SetDrawDepth(depth);
                this.isObjectDrawDepthChanged = true;
            }
        }


    }
}
