using CobaltFrame.Core.Data;
using CobaltFrame.Mono.Context;
using CobaltFrame.Mono.Screen;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Storage;
using SimpleGame.Portable.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using CobaltFrame.Core.Screen;
using System.IO;
using SimpleGame.Portable.Screen;

namespace SimpleGame
{
    public class MainGame : Game
    {
        GameManager _gameManager;

        public MainGame()
        {
            Content.RootDirectory = "Content";

            this._gameManager = new GameManager(this, new TitleScreen(), new Vector2(1360, 768), ScaleMode.Fill);
            GameContext.GraphicsManager.IsFullScreen = true;

            GameContext.Game.Activated += (s, e) =>
            {
                SaveDataStore<SaveData>.Load(new SaveData());
            };
            GameContext.Game.Deactivated += (s, e) =>
            {
                SaveDataStore<SaveData>.Save();
            };

            SaveDataStore<SaveData>.Setup("__savedata", (name) =>
            {
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
            GameInput.SetupAccelState(() =>
            {

                return new AccelerometerState(new Vector3(1,0,0));
            });
            */
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
