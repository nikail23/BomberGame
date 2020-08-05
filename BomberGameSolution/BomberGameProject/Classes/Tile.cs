using BomberGameProject.Classes;
using BomberGameProject.Classes.AbstractClasses;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading;

namespace BomberGame.Classes
{
    public enum TileType
    {
        INDESTRUCTIBLE_BLOCK,
        GRASS,
        DESTROYED_BLOCK
    }

    public class Tile : AnimatedGameObject
    {
        public static int TileSize = 16;

        private const int GrassTileHorizontalNumber = 0;
        private const int GrassTileVerticalNumber = 13;
        private const int IndestructibleTileHorizontalNumber = 3;
        private const int IndestructibleTileVerticalNumber = 3;
        private const int DestroyedTileHorizontalNumber = 4;
        private const int DestroyedTileVerticalNumber = 3;

        public TileType Type { get; private set; }

        private Thread animationThread;
        private bool isAnimating;

        public Tile(TileType tileType)
        {
            frames = new List<AnimationFrame>();
            animationThread = new Thread(Animate);
            rectangleShape = new RectangleShape(new Vector2f(TileSize, TileSize));

            SetTile(tileType);
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
                HandleStaticAnimation(350);
                if (currentFrameNumber >= frames.Count - 1)
                {
                    SetTile(TileType.GRASS);
                    isAnimating = false;
                    break;
                }
            }
        }
        
        private void SetTexture(TileType tileType)
        {
            switch (tileType)
            {
                case TileType.GRASS:
                    SetTexture(
                        ContentHandler.Texture,
                        GrassTileHorizontalNumber * TileSize,
                        GrassTileVerticalNumber * TileSize,
                        TileSize,
                        TileSize
                    );
                    break;
                case TileType.INDESTRUCTIBLE_BLOCK:
                    SetTexture(
                        ContentHandler.Texture,
                        IndestructibleTileHorizontalNumber * TileSize,
                        IndestructibleTileVerticalNumber * TileSize,
                        TileSize,
                        TileSize
                    );
                    break;
                case TileType.DESTROYED_BLOCK:
                    SetTexture(
                        ContentHandler.Texture,
                        DestroyedTileHorizontalNumber * TileSize,
                        DestroyedTileVerticalNumber * TileSize,
                        TileSize,
                        TileSize
                    );
                    frames.Add(new AnimationFrame(new Point(5 * TileSize, 3 * TileSize), TileSize, TileSize));
                    frames.Add(new AnimationFrame(new Point(6 * TileSize, 3 * TileSize), TileSize, TileSize));
                    frames.Add(new AnimationFrame(new Point(7 * TileSize, 3 * TileSize), TileSize, TileSize));
                    frames.Add(new AnimationFrame(new Point(8 * TileSize, 3 * TileSize), TileSize, TileSize));
                    frames.Add(new AnimationFrame(new Point(9 * TileSize, 3 * TileSize), TileSize, TileSize));
                    frames.Add(new AnimationFrame(new Point(10 * TileSize, 3 * TileSize), TileSize, TileSize));
                    break;
            }
        }

        public void SetTile(TileType tileType)
        {
            Type = tileType;
            SetTexture(Type);
        }

        public void Destroy()
        {
            rectangleShape.Dispose();
            rectangleShape = null;
        }
    }
}
