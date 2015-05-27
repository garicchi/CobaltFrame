using CobaltFrame.Common;
using CobaltFrame.Context;
using CobaltFrame.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Object
{
    public abstract class GameObject:IGameObject,IComparable
    {
        public GameObject()
        {
            this._isActive = true;
            this._loadState = ObjectLoadState.Created;
            
            this._isVisible = true;
            this._layerDepth = 0.5f;
            this._isObjectLayerChanged = false;
            this._children = new List<IGameObject>();
            this._inputs = new GameInputCollection();
            this._rect = new Rectangle(0,0,100,100);

            this.OnPositionChanged += (pos) => { };
            this.OnSizeChanged += (size) => { };
        }

        #region Event

        public event Action<Point> OnPositionChanged;

        public event Action<Point> OnSizeChanged;
        #endregion

        #region Field

        protected ObjectLoadState _loadState;
        protected bool _isActive;
        protected float _layerDepth;
        protected bool _isVisible;
        protected Rectangle _rect;
        protected Game _game;
        protected IGameObject _parent;

        private bool _isObjectLayerChanged;
        private List<IGameObject> _children;
        private GameInputCollection _inputs;

        #endregion

        #region Property

        public ObjectLoadState LoadState
        {
            get { return this._loadState; }
        }

        public bool IsActive
        {
            get
            {
                return this._isActive;
            }
            set
            {
                this._isActive = value;
            }
        }

        public bool IsVisible
        {
            get
            {
                return this._isVisible;
            }
            set
            {
                this._isVisible = value;
            }
        }

        public float LayerDepth
        {
            get
            {
                return this._layerDepth;
            }
            set
            {
                if (value <= 1.0f && value >= 0.0f)
                {
                    this._layerDepth = value;
                    //自分のレイヤーに変更があったとき、親要素にソートしてもらうために
                    //フラグを立てる
                    this._isObjectLayerChanged = true;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("LayerDepth","0.0f~1.0f");
                }
            }
        }

        public List<IGameObject> Children
        {
            get { return this._children; }
        }

        public bool IsObjectLayerChanged
        {
            get { return this._isObjectLayerChanged; }
            set { this._isObjectLayerChanged = value; }
        }

        public Rectangle Rect
        {
            get
            {
                return this._rect;
            }
            
        }

        public GameInputCollection Inputs
        {
            get { return _inputs; }
            set { _inputs = value; }
        }

        public IGameObject Parent
        {
            get
            {
                return this._parent;
            }
            set
            {
                this._parent = value;
            }
        }

        #endregion

        #region Method

        public virtual void Init()
        {
            this._game = GameContext.Game;
            
            //すべての子要素を初期化
            for (int i = 0; i < this._children.Count; i++)
            {
                this._children.ElementAt(i).Init();
            }

            this._loadState = ObjectLoadState.Initialized;
        }

        public virtual void Load()
        {
            //すべての子要素をロード
            for (int i = 0; i < this._children.Count; i++)
            {
                this._children.ElementAt(i).Load();
            }

            this._loadState = ObjectLoadState.Loaded;
        }

        public virtual void Unload()
        {
            //すべての子要素をアンロード
            for (int i = 0; i < this._children.Count; i++)
            {
                this._children.ElementAt(i).Unload();
            }

            this._loadState = ObjectLoadState.Unloaded;

            //すべての入力を解除
            this._inputs.UnregisterAllInput();
        }

        public virtual void Update(Context.FrameContext context)
        {
            //入力をアップデート
            this._inputs.Update();

            //子要素の数を保持しておく
            int beforeObjectCount = this._children.Count;

            for (int i = 0; i < this._children.Count; i++)
            {
                //アクティブで子要素のロードが完了しているならアップデート
                if (this._isActive && this._children.ElementAt(i).LoadState >= ObjectLoadState.Loaded)
                    this._children.ElementAt(i).Update(context);

                //途中で子要素の数が変わったら例外が起こる可能性があるためbreak
                if (beforeObjectCount != this._children.Count)
                {
                    break;
                }
            }
        }

        public virtual void Draw(Context.FrameContext context)
        {
            //もし子要素に一つでもレイヤー変更があったなら
            if (this._children.Any(q => q.IsObjectLayerChanged))
            {
                //子要素をソート
                this._children.Sort();

                //ソートが完了したら子要素のフラグを戻す
                for (int i = 0; i < this._children.Count; i++)
                {
                    this._children.ElementAt(i).IsObjectLayerChanged = false;
                }
            }

            //子要素の数を記録しておく
            int beforeObjectCount = this._children.Count;
            for (int i = 0; i < this._children.Count; i++)
            {
                //Visibleかつロードされているなら
                if (this._children.ElementAt(i).IsVisible
                    && this._children.ElementAt(i).LoadState >= ObjectLoadState.Loaded
                    && this._children.ElementAt(i).LoadState < ObjectLoadState.Unloaded
                    )
                {
                    //子要素を描画
                    this._children.ElementAt(i).Draw(context);
                }

                //もし子要素の数に変更があれば例外を避けるためにbreak
                if (beforeObjectCount != this._children.Count)
                {
                    break;
                }
            }
            
        }

        public void AddChild(IGameObject child)
        {
            //子要素に自身への参照を渡す
            child.Parent = this;

            //自分が初期化後なら
            if (this._loadState >= ObjectLoadState.Initialized)
            {
                //子要素も初期化
                child.Init();
            }
            //自分がロード後なら
            if (this._loadState >= ObjectLoadState.Loaded)
            {
                //子要素をロード
                child.Load();
            }
            //子要素を追加
            this._children.Add(child);
        }

        public void RemoveChild(IGameObject child)
        {
            //子要素に含まれているなら
            if (this._children.Contains(child))
            {
                //リソースを解放
                child.Unload();
                //子要素を削除
                this._children.Remove(child);
            }
        }

        /// <summary>
        /// レイヤーソートをするための比較関数
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            //LayerDepthの数が大きくなるほど後に描画されるようにする
            var gObj = (IGameObject)obj;
            if (gObj.LayerDepth < this.LayerDepth) { return -1; }
            if (gObj.LayerDepth > this.LayerDepth) { return 1; }
            if (gObj.LayerDepth == this.LayerDepth) { return 0; }

            return 0;
        }

        public virtual void SetPosition(Point pos)
        {
            this._rect.X = pos.X;
            this._rect.Y = pos.Y;

            this.OnPositionChanged(pos);
        }

        public virtual void SetSize(Point size)
        {
            this._rect.Width = size.X;
            this._rect.Height = size.Y;

            this.OnSizeChanged(size);
        }

        public void SetRect(Rectangle rect)
        {
            this.SetPosition(rect.GetPosition());
            this.SetSize(rect.Size);
        }

        public Rectangle GetAbsoluteRect()
        {
            if(this._parent==null)
            {
                //親要素がないならそのまま座標を返す
                return this._rect;
            }
            else
            {
                //親要素があるなら親要素の座標と足して絶対座標を作る
                return new Rectangle(
                    this._parent.GetAbsoluteRect().X+this._rect.X,
                    this._parent.GetAbsoluteRect().Y+this._rect.Y,
                    this._rect.Width,
                    this._rect.Height
                    );
            }
        }

        #endregion


    }
}
