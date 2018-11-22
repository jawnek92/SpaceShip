using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShip {
    class Projectile {
        public static List<Projectile> projectiles = new List<Projectile>();
        private Vector2 position;
        private int speed = 800;
        private int radius = 15;
        private bool collided = false;

        public Projectile(Vector2 startPos) {
            position = startPos;
        }
        public Vector2 Position {
            get {
                return position;
            }
        }
        public int Radius {
            get {
                return radius;
            }
        }
        public bool Collided {
            get { return this.collided; }
            set { collided = value; }
        }

        public void update(GameTime gameTime) {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            position.X += speed * dt;
        }
    }
}
