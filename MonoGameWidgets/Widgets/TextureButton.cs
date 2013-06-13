using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameWidgets.Utils;

namespace MonoGameWidgets.Widgets.ButtonImpl
{
    public class TextureButton : ButtonBase
    {
        private readonly Texture2D _texture;
        private Vector2 _position;
        private readonly SpriteBatch _sb;
        private Vector2 _size;
        private Vector2 _halfSize;
        private AABB _activeBox;

        public TextureButton(Texture2D texture, Vector2 position, SpriteBatch sb)
        {
            _texture = texture;
            _position = position;
            _sb = sb;
            _size = new Vector2(_texture.Width, _texture.Height);
            _halfSize = _size/2f;
            _activeBox = new AABB( - _halfSize, _halfSize).Inflate(new Vector2(20f));
        }

        protected override bool InActiveArea(Vector2 position)
        {
            return _activeBox.Contains(position - _position);
        }

        public override void Draw(GameTime gameTime)
        {
            _sb.Draw(_texture, _position, null, Color.White, Rotation, _halfSize, Scale, SpriteEffects.None, 0f);
        }

        public override void SetPosition(Vector2 position)
        {
            _position = position;
        }
    }
}