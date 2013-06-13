using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameWidgets.Widgets.ButtonImpl;

namespace MonoGameWidgets.Widgets
{
    public class ButtonPanoramaItem : IMonoPanoramaItem
    {
        private readonly SpriteBatch _sb;
        private readonly ButtonBase _button;
        private readonly Vector2 _centerOffset;

        public ButtonPanoramaItem(SpriteBatch sb, ButtonBase button)
        {
            _sb = sb;
            _button = button;
            _centerOffset = new Vector2(sb.GraphicsDevice.Viewport.Width, sb.GraphicsDevice.Viewport.Height)/2f;
            SetDrawOffset(Vector2.Zero);
        }


        public void Update(Microsoft.Xna.Framework.GameTime time)
        {
        }

        public bool Visible { get; private set; }
        public float Width
        {
            get { return _sb.GraphicsDevice.Viewport.Width; }
        }

        public void SetDrawOffset(Vector2 offset)
        {
            _button.SetPosition(offset + _centerOffset);
        }

        public void SetActive(bool isActive)
        {
            Visible = isActive;
        }

        public void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            _button.Draw(gameTime);
        }
    }
}