using System;
using Microsoft.Xna.Framework;

namespace MonoGameWidgets.Widgets
{
    internal class ValueAnimation : IAnimation
    {
        private readonly TimeSpan _duration;
        private readonly float _end;
        private readonly Action<float> _setter;
        private readonly float _start;
        private TimeSpan _startTime;

        public ValueAnimation(float start, float end, TimeSpan duration, Action<float> setter)
        {
            _start = start;
            _end = end;
            _duration = duration;
            _setter = setter;
        }

        public void Start(GameTime time)
        {
            _startTime = time.TotalGameTime;
            Started = true;
            _setter(_start);
        }

        public void Update(GameTime time)
        {
            float completedRatio = (float)(time.TotalGameTime - _startTime).Ticks / _duration.Ticks;
            if (completedRatio < 1f)
            {
                float v = MathHelper.SmoothStep(_start, _end, completedRatio);
                _setter(v);
            }
            else
            {
                _setter(_end);
                Done = true;
            }
        }

        public bool Started { get; private set; }
        public bool Done { get; private set; }
    }
}