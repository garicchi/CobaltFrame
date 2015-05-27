﻿using CobaltFrame.Context;
using CobaltFrame.Object;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Mono.Sound
{
    public class SoundEffectObject:GameObject
    {
        protected SoundEffect _soundEffect;
        protected SoundEffectInstance _soundInstance;
        protected string _soundContentPath;
        public SoundEffectObject(string soundContentPath)
        {
            this._soundContentPath = soundContentPath;
        }

        public override void Load()
        {
            base.Load();
            this._soundEffect = ResourceContext.Load<SoundEffect>(this._soundContentPath);

            this._soundInstance = this._soundEffect.CreateInstance();
        }

        public void Play()
        {
            this._soundInstance.Play();
        }

        public void Pause()
        {
            this._soundInstance.Pause();
            
        }
        public void Stop()
        {
            this._soundInstance.Stop();
        }
        public void Resume()
        {
            this._soundInstance.Resume();
        }
    }
}
