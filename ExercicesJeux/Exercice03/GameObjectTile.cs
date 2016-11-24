using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Exercice03
{
    class GameObjectTile
    {
        //Le nombre total de tuiles pour les lignes qui entrent dans l'écran
        public const int LIGNE = 10;
        //Le nomnbre total de tuiles par colonne dans un écran
        public const int COLONNE = 17;

        //La texture tileLayer
        public Texture2D sprite;
        public Vector2 position;

        public Rectangle rectSource = new Rectangle(0, 0, 48 * 2, 48 * 2);

        //1 = pierre, 2 = gazon 3 = sable (voir image tiles1.jpg)        
        public Rectangle rectPierre = new Rectangle(128, 0, 128, 128);
        public Rectangle rectGazon = new Rectangle(256, 128, 128, 128);
        public Rectangle rectSable = new Rectangle(256, 0, 128, 128);

        //La carte qui est affichée
        public int[,] map = {
                            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                            {1,1,1,2,2,1,1,1,1,1,1,1,1,3,1,1,1},
                            {1,1,1,2,2,1,1,1,1,1,1,1,1,1,1,1,1},
                            {1,1,1,1,1,1,1,1,3,1,1,1,1,1,1,1,1},
                            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                            {1,1,1,2,1,1,1,1,1,1,1,1,1,1,1,1,1},
                            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                            {1,1,1,1,3,1,1,1,1,1,1,1,1,1,1,1,1},
                            };


        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            for (int i = 0; i < LIGNE; i++)
            {
                rectSource.Y = (i * 48 * 2);
                for (int j = 0; j < COLONNE; j++)
                {
                    this.rectSource.X = (j * 48 * 2);
                    if (map[i, j] == 1)
                        spriteBatch.Draw(sprite, rectSource, rectPierre, Color.White);
                    if (map[i, j] == 2)
                        spriteBatch.Draw(sprite, rectSource, rectGazon, Color.White);
                    if (map[i, j] == 3)
                        spriteBatch.Draw(sprite, rectSource, rectSable, Color.White);
                }
            }
        }
        }
}
