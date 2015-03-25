using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using CobaltFrame;

namespace CobaltGame.Android
{
    class MainGame:Game
    {
        GraphicsDeviceManager graphics;
        SampleComponent component;
        public MainGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            component = new SampleComponent(this);
            Components.Add(component);
        }

        protected override void Initialize()
        {
            
            base.Initialize();
        }
    }
}
