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
using CobaltFrame.Universal.Test;
#endif

namespace CobaltFrame
{
    /// <summary>
    /// ���ׂĂ̐e�ƂȂ�Q�[���N���X
    /// </summary>
    public class MainGame : Game
    {
        //�Q�[���S�̂��Ǘ�����N���X
        GameManager _gameManager;
        FrameContext _frameContext;
        public MainGame()
        {
            Content.RootDirectory = "Content";

            //�Q�[����ʂ̉𑜓x���w��
            this._gameManager = new GameManager(this, new Point(1360, 768), this.Window.ClientBounds, ScaleMode.Fill);
            this._frameContext = new FrameContext();

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

            //�T�|�[�g����f�o�C�X�̌��������Ɍ���
            GameContext.GraphicsManager.SupportedOrientations = DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight;

            /*
			//�����x�Z���T�[���g���ꍇ�͂�����API���Ă�ŉ����x����Ԃ�
            InputContext.SetupAccelState(() =>
            {

                return new AccelerometerState(new Vector3(1,0,0));
            });
            */

            //�ŏ��̉�ʂɑJ��
            this._gameManager.ChangeScreen(new Physics2DScreen(), null, null);
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
            this._frameContext.GameTime = gameTime;
            this._gameManager.Update(this._frameContext);
        }

        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            this._frameContext.GameTime = gameTime;
            this._gameManager.Draw(this._frameContext);
        }


    }
}
