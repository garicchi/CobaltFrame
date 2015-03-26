using CobaltFrame.Common;
using CobaltFrame.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Animation
{
    public abstract class AnimationBase:GameObject,IAnimation
    {
        

        public AnimationState State { get; private set; }
        public TimeSpan Duration { get; private set; }

        public TimeSpan ElapsedTime { get; private set; }
        public TimeSpan BeginTime { get; private set; }
        public float Progress { get; private set; }

        public bool IsLoop { get; private set; }

        private AnimationState _beforeState;

        public AnimationBase(ObjectContext context)
            :base(context)
        {
            
            this._beforeState = AnimationState.Stop;
            this.State = AnimationState.Stop;
            
        }

        public override void Update(ObjectFrameContext frameContext)
        {
            if (this.State == AnimationState.Active)
            {
                if (this._beforeState == AnimationState.Stop && this.State == AnimationState.Active)
                {
                    BeginTime = frameContext.GameTime.TotalGameTime;
                }

                if (this._beforeState == AnimationState.Pause && this.State == AnimationState.Active)
                {
                    //Pause状態から再開した場合は現在の時間から経過した時間を戻す事によって初期時間を復元
                    this.BeginTime = frameContext.GameTime.TotalGameTime - ElapsedTime;
                }

                this.ElapsedTime = frameContext.GameTime.TotalGameTime - this.BeginTime;
                this.Progress = (float)(this.ElapsedTime.TotalSeconds / this.Duration.TotalSeconds);

                if (this.Progress >= 1.0f)
                {
                    if (this.IsLoop)
                    {
                        this.Progress = 0.0f;
                        this.BeginTime = frameContext.GameTime.TotalGameTime;
                    }
                    else 
                    {
                        this.Progress = 1.0f;
                        this.State = AnimationState.Stop;
                    }
                }
            }
            this._beforeState = State;
        }

        public virtual void Start(TimeSpan duration, bool isLoop)
        {
            this.Progress = 0.0f;
            this.IsLoop = isLoop;
            this.State = AnimationState.Active;
            this.Duration = duration;
        }

        public virtual void Pause()
        {
            this.State = AnimationState.Pause;
        }

        public virtual void Resume()
        {
            this.State = AnimationState.Active;
        }

        public virtual void Stop()
        {
            this.State = AnimationState.Stop;
        }

        

        

    }
}
