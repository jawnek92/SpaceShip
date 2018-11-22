using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShip {
    class Controller {
        public List<Asteroid> asteroids = new List<Asteroid>();
        public double timer = 2;
        public double maxTime = 2;
        public int nextSpeed = 200;
        public bool inGame = false;

        public void controllerUpdate(GameTime gameTime) {
            if (inGame) {
                timer -= gameTime.ElapsedGameTime.TotalSeconds;
            } else {
                KeyboardState kstate = Keyboard.GetState();
                if(kstate.IsKeyDown(Keys.Enter))
                    inGame = true;
            }

            if (timer <= 0) {
                asteroids.Add(new Asteroid(nextSpeed));
                timer = maxTime;
                //if (maxTime > 0.5)
                //    maxTime -= 0.1;
                //if (nextSpeed < 500)
                //    nextSpeed += 5;
                //else
                //    nextSpeed += 10;
            }
        }
    }
}
