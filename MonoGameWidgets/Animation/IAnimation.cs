using Microsoft.Xna.Framework;

namespace MonoGameWidgets.Widgets
{
    internal interface IAnimation
    {
        bool Started { get; }
        bool Done { get; }
        void Start(GameTime time);
        void Update(GameTime time);
    }
}