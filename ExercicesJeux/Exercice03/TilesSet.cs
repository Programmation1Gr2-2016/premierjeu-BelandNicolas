using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
/*
 * Création de plusieurs objet pour avoir plus de contrôle sur les tiles
 */
namespace Exercice03
{
    public struct TilesSet
    {
        public class Door
        {
            public Texture2D Texture;
            public bool isOpen;
            public Rectangle collision;

            /*Choix de ce qu'on veut appliqué à l'instance créé
             * 
             */
            public Door(Texture2D sprite)
            {
                Texture = sprite;
            }
            public Door(Texture2D sprite, bool ouverture)
            {
                Texture = sprite;
                isOpen = ouverture;
            }
            public Door(Texture2D sprite, bool ouverture, Rectangle collider)
            {
                Texture = sprite;
                isOpen = ouverture;
                collision = collider;
            }

            

        }
    }
}
