using Microsoft.Xna.Framework;

namespace MonoGameWidgets.Widgets.Common
{
    public interface IWidget
    {
        void Update(GameTime gameTime);
        void Draw(GameTime gameTime);
        void SetPosition(Vector2 position);
    }
}