using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Exercice03
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        SpriteFont debugFont;
        string debugTexte = "";
        bool finNiveau = false;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        KeyboardState keys = new KeyboardState();
        KeyboardState previousKeys = new KeyboardState();
        Rectangle fenetre;

        Texture2D test;
        // Fond de tuiles
        GameObjectTile fond;
        Rectangle[,] tuile = new Rectangle[GameObjectTile.COLONNE, GameObjectTile.LIGNE];
        Rectangle[,] tuileDestroy = new Rectangle[GameObjectTile.COLONNE, GameObjectTile.LIGNE];
        Rectangle tuileEnd;
        Rectangle[,] tuileDanger = new Rectangle[GameObjectTile.COLONNE, GameObjectTile.LIGNE];
        GameObjectPlayer heros;
        StatsInterface statsInterface = new StatsInterface();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            if (Settings.BY_TILE_DIMENSION)
            {
                this.graphics.PreferredBackBufferWidth = (int)(Settings.SCREEN_WIDTH_TILES * Settings.PIXEL_RATIO);
                this.graphics.PreferredBackBufferHeight = (int)(Settings.SCREEN_HEIGHT_TILES * Settings.PIXEL_RATIO);
            }
            else
            {
                this.graphics.PreferredBackBufferWidth = (int)(Settings.SCREEN_WIDTH * Settings.PIXEL_RATIO);
                this.graphics.PreferredBackBufferHeight = (int)(Settings.SCREEN_HEIGHT * Settings.PIXEL_RATIO);
            }
            this.graphics.IsFullScreen = Settings.IS_FULLSCREEN;
            this.IsMouseVisible = Settings.IS_MOUSE_VISIBLE;
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
            fenetre = graphics.GraphicsDevice.Viewport.Bounds;
            fenetre.Width = Settings.SCREEN_WIDTH_TILES;
            fenetre.Height = Settings.SCREEN_HEIGHT_TILES;

            debugFont = Content.Load<SpriteFont>("DebugFont");
            test = Content.Load<Texture2D>("TileSet.png");
            // TODO: use this.Content to load your game content here
            fond = new GameObjectTile();
            fond.texture = Content.Load<Texture2D>("TileSet.png");
            statsInterface.picture = Content.Load<Texture2D>("Bomberman.png");
            statsInterface.texteStats = Content.Load<SpriteFont>("TexteInterface");


            heros = new GameObjectPlayer();
            heros.estVivant = true;
            heros.toucheFin = false;
            heros.tempsNiveau = 0f;
            heros.tempsRecord = 999;
            heros.vie = 100;
            heros.nombreEssaie = 1;
            heros.vitesse = new Vector2(5, 5);
            heros.direction = Vector2.Zero;
            heros.objetState = GameObjectPlayer.etats.attenteDroite;
            heros.position = new Rectangle(128, 126, 120, 128);   //Position initiale de Bomberman
            heros.collider = new Rectangle(heros.position.X + 2, heros.position.Y + 58, 116, 70);
            heros.lastPosition.X = heros.position.X;
            heros.lastPosition.Y = heros.position.Y;
            heros.sprite = Content.Load<Texture2D>("Bomberman.png");

            //GÉNÉRER LES BOITES DE COLLISION DE CHAQUE CASE

            #region Generate Tuiles
            for (int ligne = 0; ligne < fond.map.GetLength(0); ligne++)
            {
                for (int colonne = 0; colonne < fond.map.GetLength(1); colonne++)
                {
                    switch (fond.map[ligne, colonne])//1 = pierre, 2 = roche, 3 = gazon, 4 = fin, 5 = zone mortel
                    {
                        case 1:
                            tuile[colonne, ligne] = new Rectangle();
                            tuile[colonne, ligne].X = colonne * GameObjectTile.LARGEUR_TUILE;
                            tuile[colonne, ligne].Y = ligne * GameObjectTile.HAUTEUR_TUILE;
                            tuile[colonne, ligne].Width = GameObjectTile.LARGEUR_TUILE;
                            tuile[colonne, ligne].Height = GameObjectTile.HAUTEUR_TUILE;
                            break;
                        case 2:
                            tuileDestroy[colonne, ligne] = new Rectangle();
                            tuileDestroy[colonne, ligne].X = colonne * GameObjectTile.LARGEUR_TUILE;
                            tuileDestroy[colonne, ligne].Y = ligne * GameObjectTile.HAUTEUR_TUILE;
                            tuileDestroy[colonne, ligne].Width = GameObjectTile.LARGEUR_TUILE;
                            tuileDestroy[colonne, ligne].Height = GameObjectTile.HAUTEUR_TUILE;
                            break;
                        case 3:
                            break;
                        case 4:
                            tuileEnd = new Rectangle();
                            tuileEnd.X = colonne * GameObjectTile.LARGEUR_TUILE;
                            tuileEnd.Y = ligne * GameObjectTile.HAUTEUR_TUILE;
                            tuileEnd.Width = GameObjectTile.LARGEUR_TUILE;
                            tuileEnd.Height = GameObjectTile.HAUTEUR_TUILE;
                            break;
                        case 5:
                            tuileDanger[colonne, ligne] = new Rectangle();
                            tuileDanger[colonne, ligne].X = colonne * GameObjectTile.LARGEUR_TUILE;
                            tuileDanger[colonne, ligne].Y = ligne * GameObjectTile.HAUTEUR_TUILE;
                            tuileDanger[colonne, ligne].Width = GameObjectTile.LARGEUR_TUILE;
                            tuileDanger[colonne, ligne].Height = GameObjectTile.HAUTEUR_TUILE;
                            break;
                        default:
                            break;
                    }

                }
            }
            #endregion
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
            keys = Keyboard.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            #region Hero en vie

            if (heros.estVivant)
            {
                #region Mouvement Horizontale
                if (keys.IsKeyDown(Keys.D))
                {
                    heros.direction.X = 2;
                    heros.objetState = GameObjectPlayer.etats.runDroite;
                }
                if (keys.IsKeyUp(Keys.D) && previousKeys.IsKeyDown(Keys.D))
                {
                    heros.direction.X = 0;
                    heros.objetState = GameObjectPlayer.etats.attenteDroite;
                }
                if (keys.IsKeyDown(Keys.A))
                {
                    heros.direction.X = -2;
                    heros.objetState = GameObjectPlayer.etats.runGauche;
                }
                if (keys.IsKeyUp(Keys.A) && previousKeys.IsKeyDown(Keys.A))
                {
                    heros.direction.X = 0;
                    heros.objetState = GameObjectPlayer.etats.attenteGauche;
                }
                #endregion

                heros.position.X += (int)(heros.vitesse.X * heros.direction.X);
                heros.collider.X = heros.position.X + 2;
                #region Collision Horizontale
                foreach (Rectangle tuile in tuile)
                {
                    if (tuile.Intersects(heros.collider) && (heros.direction.X == 2 || heros.direction.X == -2))
                    {
                        heros.position.X = (int)heros.lastPosition.X;
                        heros.collider.X = heros.position.X + 2;
                    }
                }
                foreach (Rectangle tuileDestroy in tuileDestroy)
                {
                    if (tuileDestroy.Intersects(heros.collider) && (heros.direction.X == 2 || heros.direction.X == -2))
                    {
                        heros.position.X = (int)heros.lastPosition.X;
                        heros.collider.X = heros.position.X + 2;
                    }
                }

                if (heros.position.Left < fenetre.Left || heros.position.Right > fenetre.Right)//Bord de fenêtre
                {
                    heros.position.X = (int)heros.lastPosition.X;
                }
                #endregion

                #region Mouvement Vertical
                if (keys.IsKeyDown(Keys.W))
                {
                    heros.direction.Y = -2;
                    heros.objetState = GameObjectPlayer.etats.runHaut;
                }
                if (keys.IsKeyUp(Keys.W) && previousKeys.IsKeyDown(Keys.W))
                {
                    heros.direction.Y = 0;
                    heros.objetState = GameObjectPlayer.etats.attenteHaut;
                }
                if (keys.IsKeyDown(Keys.S))
                {
                    heros.direction.Y = 2;
                    heros.objetState = GameObjectPlayer.etats.runBas;
                }
                if (keys.IsKeyUp(Keys.S) && previousKeys.IsKeyDown(Keys.S))
                {
                    heros.direction.Y = 0;
                    heros.objetState = GameObjectPlayer.etats.attenteBas;
                }
                #endregion

                heros.position.Y += (int)(heros.vitesse.Y * heros.direction.Y);
                heros.collider.Y = heros.position.Y + 59;

                #region Collision Vertical
                foreach (Rectangle tuile in tuile)
                {
                    if (tuile.Intersects(heros.collider))
                    {
                        heros.position.Y = (int)heros.lastPosition.Y;
                        heros.collider.Y = heros.position.Y + 59;
                    }
                }
                foreach (Rectangle tuileDestroy in tuileDestroy)
                {
                    if (tuileDestroy.Intersects(heros.collider) && (heros.direction.Y == 2 || heros.direction.Y == -2))
                    {
                        heros.position.Y = (int)heros.lastPosition.Y;
                        heros.collider.Y = heros.position.Y + 59;
                    }
                }

                if (heros.position.Y < fenetre.Top || heros.position.Bottom > fenetre.Bottom) //Bord de fenêtre
                {
                    heros.position.Y = (int)heros.lastPosition.Y;
                }
                #endregion

                foreach (Rectangle tuileDanger in tuileDanger)
                {
                    if (tuileDanger.Intersects(heros.collider))
                    {
                        heros.vie--;
                    }
                }

                if (heros.vie <= 0) //heros meurt pus de vie
                {
                    heros.estVivant = false;
                }
                if (tuileEnd.Intersects(heros.collider) && finNiveau == false) //Arrive à la fin
                {
                    heros.toucheFin = true;
                    finNiveau = true;
                    heros.nombreEssaieReussi++;
                    if (heros.tempsNiveau < heros.tempsRecord)
                    {
                        heros.tempsRecord = heros.tempsNiveau;
                    }
                }
                else if (finNiveau == true && keys.IsKeyDown(Keys.T))
                {
                    heros.position.X = 128;
                    heros.position.Y = 126;
                    finNiveau = false;
                    heros.toucheFin = false;
                    heros.tempsNiveau = 0f;
                    heros.vie = 100;
                    heros.nombreEssaie++;
                }
            }
            #endregion
            #region Heros est mort
            else
            {
                if (keys.IsKeyDown(Keys.R)) //heros revie
                {
                    heros.position.X = 128;
                    heros.position.Y = 128;
                    heros.vie = 100;
                    heros.estVivant = true;
                    heros.tempsNiveau = 0f;
                    heros.nombreEssaie++;
                }
            }
            #endregion
            // TODO: Add your update logic here

            if (finNiveau == false && heros.estVivant == true)
            {
                heros.tempsNiveau += (float)gameTime.ElapsedGameTime.TotalSeconds; 
            }

            heros.Update(gameTime);
            previousKeys = keys;
            heros.lastPosition.X = heros.position.X;
            heros.lastPosition.Y = heros.position.Y;
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkGray);

            // TODO: Add your drawing code here
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, Matrix.CreateScale(Settings.PIXEL_RATIO));


            fond.Draw(spriteBatch);
            if (heros.estVivant)
            {
                spriteBatch.Draw(heros.sprite, heros.position, heros.spriteAfficher, Color.White);
            }
            else
            {
                spriteBatch.DrawString(statsInterface.texteStats, "Vous etes mort,\n appuyer sur R pour recommencer", new Vector2(fenetre.Center.X - 700, fenetre.Center.Y - 200), Color.White);
            }
            if (heros.toucheFin == true)
            {
                spriteBatch.DrawString(statsInterface.texteStats, "Reussi!\nTemps : " + heros.tempsNiveau + "\nRecord : " + heros.tempsRecord + "\nNombre essaie : " + heros.nombreEssaie + "\nNombre reussi : " + heros.nombreEssaieReussi, new Vector2(fenetre.Center.X - 500, fenetre.Center.Y - 200), Color.White);
            }

            spriteBatch.DrawString(debugFont, debugTexte, new Vector2(100, 100), Color.White);
            //foreach (var tuile in tuile)
            //{
            //    spriteBatch.Draw(debugFont.Texture, tuile, Color.Red);
            //}
            statsInterface.Draw(spriteBatch, heros, gameTime);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}