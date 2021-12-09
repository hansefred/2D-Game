using Autofac;
using GameEngine.HUDs;
using GameEngine.Model;
using GameEngine.Service;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Myra.Graphics2D.UI;

namespace GameEngine
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private IContainer _container;
        private GameObjectManager _gameObjecManager;
        private UIService _uiService;


        #region Public Property
        public GameData GameData;
        #endregion




        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            GameData = new GameData();
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            this.GraphicsDevice.PresentationParameters.BackBufferWidth = 1920;
            this.GraphicsDevice.PresentationParameters.BackBufferHeight = 1080;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _container = ServiceRegister.Register(this,_graphics,_spriteBatch);
            _gameObjecManager = _container.Resolve<GameObjectManager>();



            var player = new Player(new Vector2(100, 100), new Vector2(50, 50), 100, new Vector2(), 5, new Animation(Content.Load<Texture2D>("Player"), new Vector2(100, 100)), new Animation(Content.Load<Texture2D>("Explosion"), new Vector2(100, 100)));
            _gameObjecManager.Add(player);
            _gameObjecManager.Add(new Enemy(new Vector2(500, 100), new Vector2(50, 50), 100, new Vector2(), 2, new Animation (Content.Load<Texture2D>("Enemy"), new Vector2 (100,100)), new Animation(Content.Load<Texture2D>("Explosion"), new Vector2(100, 100)), player));


            _uiService = _container.Resolve<UIService>();
            _uiService.ChangeUI(new IngameHUD(_container.Resolve<Game1>(),_container.Resolve<GameObjectManager>().GetPlayer()));

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            GameData.IngameTime = GameData.IngameTime + gameTime.ElapsedGameTime;


            _gameObjecManager.Update(gameTime);
            _uiService.Update(gameTime);
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            // TODO: Add your drawing code here
            _uiService.Render();
            _gameObjecManager.Render();
       

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
