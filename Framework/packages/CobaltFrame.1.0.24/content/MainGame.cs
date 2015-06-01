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

#if WINDOWS_PHONE_APP||WINDOWS_APP
using Windows.Storage;
using Windows.Storage.Streams;
#endif

namespace CobaltFrame
{
    public class MainGame : Game
    {
        //�Q�[���S�̂��Ǘ�����N���X
        GameManager _gameManager;

        public MainGame()
        {
            Content.RootDirectory = "Content";

            this._gameManager = new GameManager(this, new Point(1360, 768), this.Window.ClientBounds, ScaleMode.Fill);
            //�A�v�����L���ɂȂ����Ƃ��ɃZ�[�u�f�[�^�����[�h
            GameContext.Game.Activated += (s, e) =>
            {
                DataContext<SaveData>.Load(new SaveData());
            };
            //�A�v���������ɂȂ����Ƃ��ɃZ�[�u�f�[�^���Z�[�u
            GameContext.Game.Deactivated += (s, e) =>
            {
                DataContext<SaveData>.Save();
            };

            //�E�C���h�E�T�C�Y�̕ύX��ʒm
            this.Window.ClientSizeChanged += (s, e) =>
            {
                this._gameManager.WindowSizeChanged(this.Window.ClientBounds);
            };

            //DataContext���Z�b�g�A�b�v
            DataContext<SaveData>.Setup("__savedata", (name) =>
            {
                //�f�[�^���[�h��
                SaveData data = null;

#if WINDOWS_PHONE_APP||WINDOWS_APP
                var folder = ApplicationData.Current.LocalFolder;
                var files = folder.GetFilesAsync().AsTask<IReadOnlyList<StorageFile>>().Result;
                if (files.Any(q => q.Name == name))
                {
                    using (var stream = files.First(q => q.Name == name).OpenReadAsync().AsTask<IRandomAccessStreamWithContentType>().Result.AsStream())
                    {
                        var serializer = new XmlSerializer(typeof(SaveData));
                        data = (SaveData)serializer.Deserialize(stream);
                    }
                }
                
#else

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
#endif
                return data;

            }, (name, data) =>
            {
                //�f�[�^�Z�[�u��
                try
                {
#if WINDOWS_PHONE_APP||WINDOWS_APP
                    var folder = ApplicationData.Current.LocalFolder;

                    var file = folder.CreateFileAsync(name,CreationCollisionOption.ReplaceExisting).AsTask<StorageFile>().Result;
                    using (var stream = file.OpenAsync(FileAccessMode.ReadWrite).AsTask<IRandomAccessStream>().Result.AsStream())
                    {
                        var serializer = new XmlSerializer(typeof(SaveData));
                        serializer.Serialize(stream, data);
                    }
#else
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

#endif
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }


            });

            /*
			//�����x�Z���T�[���g���ꍇ�͂�����API���Ă�
            InputContext.SetupAccelState(() =>
            {

                return new AccelerometerState(new Vector3(1,0,0));
            });
            */

            //�ŏ��̉�ʂɑJ��
            this._gameManager.ChangeScreen(new LoadScreen(), null, null);
        }



        protected override void Initialize()
        {
            this._gameManager.Init();
            base.Initialize();

#if WINDOWS
            GameContext.Game.IsMouseVisible = true;
#endif
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
