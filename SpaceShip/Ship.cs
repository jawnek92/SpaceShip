using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShip
{
    class Ship
    {
        private float speed = 300;
        private float cordX = 50;
        private float cordY = 50;

        


        private Vector2 position;

        public static Vector2 defaultPosition = new Vector2(640, 400);

        public Vector2 GetVector {
            get {
                return this.position;
            }
        }

        //public Ship(float cordX, float cordY) {
        //    this.cordX = cordX;
        //    this.cordY = cordY;
        //    position = new Vector2(cordX - 34, cordY - 50);
        //}
        public enum Cords {
            A,B,C,D
        }

        public Vector2 GetCord(Cords cord) {
            switch (cord) {
                case Cords.A:
                    return new Vector2(position.X - 34, position.Y - 50);       // A---B
                case Cords.B:                                                   // |   |
                    return new Vector2(position.X + 34, position.Y - 50);       // |   |
                case Cords.C:                                                   // C---D
                    return new Vector2(position.X - 34, position.Y + 50);
                case Cords.D:
                    return new Vector2(position.X + 34, position.Y + 50);
                default:
                    System.Console.WriteLine("Wektor (0,0) zwrocono.");
                    return new Vector2(0, 0);
            }
        }

        public Ship(Vector2 vector) {
            Vector2 temp = new Vector2(vector.X - 34, vector.Y - 50);
            this.position = temp;
        }


        public void SetVector(float X, float Y) {
            this.position = new Vector2(cordX - 34, cordY - 50); // -34, -50 to set ship at the center of coordinates
        }
        public void SetVector(Vector2 vector) {
            Vector2 tempVec = new Vector2(vector.X - 34, vector.Y - 50);
            this.position = tempVec;
        }

        public void shipUpdate(GameTime gameTime, Controller gameController) {
            KeyboardState state = Keyboard.GetState();
            //if((this.cordY + speed) <= graphics.PreferredBackBufferHeight && this.cordX + speed )
            if (gameController.inGame) {
                float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (state.IsKeyDown(Keys.Right))
                    this.cordX += speed * dt;
                if (state.IsKeyDown(Keys.Left))
                    this.cordX -= speed * dt;
                if (state.IsKeyDown(Keys.Up))
                    this.cordY -= speed * dt;
                if (state.IsKeyDown(Keys.Down))
                    this.cordY += speed * dt;

                SetVector(cordX, cordY);
            }
        }
    }
}
