using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShip {
    class Asteroid {
        private static Random random = new Random();
        private Vector2 position;

        public Vector2 Position {
            get {
                return this.position;
            }
        }
        private float speed;
        private const int radius = 29;
        public int Radius {
            get { return radius; }
        }

        public Asteroid(float speed) {
            this.speed = speed;
            position = new Vector2(1280 - radius, (random.Next(0, 800) - radius)); //
        }

        

        public void asteroidUpdate(GameTime gameTime) {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            //System.Console.WriteLine(dt);
            this.position.X -= speed * dt;
           
        }
    }
}
