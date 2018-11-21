using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceShip
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D shipSprite;
        Texture2D asteroidSprite;
        Texture2D spaceSprite;

        SpriteFont gameFont;
        SpriteFont timerFont;

        Ship player = new Ship(Ship.defaultPosition);
        //Asteroid asteroid = new Asteroid(200, 50);
        Controller gameController = new Controller();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 800;
            graphics.PreferredBackBufferWidth = 1280;

            Content.RootDirectory = "Content";
        }

     
        protected override void Initialize()
        {
            base.Initialize();
        }
        
        protected override void LoadContent()
        {

            spriteBatch = new SpriteBatch(GraphicsDevice);

            shipSprite = Content.Load<Texture2D>("ship");
            spaceSprite = Content.Load<Texture2D>("space");
            asteroidSprite = Content.Load<Texture2D>("asteroid");

            gameFont = Content.Load<SpriteFont>("spaceFont");
            timerFont = Content.Load<SpriteFont>("timerFont");


            // TODO: use this.Content to load your game content here
        }

    
        protected override void UnloadContent()
        {
           
        }
        
        protected override void Update(GameTime gameTime)
        {
            KeyboardState state = Keyboard.GetState();

            if (state.IsKeyDown(Keys.Escape))
                Exit();
            

           
            player.shipUpdate(gameTime, gameController);
                //asteroid.asteroidUpdate(gameTime);
            gameController.controllerUpdate(gameTime);

            for (int i = 0; i < gameController.asteroids.Count; i++) {
                Asteroid a = gameController.asteroids[i];
                a.asteroidUpdate(gameTime);
                //int sumVertical = a.getRadius() + 50;
                int sumHorizontal = a.getRadius() + 34;
                System.Console.WriteLine("Vector Distance: " + Vector2.Distance(a.GetVector, player.GetVector) + "; horizontalSum: " + sumHorizontal);
                System.Console.WriteLine("Vectors Cords - player : (" + player.GetVector.X + ", " + player.GetVector.Y + "); asteroida: (" + a.GetVector.X + ", " + a.GetVector.Y + ")");
                if (Vector2.Distance(a.GetVector, player.GetVector) < sumHorizontal) {  //(int)(Vector2.Distance(a.GetVector(), player.GetVector())            Vector2.Distance(player.GetCord(Ship.Cords.B), a.GetVector()) 
                    gameController.inGame = false;
                    player.SetVector(Ship.defaultPosition);
                    i = gameController.asteroids.Count;
                    gameController.asteroids.Clear();
                }
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            spriteBatch.Draw(spaceSprite, new Vector2(0, 0), Color.White);
            if (!gameController.inGame) {
                string menuMsg = "Press 'Enter' to start the game.";
                Vector2 stringPosition = gameFont.MeasureString(menuMsg);
                spriteBatch.DrawString(gameFont, menuMsg, new Vector2(stringPosition.X/2, stringPosition.Y), Color.White);
            } else { 
                    foreach(Asteroid a in gameController.asteroids)
                    {
                        spriteBatch.Draw(asteroidSprite, a.GetVector, Color.White);
                }
            }
            spriteBatch.Draw(shipSprite, player.GetVector, Color.Green);
            spriteBatch.End();
            
            base.Draw(gameTime);
        }
    }
}
