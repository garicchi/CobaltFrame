using CobaltFrame.Context;
using CobaltFrame.Object;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.IO;
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
        protected bool _isFromStream;

        public SoundEffectObject(string soundContentPath,bool isFromStream = false)
        {
            this._soundContentPath = soundContentPath;
            this._isFromStream = isFromStream;
        }

        public override void Load()
        {
            base.Load();
            if (this._isFromStream)
            {
                //MonoGameのバグ？で再生できない
                this._soundEffect = ResourceContext.LoadWithoutManager<SoundEffect>(this._soundContentPath, () => 
                {
                    var root = GameContext.Game.Content.RootDirectory;
                    var fontFilePath = Path.Combine(root + "/" + this._soundContentPath + ".wav");
                    SoundEffect sound = null;
                    using (var stream = TitleContainer.OpenStream(fontFilePath))
                    {
                        sound = SoundEffect.FromStream(stream);
                    }
                    return sound;
                });
                
            }
            else 
            {
                this._soundEffect = ResourceContext.Load<SoundEffect>(this._soundContentPath);
            }
            
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

        public override void Update(FrameContext context)
        {
            
            base.Update(context);
        }
    }
}
