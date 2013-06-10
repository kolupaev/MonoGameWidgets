using System;

namespace MonoGameWidgets.Widgets
{
    internal static class Animation
    {
        public static IAnimation Action(Action f)
        {
            return new FuncAnimation(f);
        }

        public static IAnimation Value(float start, float end, TimeSpan duration, Action<float> setter)
        {
            return new ValueAnimation(start, end, duration, setter);
        }
    }
}