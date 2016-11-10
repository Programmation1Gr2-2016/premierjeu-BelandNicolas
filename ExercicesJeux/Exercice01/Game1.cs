using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Exercice01
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        bool isDirection = false;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Rectangle fenetre;
        GameObject heros;
        GameObject ennemie;
        GameObject fireBall;

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
            fenetre = graphics.GraphicsDevice.Viewport.Bounds;
            fenetre.Width = graphics.GraphicsDevice.DisplayMode.Width;
            fenetre.Height = graphics.GraphicsDevice.DisplayMode.Height;
            //Chargement du héros
            heros = new GameObject();
            heros.estVivant = true;
            heros.vitesse = 5;
            heros.sprite = Content.Load<Texture2D>("Mario.png");
            heros.position = heros.sprite.Bounds;
            heros.position.X = fenetre.Right / 2;
            heros.position.Y = fenetre.Bottom / 2;
            //Chargement de l'ennemie
            ennemie = new GameObject();
            ennemie.estVivant = true;
            ennemie.vitesse = 5;
            ennemie.sprite = Content.Load<Texture2D>("EnnemieMonster.png");
            ennemie.position = ennemie.sprite.Bounds;
            //Chargement FireBall
            fireBall = new GameObject();
            fireBall.estVivant = true;
            fireBall.vitesse = 5;
            fireBall.sprite = Content.Load<Texture2D>("FireBall/fireball1.png");
            fireBall.position = fireBall.sprite.Bounds;

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
            #region MouvementHeros
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

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
            #endregion
            #region EnnemieMouvement
            if (isDirection == true)
            {
                ennemie.position.X += ennemie.vitesse;
            }
            else
            {
                ennemie.position.X -= ennemie.vitesse;
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
            if (heros.position.Intersects(fireBall.position))
            {
                heros.estVivant = false;
            }
        }
        protected void UpdateEnnemie()
        {
            if (ennemie.position.X < fenetre.Left)
            {
                isDirection = true;
            }
            else if(ennemie.position.X + ennemie.sprite.Bounds.Width > fenetre.Right)
            {
                isDirection = false;
            }
        }
        protected void UpdateFireBall()
        {
            if (fireBall.estVivant == true)
            {
                fireBall.position.Y += fireBall.vitesse;
            }
            else
            {
                fireBall.position = ennemie.position;
                fireBall.estVivant = true;
            }
            if (fireBall.position.Y + fireBall.sprite.Bounds.Height > fenetre.Bottom)
            {
                fireBall.estVivant = false;
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
            spriteBatch.Draw(ennemie.sprite, ennemie.position, Color.White);
            spriteBatch.Draw(fireBall.sprite, fireBall.position, Color.White);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
