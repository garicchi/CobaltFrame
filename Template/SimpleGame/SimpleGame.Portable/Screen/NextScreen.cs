﻿using CobaltFrame.Context;
using CobaltFrame.Screen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleGame.Portable.Screen
{
    public class NextScreen:GameScreen
    {
        public NextScreen()
        {

        }

        public override void Init()
        {
            base.Init();
        }

        public override void Load()
        {
            base.Load();
        }

        public override void Unload()
        {
            base.Unload();
        }

        public override void Update(FrameContext context)
        {
            base.Update(context);
        }

        public override void Draw(FrameContext context)
        {
            base.Draw(context);
        }

        public override void NavigateTo(object parameter,IScreenTransition transition = null)
        {
            base.NavigateTo(parameter, transition);
        }
    }
}
