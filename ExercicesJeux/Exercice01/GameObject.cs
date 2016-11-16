using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Exercice01
{
    class GameObject
    {
        //CARACTÉRISTIQUE
        public Rectangle position;
        public int vitesse;
        public Texture2D sprite;
        public bool estVivant;
        public Random randomMouvement;

        //ANIMATION
        public Texture2D sprites;
        public Texture2D sprites1;
        public Texture2D sprites2;
        public Texture2D sprites3;
        public int timeFrame;
    }
}
