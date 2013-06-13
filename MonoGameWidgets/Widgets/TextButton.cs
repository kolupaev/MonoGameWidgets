using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameWidgets.Utils;

namespace MonoGameWidgets.Widgets.ButtonImpl
{
    public class TextButton : ButtonBase
    {
        private Vector2 _position;
        private readonly SpriteFont _font;
        private readonly string _text;
        private readonly SpriteBatch _sb;
        private readonly Vector2 _size;
        private readonly AABB _activeBox;
        private readonly Vector2 _center;
        private Vector2 _halfSize;

        public TextButton(Vector2 position, SpriteFont font, string text, SpriteBatch sb)
        {
            _position = position;
            _font = font;
            _text = text;
            _sb = sb;

            _size = _font.MeasureString(_text);
            _halfSize = _size / 2f;


            _activeBox = new AABB(- _halfSize, _halfSize).Inflate(new Vector2(20f));
            _center = _activeBox.GetCenter();
        }
        protected override bool InActiveArea(Vector2 position)
        {
            return _activeBox.Contains(position - _position);
        }
        public override void Draw(GameTime gameTime)
        {
            _sb.DrawString(_font, _text, _position, Color.Black, 0f, _halfSize, Scale, SpriteEffects.None, 1f);
        }

        public override void SetPosition(Vector2 position)
        {
            _position = position;
        }
    }
}