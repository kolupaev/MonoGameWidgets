using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameWidgets.Widgets.ButtonImpl;

namespace MonoGameWidgetsDemo.Screens
{
    public class MenuScreen : GameScreen
    {
        private SpriteFont _buttonFont;
        private TextButton _button;

        public override void LoadContent()
        {
            _buttonFont = ScreenManager.Game.Content.Load<SpriteFont>(@"Fonts\Button");
            var screenCenter =
                new Vector2(ScreenManager.GraphicsDevice.Viewport.Width, ScreenManager.GraphicsDevice.Viewport.Height)/
                2f;
            _button = new TextButton(screenCenter, _buttonFont, "Play!", ScreenManager.SpriteBatch);
            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            ScreenManager.SpriteBatch.Begin();
            _button.Draw(gameTime);
            ScreenManager.SpriteBatch.End();
            base.Draw(gameTime);
        }

        public override void HandleInput()
        {
            _button.HandleInput();
            base.HandleInput();
        }
        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            _button.Update(gameTime);
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }
    }
}