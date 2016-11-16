using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Exercice02
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        public static int TOTAL_TIME_FRAME = 30;
        public int timeByFrame = TOTAL_TIME_FRAME / 3;

        const int NBENEMIE = 3;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Rectangle fenetre;
        GameObject heros;
        GameObject[] ennemies;
        Random randomMouvement;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            this.graphics.PreferredBackBufferWidth = graphics.GraphicsDevice.DisplayMode.Width;
            this.graphics.PreferredBackBufferHeight = graphics.GraphicsDevice.DisplayMode.Height;

            this.graphics.ApplyChanges();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            randomMouvement = new Random();

            fenetre = graphics.GraphicsDevice.Viewport.Bounds;
            fenetre.Width = graphics.GraphicsDevice.DisplayMode.Width;
            fenetre.Height = graphics.GraphicsDevice.DisplayMode.Height;
            //Chargement du héros
            heros = new GameObject();
            heros.estVivant = true;
            heros.vitesse = 5;
            heros.sprite = Content.Load<Texture2D>("goodFighter.png");
            heros.position = heros.sprite.Bounds;
            heros.position.X = fenetre.Right / 2;
            heros.position.Y = fenetre.Bottom / 2;
            //Chargement de l'ennemie
            ennemies = new GameObject[NBENEMIE];
            for (int i = 0; i < ennemies.Length; i++)
            {
                ennemies[i] = new GameObject();
                ennemies[i].estVivant = true;
                ennemies[i].vitesse = 10;
                ennemies[i].sprite = Content.Load<Texture2D>("EnnemieMonster.png");
                ennemies[i].position = ennemies[i].sprite.Bounds;
                ennemies[i].position.X = randomMouvement.Next(0,40);
                ennemies[i].position.Y = randomMouvement.Next(0, 40);

                //Fireball de chaque ennemies
                ennemies[i].timeFrame = 0;
                ennemies[i].estVivantAttaque = false;
                ennemies[i].vitesseAttaque = 10;
                ennemies[i].sprites = Content.Load<Texture2D>("FireBall/fireball1.png");
                ennemies[i].sprites1 = Content.Load<Texture2D>("FireBall/fireball1.png");
                ennemies[i].sprites2 = Content.Load<Texture2D>("FireBall/fireball2.png");
                ennemies[i].sprites3 = Content.Load<Texture2D>("FireBall/fireball3.png");
                ennemies[i].positionAttaque = ennemies[i].sprites.Bounds;
            }
            
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            #region MouvementHeros
            if (heros.estVivant == true)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.D))
                {
                    heros.position.X += heros.vitesse;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.A))
                {
                    heros.position.X -= heros.vitesse;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.W))
                {
                    heros.position.Y -= heros.vitesse;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.S))
                {
                    heros.position.Y += heros.vitesse;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    //Attaque du vaisseau
                }
            }
            #endregion
            #region EnnemieMouvement
            for (int i = 0; i < ennemies.Length; i++)
            {
                if (ennemies[i].isDirection == true)
                {
                    ennemies[i].position.X += ennemies[i].vitesse;
                }
                else
                {
                    ennemies[i].position.X -= ennemies[i].vitesse;
                } 
            }
            #endregion
            if (Keyboard.GetState().IsKeyDown(Keys.R))
            {
                heros.estVivant = true;
            }
            // TODO: Add your update logic here
            UpdateHeros();
            UpdateEnnemie();
            UpdateFireBall();
            AnimatedFireBall();
            RandomMouvementEnnemie();
            base.Update(gameTime);
        }
        protected void UpdateHeros()
        {
            if (heros.position.X < fenetre.Left)
            {
                heros.position.X = fenetre.Left;
            }
            if (heros.position.X + heros.sprite.Bounds.Width > fenetre.Right)
            {
                heros.position.X = fenetre.Right - heros.sprite.Bounds.Width;
            }
            if (heros.position.Y < fenetre.Top)
            {
                heros.position.Y = fenetre.Top;
            }
            if (heros.position.Y + heros.sprite.Bounds.Height > fenetre.Bottom)
            {
                heros.position.Y = fenetre.Bottom - heros.sprite.Bounds.Height;
            }
            for (int i = 0; i < ennemies.Length; i++)
            {
                if (heros.position.Intersects(ennemies[i].positionAttaque))
                {
                    heros.estVivant = false;
                } 
            }
        }
        protected void UpdateEnnemie()
        {
            for (int i = 0; i < ennemies.Length; i++)
            {
                if (ennemies[i].position.X < fenetre.Left)
                {
                    ennemies[i].isDirection = true;
                }
                else if (ennemies[i].position.X + ennemies[i].sprite.Bounds.Width > fenetre.Right)
                {
                    ennemies[i].isDirection = false;
                } 
            }
        }
        protected void UpdateFireBall()
        {
            for (int i = 0; i < ennemies.Length; i++)
            {
                if (ennemies[i].estVivantAttaque == true)
                {
                    ennemies[i].positionAttaque.Y += ennemies[i].vitesseAttaque;
                }
                else
                {
                    ennemies[i].positionAttaque = ennemies[i].position;
                    ennemies[i].estVivantAttaque = true;
                }
                if (ennemies[i].positionAttaque.Y + ennemies[i].sprites.Bounds.Height > fenetre.Bottom)
                {
                    ennemies[i].estVivantAttaque = false;
                } 
            }
        }
        protected void AnimatedFireBall()
        {
            for (int i = 0; i < ennemies.Length; i++)
            {
                if (ennemies[i].timeFrame < timeByFrame)
                {
                    ennemies[i].sprites = ennemies[i].sprites1;
                }
                else if (ennemies[i].timeFrame < timeByFrame * 2)
                {
                    ennemies[i].sprites = ennemies[i].sprites2;
                }
                else if (ennemies[i].timeFrame < timeByFrame * 3)
                {
                    ennemies[i].sprites = ennemies[i].sprites3;
                }

                ennemies[i].timeFrame++;
                if (ennemies[i].timeFrame == TOTAL_TIME_FRAME)
                {
                    ennemies[i].timeFrame = 0;
                } 
            }
        }
        protected void RandomMouvementEnnemie()
        {
            for (int i = 0; i < ennemies.Length; i++)
            {
                int voyeur = randomMouvement.Next(0, 150);

                if (voyeur == 50)
                {
                    ennemies[i].isDirection = !ennemies[i].isDirection;
                } 
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            if (heros.estVivant == true)
            {
                spriteBatch.Draw(heros.sprite, heros.position, Color.White);
            }           

            for (int i = 0; i < ennemies.Length; i++)
            {
                spriteBatch.Draw(ennemies[i].sprite, ennemies[i].position, Color.White);
                spriteBatch.Draw(ennemies[i].sprites, ennemies[i].positionAttaque, Color.White);
            }
            

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
