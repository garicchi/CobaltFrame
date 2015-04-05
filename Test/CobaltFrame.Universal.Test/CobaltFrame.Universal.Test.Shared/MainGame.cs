using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using CobaltFrame;
using CobaltFrame.Screen;
using CobaltFrame.Test.Screen;
using CobaltFrame.Context;
using CobaltFrame.Core.Screen;
using CobaltFrame.Test.Data;
using CobaltFrame.Mono.Data;
using Windows.Storage;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Windows.Storage.Streams;
using System.IO;
using System.Linq;

namespace CobaltFrame.Universal.Test
{
    class MainGame:Game
    {
        GraphicsDeviceManager graphics;
        GameScreenManager _screenManager;
        GameContext _gameContext;
        public MainGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this._gameContext = new GameContext(this);
            this._screenManager = new GameScreenManager(this._gameContext, new SampleScreen(this._gameContext), null,new Vector2(1366,768),ScaleMode.None);
            
            SaveDataFactory<SaveData>.Setup("savedata", (name) =>
            {
                SaveData data=null;
                var task=Task.Run(async () =>
                {
                    StorageFolder folder = ApplicationData.Current.LocalFolder;
                    var files = await folder.GetFilesAsync();
                    if (files.Any(q => q.Name == name))
                    {
                        StorageFile file = await folder.GetFileAsync(name);
                        var serializer = new XmlSerializer(typeof(SaveData));
                        
                        using (var stream = await file.OpenAsync(FileAccessMode.Read))
                        {
                            data = (SaveData)serializer.Deserialize(stream.AsStream());

                        }
                        
                    }
                    
                });
                task.Wait();
                return data;
                
            }, (name, data) =>
            {
                try
                {
                   var task= Task.Run(async () =>
                    {
                        StorageFolder folder = ApplicationData.Current.LocalFolder;
                        StorageFile file = await folder.CreateFileAsync(name, CreationCollisionOption.ReplaceExisting);
                        var serializer = new XmlSerializer(typeof(SaveData));
                        using (var stream = await file.OpenAsync(FileAccessMode.ReadWrite))
                        {
                            serializer.Serialize(stream.AsStream(), data);
                        }

                    });
                   task.Wait();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
                
                
            });
        }

        protected override void Initialize()
        {
            base.Initialize();
            
            this._screenManager.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            this._screenManager.LoadObject();
        }

        protected override void UnloadContent()
        {
            base.UnloadContent();
            this._screenManager.UnloadObject();
        }
        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            this._screenManager.Update(new FrameContext(gameTime));
        }

        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            this._screenManager.Draw(new FrameContext(gameTime));
        }
        

        
    }
}
