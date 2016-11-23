using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace Exercice02
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        public static int TOTAL_TIME_FRAME = 30;
        public int timeByFrame = TOTAL_TIME_FRAME / 3;
        int timerScore = 0;
        int timeGame = 0;
        const int SCREEN_WIDTH = 1200;
        const int SCREEN_HEIGHT = 700;

        const int NBENEMIE = 3;
        int nombreMeteor = 0;
        string messageGameOver = "*********************************\n*                  Game Over !!!                  *\n*********************************";
        string messageTimeGame = "";
        string messageTimeScore = "";

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D backGround;
        Rectangle fenetre;
        GameObject heros;
        GameObject[] meteors;
        Random randomMouvement;

        SoundEffect son;
        SoundEffectInstance bombe;
        Song song;

        SpriteFont timerScoreSpriteFront;
        SpriteFont gameOver;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.graphics.PreferredBackBufferWidth = SCREEN_WIDTH;
            this.graphics.PreferredBackBufferHeight = SCREEN_HEIGHT;
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

            //Sounds 
            son = Content.Load<SoundEffect>("Sounds\\Bombe");
            bombe = son.CreateInstance();
            song = Content.Load<Song>("Sounds\\TechnoMusic");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0.1f;
            MediaPlayer.Play(song);

            //Texte
            timerScoreSpriteFront = Content.Load<SpriteFont>("TimerScore");
            gameOver = Content.Load<SpriteFont>("GameOver");

            //BackGround
            backGround = Content.Load<Texture2D>("bg_1_1.png");
            randomMouvement = new Random();

            //Fenêtre
            fenetre = graphics.GraphicsDevice.Viewport.Bounds;
            fenetre.Width = SCREEN_WIDTH;
            fenetre.Height = SCREEN_HEIGHT;

            //Chargement du héros
            heros = new GameObject();
            heros.estVivant = true;
            heros.vitesse = 7;
            heros.sprite = Content.Load<Texture2D>("goodFighter.png");
            heros.position = heros.sprite.Bounds;
            heros.position.X = fenetre.Right / 2;
            heros.position.Y = fenetre.Bottom / 2;
            heros.angle = 0;

            //Chargement meteors
            meteors = new GameObject[NBENEMIE];
            for (int i = 0; i < meteors.Length; i++)
            {
                meteors[i] = new GameObject();
                meteors[i].estVivant = false;
                meteors[i].vitesse = 5;
                meteors[i].sprite = Content.Load<Texture2D>("Meteor.png");
                meteors[i].position = meteors[i].sprite.Bounds;
                meteors[i].position.X = randomMouvement.Next(0, SCREEN_WIDTH - 100);
                meteors[i].position.Y = randomMouvement.Next(0, SCREEN_HEIGHT - 100);
                meteors[i].direction.X = randomMouvement.Next(-4, 5);
                meteors[i].direction.Y = randomMouvement.Next(-4, 5);
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
            timeGame = gameTime.TotalGameTime.Seconds + gameTime.TotalGameTime.Minutes * 60;
            messageTimeGame = "Temps de jeu : " + timeGame + " seconde";
            if (nombreMeteor * 10 < gameTime.TotalGameTime.Seconds && nombreMeteor < NBENEMIE)
            {
                //Spawn meteor pour chacun
                meteors[nombreMeteor].estVivant = true;
                nombreMeteor++;
            }

            #region MouvementHeros
            if (heros.estVivant == true)
            {
                //Faire bouger le héros quand il est vivant
                if (Keyboard.GetState().IsKeyDown(Keys.D))
                {
                    heros.angle += 0.1f;
                    if (RadianToDegree(heros.angle) > 360.0)
                    {
                        heros.angle = 0.0f;
                    }
                    else if (RadianToDegree(heros.angle) <= 0.0f)
                    {
                        heros.angle = (float)DegreeToRadian(360.0f);
                    }
                }
                if (Keyboard.GetState().IsKeyDown(Keys.A))
                {
                    heros.angle -= 0.1f;
                    if (RadianToDegree(heros.angle) > 360.0)
                    {
                        heros.angle = 0.0f;
                    }
                    else if (RadianToDegree(heros.angle) <= 0.0f)
                    {
                        heros.angle = (float)DegreeToRadian(360.0f);
                    }
                }
                if (Keyboard.GetState().IsKeyDown(Keys.W))
                {
                    heros.position.Y -= (int)ComposanteX(heros.angle, heros.vitesse);
                    heros.position.X += (int)ComposanteY(heros.angle, heros.vitesse);
                }
                if (Keyboard.GetState().IsKeyDown(Keys.S))
                {
                    heros.position.Y += (int)ComposanteX(heros.angle, heros.vitesse);
                    heros.position.X -= (int)ComposanteY(heros.angle, heros.vitesse);
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.R))
            {
                //Réaparaitre quand on est mort
                heros.estVivant = true;
            }
            #endregion
            #region EnnemieMouvement
            for (int i = 0; i < meteors.Length; i++)
            {
                //Calcule du déplacement pour chaque meteors
                meteors[i].position.X += (int)meteors[i].direction.X;
                meteors[i].position.Y += (int)meteors[i].direction.Y;
            }
            #endregion
            #region HerosUpdate
            //Détermine collision des bords de fenêtre
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
            //Détecte Collision avec meteors
            if (heros.estVivant == true)
            {
                for (int i = 0; i < meteors.Length; i++)
                {
                    if (heros.position.Intersects(meteors[i].position) && meteors[i].estVivant == true)
                    {
                        //Mort si il touche et calcule du temps écoulé
                        heros.estVivant = false;
                        bombe.Play();
                        timerScore = gameTime.TotalGameTime.Seconds + gameTime.TotalGameTime.Minutes * 60;
                        messageTimeScore = "Tu as survecu : " + timerScore + " seconde";
                    }
                }
            }
            #endregion
            // TODO: Add your update logic here
            UpdateEnnemie();
            base.Update(gameTime);
        }
        protected void UpdateEnnemie()
        {
            for (int i = 0; i < meteors.Length; i++)
            {
                //Détection des bords de la fenêtre et inversement de direction
                if (meteors[i].position.X < fenetre.Left)
                {
                    meteors[i].direction.X = -meteors[i].direction.X;
                }
                else if (meteors[i].position.X + meteors[i].sprite.Bounds.Width > fenetre.Right)
                {
                    meteors[i].direction.X = -meteors[i].direction.X;
                }
                if (meteors[i].position.Y < fenetre.Top)
                {
                    meteors[i].direction.Y = -meteors[i].direction.Y;
                }
                else if (meteors[i].position.Bottom > fenetre.Bottom)
                {
                    meteors[i].direction.Y = -meteors[i].direction.Y;
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
            spriteBatch.Draw(backGround, backGround.Bounds, Color.White);

            if (heros.estVivant == true)
            {
                //Dessine le héros si il est vivant
                spriteBatch.Draw(heros.sprite, new Rectangle(heros.position.X + heros.sprite.Bounds.Width / 2, heros.position.Y + heros.sprite.Bounds.Height / 2, heros.sprite.Bounds.Width, heros.sprite.Bounds.Height), null, Color.White, heros.angle, new Vector2(heros.sprite.Bounds.Width / 2, heros.sprite.Bounds.Height / 2), SpriteEffects.None, 0);
            }

            for (int i = 0; i < meteors.Length; i++)
            {
                if (meteors[i].estVivant == true)
                {
                    //Dessine chacun des meteors si ils sont vivant
                    spriteBatch.Draw(meteors[i].sprite, meteors[i].position, Color.White);
                }
            }
            if (heros.estVivant == false)
            {
                //Dessine le texte avec le temps écouler quand le héros est mort
                spriteBatch.DrawString(timerScoreSpriteFront, messageTimeScore, new Vector2(fenetre.Width / 2 - timerScoreSpriteFront.MeasureString(messageTimeScore).X / 2, fenetre.Height / 2 + gameOver.MeasureString(messageGameOver).Y), Color.White);
                spriteBatch.DrawString(gameOver, messageGameOver, new Vector2((fenetre.Width / 2 - gameOver.MeasureString(messageGameOver).X / 2), (fenetre.Height / 2 - gameOver.MeasureString(messageGameOver).Y / 2)), Color.White);
            }
            else
            {
                spriteBatch.DrawString(timerScoreSpriteFront, messageTimeGame, new Vector2(fenetre.Width / 10, fenetre.Height / 10), Color.White);
            }
            spriteBatch.DrawString(timerScoreSpriteFront, "Orientation : " + RadianToDegree(heros.angle).ToString() + "\nValeur X : " + heros.position.X + "\nValeur Y : " + heros.position.Y, new Vector2(200, 200), Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
        private double DegreeToRadian(double angle)
        {
            return Math.PI * angle / 180.0;
        }
        private double RadianToDegree(double angle)
        {
            return angle * (180.0 / Math.PI);
        }
        private double ComposanteX(double angle, double grandeur)
        {
            //Donne la composante X avec COS pour la direction
            return grandeur * Math.Cos(angle);
        }
        private double ComposanteY(double angle, double grandeur)
        {
            return grandeur * Math.Sin(angle);
        }
    }
}
