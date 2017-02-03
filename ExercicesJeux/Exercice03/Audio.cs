using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

namespace Exercice03
{
    class Audio
    {
        // STATIC FIELD 
        public static Dictionary<string, Song> Song;

        // STATIC METHODS
        public static void LoadSounds(ContentManager content)
        {
            Song = new Dictionary<string, Song>();

            List<string> songsName = new List<string>()
            {
                "Fast_Lane"
            };

            foreach (string sfx in songsName)
            {
                Song.Add(sfx, content.Load<Song>("Sounds/" + sfx));
            }
        }
    }
}
