using Microsoft.Xna.Framework;

namespace MonoGameWidgets.Widgets
{
    public interface IMonoPanoramaItem
    {
        float Width { get; }
        void SetDrawOffset(Vector2 offset);
        void SetActive(bool isActive);
        void Draw(GameTime time);
        void Update(GameTime time);
        bool Visible { get; }
    }
}