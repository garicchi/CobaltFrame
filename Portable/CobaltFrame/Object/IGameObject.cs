﻿using CobaltFrame.Common;
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
    public interface IGameObject
    {
        /// <summary>
        /// Positionが変更されたとき
        /// </summary>
        event Action<Point> OnPositionChanged;

        /// <summary>
        /// Sizeが変更されたとき
        /// </summary>
        event Action<Point> OnSizeChanged;

        /// <summary>
        /// どこまでロードされたか
        /// </summary>
        /// <value>The state of the load.</value>
        ObjectLoadState LoadState { get; }

        /// <summary>
        /// 更新するかどうか
        /// </summary>
        /// <value><c>true</c> if this instance is active; otherwise, <c>false</c>.</value>
        bool IsActive { get; set; }

        /// <summary>
        /// 表示するかどうか
        /// </summary>
        /// <value><c>true</c> if this instance is visible; otherwise, <c>false</c>.</value>
        bool IsVisible { get; set; }
        /// <summary>
        /// レイヤーの深度
        /// </summary>
        /// <value>The layer depth.</value>
        float LayerDepth { get; set; }

        /// <summary>
        /// オブジェクトのレイヤーが変更されたかどうか
        /// </summary>
        bool IsObjectLayerChanged { get; set; }

        /// <summary>
        /// 子要素
        /// </summary>
        List<IGameObject> Children { get; }

        /// <summary>
        /// 描画矩形
        /// </summary>
        Rectangle Rect { get; }

        /// <summary>
        /// 入力条件のリスト
        /// </summary>
        GameInputCollection Inputs { get; set; }

        /// <summary>
        /// 親要素への参照
        /// </summary>
        IGameObject Parent { get; set; }

        /// <summary>
        /// 初期化関数
        /// </summary>
        void Init();

        /// <summary>
        /// リソース確保関数
        /// </summary>
        void Load();

        /// <summary>
        /// リソース解放関数
        /// </summary>
        void Unload();

        /// <summary>
        /// 更新関数
        /// </summary>
        /// <param name="context">Context.</param>
        void Update(FrameContext context);

        
        /// <summary>
        /// 描画関数
        /// </summary>
        /// <param name="context">Context.</param>
        void Draw(FrameContext context);

        

        /// <summary>
        /// 子要素を追加する
        /// </summary>
        /// <param name="child"></param>
        void AddChild(IGameObject child);

        /// <summary>
        /// 子要素を削除する
        /// </summary>
        /// <param name="child"></param>
        void RemoveChild(IGameObject child);

        
        /// <summary>
        /// 座標設定
        /// </summary>
        /// <param name="pos"></param>
        void SetPosition(Point pos);

        /// <summary>
        /// WidthとHeight指定
        /// </summary>
        /// <param name="size"></param>
        void SetSize(Point size);

        /// <summary>
        /// 描画矩形を指定
        /// </summary>
        /// <param name="rect"></param>
        void SetRect(Rectangle rect);

        /// <summary>
        /// 絶対座標なRectを取得
        /// 子要素は親要素からの相対座標となる
        /// </summary>
        /// <returns></returns>
        Rectangle GetAbsoluteRect();

        


    }
}
