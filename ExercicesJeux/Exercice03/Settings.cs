using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercice03
{
    class Settings
    {
        // STATIC FIELD
        public static int SCREEN_WIDTH = 1920;
        public static int SCREEN_HEIGHT = 1920;
        public static int SCREEN_WIDTH_TILES = GameObjectTile.COLONNE * GameObjectTile.LARGEUR_TUILE;
        public static int SCREEN_HEIGHT_TILES = GameObjectTile.LIGNE * GameObjectTile.HAUTEUR_TUILE + GameObjectTile.HAUTEUR_STATS_ZONE;

        public static float PIXEL_RATIO = 0.3f;

        public static bool IS_FULLSCREEN = false;
        public static bool IS_MOUSE_VISIBLE = false;
        public static bool BY_TILE_DIMENSION = true;

    }
}
