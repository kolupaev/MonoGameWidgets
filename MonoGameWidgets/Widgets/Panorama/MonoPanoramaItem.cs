using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameWidgets.Widgets
{
    public class MonoPanoramaItem : IMonoPanoramaItem
    {
        private readonly Vector2 _centerAdjust;
        private readonly SpriteBatch _sb;
        private readonly Texture2D _texture;
        private Vector2 _offset = Vector2.Zero;
        private bool _visible;

        public MonoPanoramaItem(Texture2D texture, SpriteBatch sb)
            : this(texture, sb, false)
        {
        }

        public MonoPanoramaItem(Texture2D texture, SpriteBatch sb, bool centered)
        {
            _texture = texture;
            _sb = sb;
            _centerAdjust = Vector2.Zero;
            if (centered)
            {
                _centerAdjust = new Vector2(_sb.GraphicsDevice.Viewport.Width, _sb.GraphicsDevice.Viewport.Height) / 2f -
                                new Vector2(_texture.Width, _texture.Height) / 2f;
            }
        }

        public void Draw(GameTime gameTime)
        {
            _sb.Draw(_texture, _offset + _centerAdjust, Color.White);
        }

        public void Update(GameTime time)
        {


        }

        public bool Visible
        {
            get { return _visible; }
            private set
            {
                _visible = value;
            }
        }


        public void SetDrawOffset(Vector2 offset)
        {
            _offset = offset;
        }

        public void SetActive(bool isActive)
        {
            Visible = isActive;
        }

        public float Width
        {
            get { return Math.Max(_texture.Width, _sb.GraphicsDevice.Viewport.Width); }
        }

    }
}