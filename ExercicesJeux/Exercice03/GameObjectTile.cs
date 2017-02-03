using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Exercice03
{
    class GameObjectTile
    {
        //Le nombre total de tuiles pour les lignes qui entrent dans l'écran
        public const int LIGNE = 16;
        //Le nombre total de tuiles par colonne dans un écran
        public const int COLONNE = 20;
        //Dimensions d'une tuile
        public const int LARGEUR_TUILE = 128;
        public const int HAUTEUR_TUILE = 128;
        public const int HAUTEUR_STATS_ZONE = 380;

        //La texture tileLayer
        public Texture2D texture;
        public Texture2D texturePorte;
        public Vector2 position;

        public TilesSet.Door tileDoor;

        public Rectangle rectSource = new Rectangle(0, 0, 128, 128);

        //1 = pierre, 2 = roche, 3 = gazon, 4 = fin, 5 = zone mortel
        public Rectangle rectPierre = new Rectangle(128, 0, 128, 128);
        public Rectangle rectRoche = new Rectangle(384, 0, 128, 128);
        public Rectangle rectGazon = new Rectangle(0, 0, 128, 128);
        public Rectangle rectEnd = new Rectangle(256, 0, 128, 128);
        public Rectangle rectDanger = new Rectangle(522, 0, 128, 128);
        public Rectangle rectPorte = new Rectangle(0, 0, 32, 32);

        public virtual void GenerateTiles()
        {
            tileDoor = new TilesSet.Door(texturePorte, false);
        }

        //La carte est affiché
        public int[,] map =
        {
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
            {1,3,2,1,1,2,3,3,3,1,1,3,1,3,3,1,1,1,1,1 },
            {1,3,1,1,1,3,3,1,3,3,1,3,2,1,3,1,3,1,1,1 },
            {1,3,3,3,3,5,2,1,1,3,3,3,3,3,2,1,3,1,1,1 },
            {1,2,3,1,1,3,2,1,1,1,1,5,1,3,1,1,3,3,1,1 },
            {1,1,3,1,1,3,1,1,1,1,1,1,1,3,1,1,3,1,1,1 },
            {1,1,3,1,1,3,3,3,3,3,3,1,1,3,2,1,5,1,1,1 },
            {1,1,1,1,1,1,1,2,1,1,3,1,3,5,3,3,3,1,1,1 },
            {1,1,2,1,3,3,3,5,3,3,3,1,3,1,1,1,3,2,1,1 },
            {1,1,1,1,3,1,1,1,1,3,1,2,2,1,1,3,3,2,1,1 },
            {1,1,2,1,6,1,1,2,1,3,1,1,1,3,3,3,1,1,1,1 },
            {1,3,5,3,3,3,3,1,1,3,3,5,3,3,1,5,3,3,1,1 },
            {1,3,1,1,1,1,3,1,1,1,2,1,1,1,1,1,1,5,1,1 },
            {1,3,5,3,3,1,3,3,3,3,1,3,1,3,1,1,1,1,1,1 },
            {1,1,1,1,3,3,1,2,1,3,3,5,3,3,3,3,3,3,4,1 },
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 }
        };


        //On dessine la carte
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < LIGNE; i++)
            {
                position.Y = (i * HAUTEUR_TUILE);
                for (int j = 0; j < COLONNE; j++)
                {
                    position.X = (j * LARGEUR_TUILE);

                    switch (map[i, j])
                    {
                        case 1:
                            spriteBatch.Draw(texture, position, rectPierre, Color.White);
                            break;
                        case 2:
                            spriteBatch.Draw(texture, position, rectRoche, Color.White);
                            break;
                        case 3:
                            spriteBatch.Draw(texture, position, rectGazon, Color.White);
                            break;
                        case 4:
                            spriteBatch.Draw(texture, position, rectEnd, Color.White);
                            break;
                        case 5:
                            spriteBatch.Draw(texture, position, rectDanger, Color.White);
                            break;
                        case 6:
                            spriteBatch.Draw(tileDoor.Texture, new Rectangle((int)position.X,(int)position.Y, rectSource.Width,rectSource.Height), rectPorte, Color.White);
                            
                            break;
                    }
                }
            }
        }
    }
}
