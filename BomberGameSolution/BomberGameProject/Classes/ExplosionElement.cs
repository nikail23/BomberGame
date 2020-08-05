using BomberGame.Classes;
using BomberGameProject.Classes.AbstractClasses;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;

namespace BomberGameProject.Classes
{
    public enum ExplosionElementType
    {
        CENTER,
        LEFT_CONTINIOUS,
        RIGHT_CONTINIOUS,
        UP_CONTINIOUS,
        DOWN_CONTINIOUS,
        LEFT_ENDED,
        RIGHT_ENDED,
        UP_ENDED,
        DOWN_ENDED
    }

    public class ExplosionElement : AnimatedGameObject
    {
        public ExplosionElementType ExplosionElementType { get; private set; }
        public Point Coordinates { get; private set; }
        public bool isAnimating { get; private set; }

        private Thread animationThread;

        public ExplosionElement(ExplosionElementType explosionElementType, Point coordinates)
        {
            ExplosionElementType = explosionElementType;
            Coordinates = coordinates;
            animationThread = new Thread(Animate);
            animationThread.IsBackground = true;

            rectangleShape.Position = new Vector2f(coordinates.X * Tile.TileSize, coordinates.Y * Tile.TileSize);
            rectangleShape.Texture = ContentHandler.Texture;

            var frames = new List<AnimationFrame>();

            switch (explosionElementType)
            {
                case ExplosionElementType.CENTER:
                    //frames.Add(new AnimationFrame(new Point(2 * Tile.TileSize, 6 * Tile.TileSize), Tile.TileSize, Tile.TileSize));
                    //frames.Add(new AnimationFrame(new Point(7 * Tile.TileSize, 6 * Tile.TileSize), Tile.TileSize, Tile.TileSize));
                    frames.Add(new AnimationFrame(new Point(2 * Tile.TileSize, 11 * Tile.TileSize), Tile.TileSize, Tile.TileSize));
                    frames.Add(new AnimationFrame(new Point(7 * Tile.TileSize, 11 * Tile.TileSize), Tile.TileSize, Tile.TileSize));
                    break;
                case ExplosionElementType.LEFT_CONTINIOUS:
                    //frames.Add(new AnimationFrame(new Point(1 * Tile.TileSize, 6 * Tile.TileSize), Tile.TileSize, Tile.TileSize));
                    //frames.Add(new AnimationFrame(new Point(6 * Tile.TileSize, 6 * Tile.TileSize), Tile.TileSize, Tile.TileSize));
                    frames.Add(new AnimationFrame(new Point(1 * Tile.TileSize, 11 * Tile.TileSize), Tile.TileSize, Tile.TileSize));
                    frames.Add(new AnimationFrame(new Point(6 * Tile.TileSize, 11 * Tile.TileSize), Tile.TileSize, Tile.TileSize));
                    break;
                case ExplosionElementType.RIGHT_CONTINIOUS:
                    //frames.Add(new AnimationFrame(new Point(3 * Tile.TileSize, 6 * Tile.TileSize), Tile.TileSize, Tile.TileSize));
                    //frames.Add(new AnimationFrame(new Point(8 * Tile.TileSize, 6 * Tile.TileSize), Tile.TileSize, Tile.TileSize));
                    frames.Add(new AnimationFrame(new Point(3 * Tile.TileSize, 11 * Tile.TileSize), Tile.TileSize, Tile.TileSize));
                    frames.Add(new AnimationFrame(new Point(8 * Tile.TileSize, 11 * Tile.TileSize), Tile.TileSize, Tile.TileSize));
                    break;
                case ExplosionElementType.DOWN_CONTINIOUS:
                    //frames.Add(new AnimationFrame(new Point(2 * Tile.TileSize, 7 * Tile.TileSize), Tile.TileSize, Tile.TileSize));
                    //frames.Add(new AnimationFrame(new Point(7 * Tile.TileSize, 7 * Tile.TileSize), Tile.TileSize, Tile.TileSize));
                    frames.Add(new AnimationFrame(new Point(2 * Tile.TileSize, 12 * Tile.TileSize), Tile.TileSize, Tile.TileSize));
                    frames.Add(new AnimationFrame(new Point(7 * Tile.TileSize, 12 * Tile.TileSize), Tile.TileSize, Tile.TileSize));
                    break;
                case ExplosionElementType.UP_CONTINIOUS:
                    //frames.Add(new AnimationFrame(new Point(2 * Tile.TileSize, 5 * Tile.TileSize), Tile.TileSize, Tile.TileSize));
                    //frames.Add(new AnimationFrame(new Point(7 * Tile.TileSize, 5 * Tile.TileSize), Tile.TileSize, Tile.TileSize));
                    frames.Add(new AnimationFrame(new Point(2 * Tile.TileSize, 10 * Tile.TileSize), Tile.TileSize, Tile.TileSize));
                    frames.Add(new AnimationFrame(new Point(7 * Tile.TileSize, 10 * Tile.TileSize), Tile.TileSize, Tile.TileSize));
                    break;
                case ExplosionElementType.LEFT_ENDED:
                    //frames.Add(new AnimationFrame(new Point(0 * Tile.TileSize, 6 * Tile.TileSize), Tile.TileSize, Tile.TileSize));
                    //frames.Add(new AnimationFrame(new Point(5 * Tile.TileSize, 6 * Tile.TileSize), Tile.TileSize, Tile.TileSize));
                    frames.Add(new AnimationFrame(new Point(0 * Tile.TileSize, 11 * Tile.TileSize), Tile.TileSize, Tile.TileSize));
                    frames.Add(new AnimationFrame(new Point(5 * Tile.TileSize, 11 * Tile.TileSize), Tile.TileSize, Tile.TileSize));
                    break;
                case ExplosionElementType.RIGHT_ENDED:
                    //frames.Add(new AnimationFrame(new Point(4 * Tile.TileSize, 6 * Tile.TileSize), Tile.TileSize, Tile.TileSize));
                    //frames.Add(new AnimationFrame(new Point(9 * Tile.TileSize, 6 * Tile.TileSize), Tile.TileSize, Tile.TileSize));
                    frames.Add(new AnimationFrame(new Point(4 * Tile.TileSize, 11 * Tile.TileSize), Tile.TileSize, Tile.TileSize));
                    frames.Add(new AnimationFrame(new Point(9 * Tile.TileSize, 11 * Tile.TileSize), Tile.TileSize, Tile.TileSize));
                    break;
                case ExplosionElementType.DOWN_ENDED:
                    //frames.Add(new AnimationFrame(new Point(2 * Tile.TileSize, 8 * Tile.TileSize), Tile.TileSize, Tile.TileSize));
                    //frames.Add(new AnimationFrame(new Point(7 * Tile.TileSize, 8 * Tile.TileSize), Tile.TileSize, Tile.TileSize));
                    frames.Add(new AnimationFrame(new Point(2 * Tile.TileSize, 13 * Tile.TileSize), Tile.TileSize, Tile.TileSize));
                    frames.Add(new AnimationFrame(new Point(7 * Tile.TileSize, 13 * Tile.TileSize), Tile.TileSize, Tile.TileSize));
                    break;
                case ExplosionElementType.UP_ENDED:
                    //frames.Add(new AnimationFrame(new Point(2 * Tile.TileSize, 4 * Tile.TileSize), Tile.TileSize, Tile.TileSize));
                    //frames.Add(new AnimationFrame(new Point(7 * Tile.TileSize, 4 * Tile.TileSize), Tile.TileSize, Tile.TileSize));
                    frames.Add(new AnimationFrame(new Point(2 * Tile.TileSize, 9 * Tile.TileSize), Tile.TileSize, Tile.TileSize));
                    frames.Add(new AnimationFrame(new Point(7 * Tile.TileSize, 9 * Tile.TileSize), Tile.TileSize, Tile.TileSize));
                    break;
            }

            SetAnimation(frames);
        }

        public void StartAnimation()
        {
            isAnimating = true;
            animationThread.Start();
        }

        private void Animate()
        {
            while (isAnimating)
            {
                HandleStaticAnimation(300);
                if (currentFrameNumber >= frames.Count - 1)
                {
                    isAnimating = false;
                    break;
                }
            }
        }

        public void Destroy()
        {
            isAnimating = false;
            rectangleShape = null;
        }
    }
}
