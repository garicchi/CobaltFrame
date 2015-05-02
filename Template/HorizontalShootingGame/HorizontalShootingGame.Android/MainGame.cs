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
using HorizontalShootingGame.Portable;
using Microsoft.Xna.Framework.Storage;

namespace HorizontalShootingGame
{
	public class MainGame:Game
	{
		GameScreenManager _screenManager;
		public MainGame()
		{
			Content.RootDirectory = "Content";

			this._screenManager = new GameScreenManager(this,new LoadScreen(),null, new Vector2(1360, 768), ScaleMode.Fill);
			GameContext.GraphicsManager.IsFullScreen = true;
			SaveDataStore<SaveData>.Setup("savedata", (name) =>
				{
					SaveData data = null;
					var deviceResult = StorageDevice.BeginShowSelector(null,null);
					deviceResult.AsyncWaitHandle.WaitOne();
					var device = StorageDevice.EndShowSelector(deviceResult);
					var containerResult = device.BeginOpenContainer(name,null,null);
					containerResult.AsyncWaitHandle.WaitOne();
					var container = device.EndOpenContainer(containerResult);

					if (container.FileExists(name))
					{
						var stream = container.OpenFile(name, FileMode.Open);
						var serializer = new XmlSerializer(typeof(SaveData));
						data = (SaveData)serializer.Deserialize(stream);
						stream.Close();
					}

					container.Dispose();
					return data;

				}, (name, data) =>
				{
					try
					{
						var deviceResult = StorageDevice.BeginShowSelector(null,null);
						deviceResult.AsyncWaitHandle.WaitOne();
						var device = StorageDevice.EndShowSelector(deviceResult);
						var containerResult = device.BeginOpenContainer(name,null,null);
						containerResult.AsyncWaitHandle.WaitOne();
						var container = device.EndOpenContainer(containerResult);
						if (container.FileExists(name))
							container.DeleteFile(name);

						var stream = container.CreateFile(name);
						var serializer = new XmlSerializer(typeof(SaveData));
						serializer.Serialize(stream, data);
						stream.Close();

						container.Dispose();

						return true;
					}
					catch (Exception)
					{
						return false;
					}


				});
			this.Activated += (s,e) => 
			{
				SaveDataStore<SaveData>.Load(new SaveData());
			};
			this.Deactivated += (s,e) => 
			{
				SaveDataStore<SaveData>.Save();
			};
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
