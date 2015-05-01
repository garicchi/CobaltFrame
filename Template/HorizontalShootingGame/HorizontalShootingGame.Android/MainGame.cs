using CobaltFrame.Mono.Context;
using CobaltFrame.Core.Data;
using CobaltFrame.Mono.Screen;
using HorizontalShootingGame.Portable.Data;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;
using System.Linq;
using HorizontalShootingGame.Portable.Screen;
using CobaltFrame.Core.Screen;
using CobaltFrame.Mono.Input;
using System.Diagnostics;

namespace HorizontalShootingGame
{
	public class MainGame:Game
	{
		GameScreenManager _screenManager;
		public MainGame()
		{
			Content.RootDirectory = "Content";


			this._screenManager = new GameScreenManager(this,new TitleScreen(),null, new Vector2(1360, 768), ScaleMode.Fill);
			GameContext.GraphicsManager.IsFullScreen = true;
			SaveDataStore<SaveData>.Setup("savedata", (name) =>
				{
					SaveData data = null;
					var task = Task.Run(async () =>
						{
							/*
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
					*/
						});
					task.Wait();
					return data;

				}, (name, data) =>
				{
					try
					{
						var task = Task.Run(async () =>
							{
								/*
                        StorageFolder folder = ApplicationData.Current.LocalFolder;
                        StorageFile file = await folder.CreateFileAsync(name, CreationCollisionOption.ReplaceExisting);
                        var serializer = new XmlSerializer(typeof(SaveData));
                        using (var stream = await file.OpenAsync(FileAccessMode.ReadWrite))
                        {
                            serializer.Serialize(stream.AsStream(), data);
                        }
						*/                    });
						task.Wait();
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
			this._screenManager.Init();
			base.Initialize();


		}

		protected override void LoadContent()
		{
			this._screenManager.Load();
			base.LoadContent();

		}

		protected override void UnloadContent()
		{
			this._screenManager.Unload();
			base.UnloadContent();

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
