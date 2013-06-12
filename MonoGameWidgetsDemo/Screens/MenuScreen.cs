using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using MonoGameWidgets.Utils;
using MonoGameWidgets.Widgets.ButtonImpl;

namespace MonoGameWidgetsDemo.Screens
{
    public class MenuScreen : GameScreen
    {
        private SpriteFont _buttonFont;
        private TextButton _playButton;
        private InputManager _inputManager;

        public MenuScreen()
        {
            _inputManager = new InputManager();
        }

        public override void LoadContent()
        {
            _buttonFont = ScreenManager.Game.Content.Load<SpriteFont>(@"Fonts\Button");
            var screenCenter =
                new Vector2(ScreenManager.GraphicsDevice.Viewport.Width, ScreenManager.GraphicsDevice.Viewport.Height)/
                2f;
            _playButton = new TextButton(screenCenter, _buttonFont, "Play!", ScreenManager.SpriteBatch);
            _playButton.OnTap += PlayButtonOnOnTap;
            base.LoadContent();
            TouchPanel.EnabledGestures = GestureType.Tap | GestureType.FreeDrag | GestureType.DragComplete;

        }

        private void PlayButtonOnOnTap()
        {
            ScreenManager.AddScreen(new Submenu1Screen(){ScreenState = ScreenState.Active});
            ExitScreen();

        }

        public override void Draw(GameTime gameTime)
        {
            ScreenManager.SpriteBatch.Begin();
            _playButton.Draw(gameTime);
            ScreenManager.SpriteBatch.End();
            base.Draw(gameTime);
        }

        public override void HandleInput()
        {
            _inputManager.ReadInput();
            _playButton.HandleInput(_inputManager);
            base.HandleInput();
        }
        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            _playButton.Update(gameTime);
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }
    }
}