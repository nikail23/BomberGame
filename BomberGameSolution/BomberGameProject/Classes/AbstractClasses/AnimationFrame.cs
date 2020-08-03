using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace BomberGameProject.Classes.AbstractClasses
{
    public class AnimationFrame
    {
        public Point Coordinates { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }

        public AnimationFrame(Point coordinates, int width, int height)
        {
            Coordinates = coordinates;
            Width = width;
            Height = height;
        }
    }
}
