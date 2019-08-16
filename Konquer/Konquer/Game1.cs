using Konquer.Classes.Sprites;
using Konquer.Classes.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Konquer
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D _tileTexture, _playerTexture;
        private Player _player;
        private Board _board;
        private SpriteFont _debugFont;

        public int ScreenWidth = 1888, ScreenHeight = 1000;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            _graphics.PreferredBackBufferWidth = ScreenWidth;
            _graphics.PreferredBackBufferHeight = ScreenHeight;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _tileTexture = Content.Load<Texture2D>("Tiles/L2/BlockA1");
            _playerTexture = Content.Load<Texture2D>("Tiles/L2/Platform");
            _player = new Player(_playerTexture, new Vector2(50, 50), _spriteBatch);
            
            _player.Load(Content);
            _board = new Board(_spriteBatch, _tileTexture, 59, 25);

            _debugFont = Content.Load<SpriteFont>("DebugFont");
        }
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            _player.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            base.Draw(gameTime);
            _board.Draw(gameTime);
            _player.Draw(gameTime);
            WriteDebugInfo();
            _spriteBatch.End();
        }

        private void WriteDebugInfo()
        {
            string positionInText =
                string.Format("Position of Player: ({0:0.0}, {1:0.0})", _player.Position.X, _player.Position.Y);
            string movementInText =
                string.Format("Current movement: ({0:0.0}, {1:0.0})", _player.Movement.X, _player.Movement.Y);
            string isGroundedText =
                string.Format("Grounded? : {0}", _player.IsGrounded());

            _spriteBatch.DrawString(_debugFont, positionInText, new Vector2(10, 0), Color.White);
            _spriteBatch.DrawString(_debugFont, movementInText, new Vector2(10, 20), Color.White);
            _spriteBatch.DrawString(_debugFont, isGroundedText, new Vector2(10, 40), Color.White);
        }
    }
}
