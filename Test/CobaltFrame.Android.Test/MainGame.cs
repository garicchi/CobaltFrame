using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using CobaltFrame;
using CobaltFrame.Screen;
using CobaltFrame.Test.Screen;
using CobaltFrame.Context;
using CobaltFrame.Core.Screen;
using Microsoft.Xna.Framework.Input;

namespace CobaltFrame.Universal.Test
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
            this._screenManager = new GameScreenManager(this._gameContext, new SampleScreen(this._gameContext), null, new Vector2(1366, 768), ScaleMode.Fill);

        }

        protected override void Initialize()
        {
            base.Initialize();
            this._screenManager.Init();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            this._screenManager.Load();
        }

        protected override void UnloadContent()
        {
            base.UnloadContent();
            this._screenManager.Unload();
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
