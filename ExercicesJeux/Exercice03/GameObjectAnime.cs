using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Exercice03
{
    class GameObjectAnime
    {
        public Texture2D sprite;
        public Vector2 vitesse;
        public Vector2 direction;
        public Rectangle position;
        public Rectangle spriteAfficher; //Le rectangle affiché à l'écran

        public enum etats
        {
            attenteDroite,
            attenteGauche,
            attenteFace,
            attenteArriere,
            runDroite,
            runGauche,
            runFace,
            runArriere
        }
        public etats objetState;


        //Compteur qui changera le sprite affiché
        private int cpt = 0;

        //GESTION DES TABLEAUX DE SPRITES (chaque sprite est un rectangle dans le tableau)
        int runState = 0; //État de départ
        int nbEtatRun = 3; //Combien il y a de rectangles pour l’état “courrir”


        public Rectangle[] tabRunDroite = {
            new Rectangle(69, 2, 17, 23),
            new Rectangle(86, 2, 17, 23),
            new Rectangle(103, 2, 17, 23),
            };


        public Rectangle[] tabRunGauche = {
            new Rectangle(179, 2, 17, 23),
            new Rectangle(198, 2, 17, 23),
            new Rectangle(216, 2, 17, 23)
             };

        public Rectangle[] tabRunArriere =
        {
            new Rectangle(14, 2, 17, 23),
            new Rectangle(23, 2, 17, 23),
            new Rectangle(48, 2, 17, 23),
        };

        int waitState = 0;
        public Rectangle[] tabAttenteDroite =
        {
            new Rectangle(69, 2, 17, 23)
        };


        public Rectangle[] tabAttenteGauche =
        {
            new Rectangle(179, 2, 17, 23)
        };

        //METHODS

        public virtual void Update(GameTime gameTime)
        {
            if (objetState == etats.attenteDroite)
            {
                spriteAfficher = tabAttenteDroite[waitState];
            }
            if (objetState == etats.attenteGauche)
            {
                spriteAfficher = tabAttenteGauche[waitState];
            }
            if (objetState == etats.runDroite)
            {
                spriteAfficher = tabRunDroite[runState];
            }
            if (objetState == etats.runGauche)
            {
                spriteAfficher = tabRunGauche[runState];
            }

            //Compteur permettant de gérer le changement d'images
            cpt++;
            if (cpt == 6) //Vitesse défilement
            {
                //Gestion de la course
                runState++;
                if (runState == nbEtatRun)
                {
                    runState = 0;
                }
                cpt = 0;
            }
        }

    }
}
