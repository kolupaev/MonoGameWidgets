using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameWidgets.Utils;
using MonoGameWidgets.Widgets;

namespace MonoGameWidgetsDemo.Screens
{
    public class Submenu1Screen : GameScreen
    {
        private MonoPanorama _monoPanorama;
        private InputManager _inputManager;

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
            _monoPanorama.AddPanoramaItem(new MonoPanoramaItem(content.Load<Texture2D>(@"Levels\1"), spriteBatch, true));
            _monoPanorama.AddPanoramaItem(new MonoPanoramaItem(content.Load<Texture2D>(@"Levels\2"), spriteBatch, true));
            _monoPanorama.AddPanoramaItem(new MonoPanoramaItem(content.Load<Texture2D>(@"Levels\3"), spriteBatch, true));
            _monoPanorama.AddPanoramaItem(new MonoPanoramaItem(content.Load<Texture2D>(@"Levels\4"), spriteBatch, true));
            _monoPanorama.AddBackgorund(new MonoPanoramaBackgroundItem(1.5f, content.Load<Texture2D>(@"Layers\Ground"), spriteBatch));
            
            _monoPanorama.Initialize();
            base.LoadContent();
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
            base.HandleInput();
        }
        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            _monoPanorama.Update(gameTime);
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }
    }
}