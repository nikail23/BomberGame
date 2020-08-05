using BomberGame.Classes;
using BomberGameProject.Classes.AbstractClasses;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BomberGameProject.Classes
{
    public class Bomb : AnimatedGameObject
    {
        private const int BombTextureHorizontalNumber = 0;
        private const int BombTextureVerticalNumber = 3;
        private const int BombDetonationTime = 2;

        public event DeleteBombDelegate DeleteBombEvent;
        public event AddExplosionDelegate AddExplosionEvent;

        private Thread animationThread;
        private bool isAnimating;
        private int power;

        public Point Coordinates { get; private set; }

        public Bomb(Point coordinates, int power)
        {
            this.power = power;
            animationThread = new Thread(StartAnimation);
            animationThread.IsBackground = true;

            Coordinates = coordinates;
            rectangleShape.Position = new Vector2f(coordinates.X * Tile.TileSize, coordinates.Y * Tile.TileSize);

            SetTexture(
                ContentHandler.Texture,
                BombTextureHorizontalNumber * Tile.TileSize,
                BombTextureVerticalNumber * Tile.TileSize,
                Tile.TileSize,
                Tile.TileSize
            );

            var frames = new List<AnimationFrame>();
            frames.Add(new AnimationFrame(new Point(0, 3 * Tile.TileSize), Tile.TileSize, Tile.TileSize));
            frames.Add(new AnimationFrame(new Point(1 * Tile.TileSize, 3 * Tile.TileSize), Tile.TileSize, Tile.TileSize));
            frames.Add(new AnimationFrame(new Point(2 * Tile.TileSize, 3 * Tile.TileSize), Tile.TileSize, Tile.TileSize));
            SetAnimation(frames);
            isAnimating = true;

            animationThread.Start();

            Detonate(BombDetonationTime);
        }

        private void StartAnimation()
        {
            while (isAnimating)
            {
                HandleStaticAnimation(2000 / 3);
                if (currentFrameNumber >= frames.Count - 1)
                {
                    break;
                }
            }
        }

        private async void Detonate(int detonateTime) 
        {
            await Task.Run(()=>WaitForDetonatingTime(detonateTime));
            DeleteBombEvent(this);
            CreateExplosion();
            Destroy();
        }

        private void CreateExplosion()
        {
            AddExplosionEvent(Coordinates, power);
        }

        private void WaitForDetonatingTime(int detonateTime)
        {
            var clock = new Clock();
            clock.Restart();
            while (clock.ElapsedTime.AsSeconds() != detonateTime) { }
        }

        public void Destroy()
        {
            isAnimating = false;
            rectangleShape = null;
            animationThread = null;
        }
    }
}
