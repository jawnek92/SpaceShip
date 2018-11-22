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
        Texture2D projectileSprite;

        SpriteFont gameFont;
        SpriteFont timerFont;

        Player player = new Player(Player.defaultPosition);
        //Asteroid asteroid = new Asteroid(200, 50);
        Controller gameController = new Controller();

        KeyboardState oldState = Keyboard.GetState();

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

            shipSprite = Content.Load<Texture2D>("Player/ship");
            spaceSprite = Content.Load<Texture2D>("Background/space");
            asteroidSprite = Content.Load<Texture2D>("Obstacles/asteroid");
            projectileSprite = Content.Load<Texture2D>("Projectiles/bullet");

            gameFont = Content.Load<SpriteFont>("Fonts/spaceFont");
            timerFont = Content.Load<SpriteFont>("Fonts/timerFont");


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
                       
            player.update(gameTime, gameController);
            gameController.controllerUpdate(gameTime);

            if (state.IsKeyDown(Keys.Space) && oldState.IsKeyUp(Keys.Space)) {
                Projectile.projectiles.Add(new Projectile(player.Position));
                //System.Console.WriteLine("Projectiles count : " + Projectile.projectiles.Count);
            }
            oldState = state;

            for (int i = 0; i < gameController.asteroids.Count; i++) { 
                Asteroid asteroid = gameController.asteroids[i];
                asteroid.asteroidUpdate(gameTime);
                int sumHorizontalBetweenPlayerAndAsteroid = asteroid.Radius + 34;
                //System.Console.WriteLine("Vector Distance: " + Vector2.Distance(a.GetVector, player.Position) + "; horizontalSum: " + sumHorizontal);
                //System.Console.WriteLine("Vectors Cords - player : (" + player.Position.X + ", " + player.Position.Y + "); asteroida: (" + a.GetVector.X + ", " + a.GetVector.Y + ")");
                if (Vector2.Distance(asteroid.Position, player.Position) < sumHorizontalBetweenPlayerAndAsteroid) {  //(int)(Vector2.Distance(a.GetVector(), player.GetVector())            Vector2.Distance(player.GetCord(Ship.Cords.B), a.GetVector()) 
                    gameController.inGame = false;
                    player.SetPosition(Player.defaultPosition);
                    i = gameController.asteroids.Count;
                    gameController.asteroids.Clear();
                }

                for (int j = 0; j < Projectile.projectiles.Count; j++) {
                    Projectile projectile = Projectile.projectiles[j];
                    //System.Console.WriteLine("projectile X : " + projectile.Position.X + ", Y: " + projectile.Position.Y);
                    int sumBetweenProjectileAndAsteroid = asteroid.Radius + projectile.Radius;
                    if (Vector2.Distance(asteroid.Position, projectile.Position) < sumBetweenProjectileAndAsteroid) {
                        gameController.asteroids.Remove(asteroid);
                        Projectile.projectiles.Remove(projectile);
                    }
                    projectile.update(gameTime);
                    //System.Console.WriteLine("projectile X : " + projectile.Position.X + ", Y: " + projectile.Position.Y);

                    //int sumVertical = a.getRadius() + 50;

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
                foreach (Asteroid a in gameController.asteroids) {
                    spriteBatch.Draw(asteroidSprite, a.Position, Color.White);
                }
                foreach(Projectile p in Projectile.projectiles) {
                    spriteBatch.Draw(projectileSprite, p.Position, Color.White);
                }
            }
            spriteBatch.Draw(shipSprite, player.Position, Color.Green);
            spriteBatch.End();
            
            base.Draw(gameTime);
        }
    }
}
