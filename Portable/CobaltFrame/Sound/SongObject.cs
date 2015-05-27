using CobaltFrame.Context;
using CobaltFrame.Object;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Sound
{
    public class SongObject:GameObject
    {
        protected Song _song;
        protected string _songContentPath;

        public bool IsRpeating
        {
            get { return MediaPlayer.IsRepeating; }
            set { MediaPlayer.IsRepeating = value; }
        }
        public SongObject(string songContentPath)
        {
            this._songContentPath = songContentPath;
        }

        public override void Init()
        {
            base.Init();
        }

        public override void Load()
        {
            base.Load();
            this._song = ResourceContext.Load<Song>(this._songContentPath);
        }

        public override void Unload()
        {
            base.Unload();
        }

        public override void Update(FrameContext context)
        {
            base.Update(context);
        }

        public void Play()
        {
            MediaPlayer.Play(this._song);
        }

        public void Pause()
        {
            MediaPlayer.Pause();
        }

        public void Stop()
        {
            MediaPlayer.Stop();
        }

        public void Resume()
        {
            MediaPlayer.Resume();
        }
    }
}
