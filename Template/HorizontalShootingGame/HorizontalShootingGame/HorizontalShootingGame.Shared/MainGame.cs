using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;
using System.Linq;
using CobaltFrame.Core.Screen;
using System.Diagnostics;
using Microsoft.Xna.Framework.Storage;
using CobaltFrame.Screen;
using CobaltFrame.Context;
using CobaltFrame.Input;
using HorizontalShootingGame.Portable.Data;
using HorizontalShootingGame.Portable.Screen;

namespace CobaltFrame
{
    public class MainGame : Game
    {
        //ゲーム全体を管理するクラス
        GameManager _gameManager;

        public MainGame()
        {
            Content.RootDirectory = "Content";

            this._gameManager = new GameManager(this, new Point(1360, 768), this.Window.ClientBounds, ScaleMode.Fill);
            //アプリが有効になったときにセーブデータをロード
            GameContext.Game.Activated += (s, e) =>
            {
                DataContext<SaveData>.Load(new SaveData());
            };
            //アプリが無効になったときにセーブデータをセーブ
            GameContext.Game.Deactivated += (s, e) =>
            {
                DataContext<SaveData>.Save();
            };

            //ウインドウサイズの変更を通知
            this.Window.ClientSizeChanged += (s, e) =>
            {
                this._gameManager.WindowSizeChanged(this.Window.ClientBounds);
            };

            //DataContextをセットアップ
            DataContext<SaveData>.Setup("__savedata", (name) =>
            {
                //データロード時
                SaveData data = null;

                var deviceResult = StorageDevice.BeginShowSelector(null, null);
                deviceResult.AsyncWaitHandle.WaitOne();
                var device = StorageDevice.EndShowSelector(deviceResult);
                var containerResult = device.BeginOpenContainer(name, null, null);
                containerResult.AsyncWaitHandle.WaitOne();
                using (var container = device.EndOpenContainer(containerResult))
                {

                    if (container.FileExists(name))
                    {
                        using (var stream = container.OpenFile(name, FileMode.Open))
                        {
                            var serializer = new XmlSerializer(typeof(SaveData));
                            data = (SaveData)serializer.Deserialize(stream);
                        }

                    }
                }

                return data;

            }, (name, data) =>
            {
                //データセーブ時
                try
                {
                    var deviceResult = StorageDevice.BeginShowSelector(null, null);
                    deviceResult.AsyncWaitHandle.WaitOne();
                    var device = StorageDevice.EndShowSelector(deviceResult);
                    var containerResult = device.BeginOpenContainer(name, null, null);
                    containerResult.AsyncWaitHandle.WaitOne();
                    using (var container = device.EndOpenContainer(containerResult))
                    {
                        if (container.FileExists(name))
                            container.DeleteFile(name);

                        using (var stream = container.CreateFile(name))
                        {
                            var serializer = new XmlSerializer(typeof(SaveData));
                            serializer.Serialize(stream, data);

                        }
                    }

                    return true;
                }
                catch (Exception)
                {
                    return false;
                }


            });

            /*
			//加速度センサーを使う場合はここでAPIを呼ぶ
            InputContext.SetupAccelState(() =>
            {

                return new AccelerometerState(new Vector3(1,0,0));
            });
            */

            //最初の画面に遷移
            this._gameManager.ChangeScreen(new TitleScreen(), null, null);
        }



        protected override void Initialize()
        {
            this._gameManager.Init();
            base.Initialize();


        }

        protected override void LoadContent()
        {
            this._gameManager.Load();
            base.LoadContent();

        }

        protected override void UnloadContent()
        {
            this._gameManager.Unload();
            base.UnloadContent();

        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            this._gameManager.Update(new FrameContext(gameTime));
        }

        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            this._gameManager.Draw(new FrameContext(gameTime));
        }


    }
}
