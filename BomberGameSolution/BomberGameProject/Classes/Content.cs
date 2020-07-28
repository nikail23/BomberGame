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

        public static Texture Texture;

        public static void Load()
        {
            try 
            {
                Texture = new Texture(TexturesFolderPath + "tiles.png");
            }
            catch
            {
                Debug.Write("Textures not founded!");
            }
        }
    }
}
