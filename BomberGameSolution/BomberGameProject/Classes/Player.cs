using BomberGame.Classes;
using BomberGameProject.Classes.AbstractClasses;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace BomberGameProject.Classes
{
    public enum TilePositionType
    {
        LEFT,
        RIGHT,
        UP,
        DOWN
    }

    public class Player : AnimatedGameObject
    {
        private const int PlayerTextureHorizontalNumber = 4;
        private const int PlayerTextureVerticalNumber = 0;

        public event AddBombDelegate AddBombEvent;

        private const float PlayerMovementSpeed = 0.8f;

        public Vector2f StartPosition;
        private Vector2f movement;
        private int maxActiveBombsCount;

        private GameBoard gameBoard;

        public Player(GameBoard gameBoard)
        {
            SetTexture(
                ContentHandler.Texture,
                PlayerTextureHorizontalNumber * Tile.TileSize,
                PlayerTextureVerticalNumber * Tile.TileSize,
                Tile.TileSize,
                Tile.TileSize
            );

            this.gameBoard = gameBoard;
            maxActiveBombsCount = 1;
        }

        public void Spawn()
        {
            Position = StartPosition;
        }

        public void Update()
        {
            UpdateBombPlacing();
            UpdateMovement();
            Position += movement;
        }

        private void UpdateBombPlacing()
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.Space))
            {
                PlaceBomb();
            }
        }

        private void PlaceBomb()
        {
            var playerCoordinates = GetPlayerCoordinates();
            AddBombEvent(playerCoordinates);
            
        }

        private void UpdateMovement()
        {
            var isMoveUp = Keyboard.IsKeyPressed(Keyboard.Key.W);
            var isMoveLeft = Keyboard.IsKeyPressed(Keyboard.Key.A);
            var isMoveDown = Keyboard.IsKeyPressed(Keyboard.Key.S);
            var isMoveRight = Keyboard.IsKeyPressed(Keyboard.Key.D);

            if (isMoveUp || isMoveLeft || isMoveDown || isMoveRight)
            {
                if (isMoveUp)
                {
                    if (CheckTile(TilePositionType.UP))
                    {
                        var frames = new List<AnimationFrame>();
                        frames.Add(new AnimationFrame(new Point(3 * Tile.TileSize, 1 * Tile.TileSize), Tile.TileSize, Tile.TileSize));
                        frames.Add(new AnimationFrame(new Point(4 * Tile.TileSize, 1 * Tile.TileSize), Tile.TileSize, Tile.TileSize));
                        frames.Add(new AnimationFrame(new Point(5 * Tile.TileSize, 1 * Tile.TileSize), Tile.TileSize, Tile.TileSize));
                        SetAnimation(frames);
                        movement.X = 0;
                        movement.Y = -1 * PlayerMovementSpeed;
                    }
                    else
                    {
                        StopMovement();
                    }
                }
                if (isMoveDown)
                {
                    if (CheckTile(TilePositionType.DOWN))
                    {
                        var frames = new List<AnimationFrame>();
                        frames.Add(new AnimationFrame(new Point(3 * Tile.TileSize, 0 * Tile.TileSize), Tile.TileSize, Tile.TileSize));
                        frames.Add(new AnimationFrame(new Point(4 * Tile.TileSize, 0 * Tile.TileSize), Tile.TileSize, Tile.TileSize));
                        frames.Add(new AnimationFrame(new Point(5 * Tile.TileSize, 0 * Tile.TileSize), Tile.TileSize, Tile.TileSize));
                        SetAnimation(frames);
                        movement.X = 0;
                        movement.Y = PlayerMovementSpeed;
                    }
                    else
                    {
                        StopMovement();
                    }
                }
                if (isMoveLeft)
                {
                    if (CheckTile(TilePositionType.LEFT))
                    {
                        var frames = new List<AnimationFrame>();
                        frames.Add(new AnimationFrame(new Point(0 * Tile.TileSize, 0 * Tile.TileSize), Tile.TileSize, Tile.TileSize));
                        frames.Add(new AnimationFrame(new Point(1 * Tile.TileSize, 0 * Tile.TileSize), Tile.TileSize, Tile.TileSize));
                        frames.Add(new AnimationFrame(new Point(2 * Tile.TileSize, 0 * Tile.TileSize), Tile.TileSize, Tile.TileSize));
                        SetAnimation(frames);
                        movement.X = -1 * PlayerMovementSpeed;
                        movement.Y = 0;
                    }
                    else
                    {
                        StopMovement();
                    }
                }
                if (isMoveRight)
                {
                    if (CheckTile(TilePositionType.RIGHT))
                    {
                        var frames = new List<AnimationFrame>();
                        frames.Add(new AnimationFrame(new Point(0 * Tile.TileSize, 1 * Tile.TileSize), Tile.TileSize, Tile.TileSize));
                        frames.Add(new AnimationFrame(new Point(1 * Tile.TileSize, 1 * Tile.TileSize), Tile.TileSize, Tile.TileSize));
                        frames.Add(new AnimationFrame(new Point(2 * Tile.TileSize, 1 * Tile.TileSize), Tile.TileSize, Tile.TileSize));
                        SetAnimation(frames);
                        movement.X = PlayerMovementSpeed;
                        movement.Y = 0;
                    }
                    else
                    {
                        StopMovement();
                    } 
                }
                HandleAnimation((float)0.005);
            }
            else
            {
                StopMovement();
            }
        }

        private void StopMovement()
        {
            movement.X = 0;
            movement.Y = 0;
            rectangleShape.TextureRect = new IntRect(
                PlayerTextureHorizontalNumber * Tile.TileSize,
                PlayerTextureVerticalNumber * Tile.TileSize,
                Tile.TileSize,
                Tile.TileSize
            );
        }

        private Point GetPlayerCoordinates()
        {
            var playerTileX = (int)Math.Round(Position.X / Tile.TileSize);
            var playerTileY = (int)Math.Round(Position.Y / Tile.TileSize);
            return new Point(playerTileX, playerTileY);
        }

        private Point GetTileCoordinates(Point playerCoordinates, TilePositionType tilePositionType)
        {
            switch (tilePositionType)
            {
                case TilePositionType.DOWN:
                    return new Point(playerCoordinates.X, playerCoordinates.Y + 1);
                case TilePositionType.UP:
                    return new Point(playerCoordinates.X, playerCoordinates.Y - 1);
                case TilePositionType.LEFT:
                    return new Point(playerCoordinates.X - 1, playerCoordinates.Y);
                case TilePositionType.RIGHT:
                    return new Point(playerCoordinates.X + 1, playerCoordinates.Y);
            }
            return Point.Empty;
        }

        private bool CheckTile(TilePositionType tilePositionType)
        {
            var playerCoordinates = GetPlayerCoordinates();
            var tileCoordinates = GetTileCoordinates(playerCoordinates, tilePositionType);
            var tile = gameBoard.GetTile(tileCoordinates);

            if (tile == null)
            {
                return false;
            }

            if (tile.Type == TileType.DESTROYED_BLOCK || tile.Type == TileType.INDESTRUCTIBLE_BLOCK)
            {
                var floatPlayerRect = new FloatRect(Position.X, Position.Y, Tile.TileSize, Tile.TileSize);
                var floatTileRect = new FloatRect(tileCoordinates.X * Tile.TileSize, tileCoordinates.Y * Tile.TileSize, Tile.TileSize, Tile.TileSize);

                if (!floatPlayerRect.Intersects(floatTileRect))
                {
                    return true;
                }
                return false;
            }
            return true;
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            states.Transform *= Transform;
            target.Draw(rectangleShape, states);
        }

        public void Destroy()
        {
            rectangleShape.Dispose();
            rectangleShape = null;

            gameBoard = null;
        }
    }
}
