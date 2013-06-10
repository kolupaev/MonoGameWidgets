using System;
using Microsoft.Xna.Framework;

namespace MonoGameWidgets.Widgets
{
    internal class FuncAnimation : IAnimation
    {
        private readonly Action _action;

        public FuncAnimation(Action action)
        {
            _action = action;
        }

        public void Start(GameTime time)
        {
            _action();
            Started = Done = true;
        }

        public void Update(GameTime time)
        {
        }

        public bool Started { get; private set; }
        public bool Done { get; private set; }
    }
}