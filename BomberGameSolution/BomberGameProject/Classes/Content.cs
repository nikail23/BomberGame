using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace BomberGame.Classes
{
    public class ContentHandler
    {
        public static readonly string TexturesFolderPath = Directory.GetCurrentDirectory() + "\\Textures\\";

        public static Texture texture;

        public static void Load()
        {
            try 
            {
                texture = new Texture(TexturesFolderPath + "tiles.png");
            }
            catch
            {
                Debug.Write("Textures not founded!");
            }
        }
    }
}
