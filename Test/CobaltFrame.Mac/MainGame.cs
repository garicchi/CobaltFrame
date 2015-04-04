using System;
using Microsoft.Xna.Framework;
using CobaltFrame.Screen;
using CobaltFrame.Context;
using CobaltFrame.Test.Screen;
using CobaltFrame.Core.Screen;

namespace CobaltFrame.Mac
{
	class MainGame : Game
	{
		GraphicsDeviceManager graphics;
		GameScreenManager _screenManager;
		GameContext _gameContext;
		public MainGame()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			this._gameContext = new GameContext(this);
			this._screenManager = new GameScreenManager(this._gameContext, new SampleScreen(this._gameContext), null, new Vector2(960, 540), ScaleMode.None);

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

