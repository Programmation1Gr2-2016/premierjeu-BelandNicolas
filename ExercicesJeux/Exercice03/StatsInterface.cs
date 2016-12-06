using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Exercice03
{
    class StatsInterface
    {
        public Texture2D picture;
        public SpriteFont texteStats;

        Rectangle picFace = new Rectangle(42, 294, 32, 32);

        public virtual void Draw(SpriteBatch spriteBatch, GameObjectPlayer heros, GameTime gameTime)
        {
            spriteBatch.Draw(picture, new Rectangle(Settings.SCREEN_WIDTH_TILES / 20, Settings.SCREEN_HEIGHT_TILES - GameObjectTile.HAUTEUR_STATS_ZONE + ((GameObjectTile.HAUTEUR_STATS_ZONE - 300) / 2), 300, 300), picFace, Color.White);
            spriteBatch.DrawString(texteStats, "Vie : " + heros.vie, new Vector2(Settings.SCREEN_WIDTH_TILES / 4, Settings.SCREEN_HEIGHT_TILES - GameObjectTile.HAUTEUR_STATS_ZONE / 2 - 100), Color.White);
            spriteBatch.DrawString(texteStats, "Temps : " + heros.tempsNiveau, new Vector2(Settings.SCREEN_WIDTH_TILES / 4, Settings.SCREEN_HEIGHT_TILES - GameObjectTile.HAUTEUR_STATS_ZONE / 2 + 30), Color.White);
            spriteBatch.DrawString(texteStats, "Record : " + heros.tempsRecord, new Vector2(Settings.SCREEN_WIDTH_TILES / 1.7f, Settings.SCREEN_HEIGHT_TILES - GameObjectTile.HAUTEUR_STATS_ZONE / 2 - 100), Color.White);
            spriteBatch.DrawString(texteStats, "Nombre essaie : " + heros.nombreEssaie, new Vector2(Settings.SCREEN_WIDTH_TILES / 1.7f, Settings.SCREEN_HEIGHT_TILES - GameObjectTile.HAUTEUR_STATS_ZONE / 2 + 30), Color.White);
        }
    }
}
