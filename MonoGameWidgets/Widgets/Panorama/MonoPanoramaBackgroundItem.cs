using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameWidgets.Widgets
{
    public class MonoPanoramaBackgroundItem : IMonoPanoramaItem
    {
        private readonly float _ratio;
        private readonly Texture2D _texture;
        private readonly SpriteBatch _sb;
        private Vector2 _offset;
        private Vector2? _offset2;

        public MonoPanoramaBackgroundItem(float ratio, Texture2D texture, SpriteBatch sb)
        {
            _ratio = ratio;
            _texture = texture;
            _sb = sb;
            Visible = true;
        }

        public void Draw(GameTime gameTime)
        {
            _sb.Draw(_texture, _offset, Color.White);
            if (_offset2.HasValue)
                _sb.Draw(_texture, _offset2.Value, Color.White);
        }

        public void Update(GameTime time)
        {
        }


        public bool Visible { get; private set; }


        public void SetDrawOffset(Vector2 offset)
        {
            _offset = new Vector2(MathHelper.Lerp(0, offset.X, _ratio), offset.Y);

            while (_offset.X < -_texture.Width)
            {
                _offset.X += _texture.Width;
            }
            while (_offset.X > _texture.Width)
            {
                _offset.X -= _texture.Width;
            }

            if (_offset.X > 0)
            {
                _offset2 = new Vector2(-_texture.Width + _offset.X, _offset.Y);
            }
            else if (_offset.X < -_texture.Width + _sb.GraphicsDevice.Viewport.Width)
            {
                _offset2 = new Vector2(_offset.X + _texture.Width, _offset.Y);
            }
            else
            {
                _offset2 = null;
            }
        }

        public void SetActive(bool isActive)
        {
            Visible = isActive;
        }

        public float Width
        {
            get { return _texture.Width; }
        }
    }
}