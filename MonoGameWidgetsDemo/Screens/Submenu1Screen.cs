using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameWidgets.Utils;
using MonoGameWidgets.Widgets;
using MonoGameWidgets.Widgets.ButtonImpl;

namespace MonoGameWidgetsDemo.Screens
{
    public class Submenu1Screen : GameScreen
    {
        private MonoPanorama _monoPanorama;
        private InputManager _inputManager;
        private List<TextureButton> _buttons = new List<TextureButton>(); 

        public Submenu1Screen()
        {
            _inputManager = new InputManager();
        }

        public override void LoadContent()
        {
            var content = ScreenManager.Game.Content;
            var spriteBatch = ScreenManager.SpriteBatch;
            _monoPanorama = new MonoPanorama();
            _monoPanorama.AddBackgorund(new MonoPanoramaBackgroundItem(0.1f, content.Load<Texture2D>(@"Layers\Sky_back_layer"), spriteBatch));
            _monoPanorama.AddBackgorund(new MonoPanoramaBackgroundItem(0.8f, content.Load<Texture2D>(@"Layers\Vegetation"), spriteBatch));
            _monoPanorama.AddPanoramaItem(new ButtonPanoramaItem(spriteBatch, Button(content.Load<Texture2D>(@"Levels\1"))));
            _monoPanorama.AddPanoramaItem(new ButtonPanoramaItem(spriteBatch, Button(content.Load<Texture2D>(@"Levels\2"))));
            _monoPanorama.AddPanoramaItem(new ButtonPanoramaItem(spriteBatch, Button(content.Load<Texture2D>(@"Levels\3"))));
            _monoPanorama.AddPanoramaItem(new ButtonPanoramaItem(spriteBatch, Button(content.Load<Texture2D>(@"Levels\4"))));
            _monoPanorama.AddBackgorund(new MonoPanoramaBackgroundItem(1.5f, content.Load<Texture2D>(@"Layers\Ground"), spriteBatch));
            
            _monoPanorama.Initialize();
            base.LoadContent();
        }

        private ButtonBase Button(Texture2D texture2D)
        {
            var textureButton = new TextureButton(texture2D, Vector2.Zero, ScreenManager.SpriteBatch);
            _buttons.Add(textureButton);
            return textureButton;
        }

        public override void Draw(GameTime gameTime)
        {
            this.ScreenManager.SpriteBatch.Begin();
            _monoPanorama.Draw(gameTime);
            this.ScreenManager.SpriteBatch.End();
            base.Draw(gameTime);
        }
        public override void HandleInput()
        {
            _inputManager.ReadInput();
            _monoPanorama.HandleInput(_inputManager);

            foreach (var button in _buttons)
            {
                button.HandleInput(_inputManager);
            }

            base.HandleInput();
        }
        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            _monoPanorama.Update(gameTime);

            foreach (var button in _buttons)
            {
                button.Update(gameTime);
            }

            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }
    }
}