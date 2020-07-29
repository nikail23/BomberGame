using BomberGame.Classes;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace BomberGameProject.Classes
{
    public class Player : AnimatedGameObject
    {
        private const int PlayerTextureHorizontalNumber = 4;
        private const int PlayerTextureVerticalNumber = 0;

        // подобавлять константы для анимации игрока

        private const float PlayerMovementSpeed = 0.8f;

        public Vector2f StartPosition;
        private Vector2f movement;

        private GameBoard gameBoard;

        public Player(GameBoard gameBoard)
        {
            rectangleShape.Texture = ContentHandler.Texture;
            rectangleShape.TextureRect = new IntRect(
                PlayerTextureHorizontalNumber * Tile.TileSize, 
                PlayerTextureVerticalNumber * Tile.TileSize, 
                Tile.TileSize, 
                Tile.TileSize
            );

            this.gameBoard = gameBoard;
        }

        public void Spawn()
        {
            Position = StartPosition;
        }

        public void Update()
        {
            UpdateMovement();
            Position += movement;
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
                    if (CheckUpTiles())
                    {
                        SetAnimation(1, 3, 6);
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
                    if (CheckDownTiles())
                    {
                        SetAnimation(0, 3, 6);
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
                    if (CheckLeftTiles())
                    {
                        SetAnimation(0, 0, 3);
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
                    if (CheckRightTiles())
                    {
                        SetAnimation(1, 0, 3);
                        movement.X = PlayerMovementSpeed;
                        movement.Y = 0;
                    }
                    else
                    {
                        StopMovement();
                    }
                }
                HandleAnimation();
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

        private bool CheckLeftTiles()
        {
            var playerTileX = (int)Math.Round(Position.X / Tile.TileSize);
            var playerTileY =(int)Math.Round(Position.Y / Tile.TileSize);

            var tileX = playerTileX - 1;
            var tileY = playerTileY;

            var tile = gameBoard.GetTile(tileX, tileY);

            if (tile == null)
            {
                return false;
            }

            if (tile.TileType == TileType.DESTROYED_BLOCK || tile.TileType == TileType.INDESTRUCTIBLE_BLOCK)
            {
                var floatPlayerRect = new FloatRect(Position.X, Position.Y, Tile.TileSize, Tile.TileSize);
                var floatTileRect = new FloatRect(tileX * Tile.TileSize, tileY * Tile.TileSize, Tile.TileSize, Tile.TileSize);

                if (!floatPlayerRect.Intersects(floatTileRect))
                {
                    return true;
                }
                return false;
            }
            return true;
        }

        private bool CheckUpTiles()
        {
            var playerTileX = (int)Math.Round(Position.X / Tile.TileSize);
            var playerTileY = (int)Math.Round(Position.Y / Tile.TileSize);

            var tileX = playerTileX;
            var tileY = playerTileY - 1;

            var tile = gameBoard.GetTile(tileX, tileY);

            if (tile == null)
            {
                return false;
            }

            if (tile.TileType == TileType.DESTROYED_BLOCK || tile.TileType == TileType.INDESTRUCTIBLE_BLOCK)
            {
                var floatPlayerRect = new FloatRect(Position.X, Position.Y, Tile.TileSize, Tile.TileSize);
                var floatTileRect = new FloatRect(tileX * Tile.TileSize, tileY * Tile.TileSize, Tile.TileSize, Tile.TileSize);

                if (!floatPlayerRect.Intersects(floatTileRect))
                {
                    return true;
                }
                return false;
            }
            return true;
        }

        private bool CheckDownTiles()
        {
            var playerTileX = (int)Math.Round(Position.X / Tile.TileSize);
            var playerTileY = (int)Math.Round(Position.Y / Tile.TileSize);

            var tileX = playerTileX;
            var tileY = playerTileY + 1;

            var tile = gameBoard.GetTile(tileX, tileY);

            if (tile == null)
            {
                return false;
            }

            if (tile.TileType == TileType.DESTROYED_BLOCK || tile.TileType == TileType.INDESTRUCTIBLE_BLOCK)
            {
                var floatPlayerRect = new FloatRect(Position.X, Position.Y, Tile.TileSize, Tile.TileSize);
                var floatTileRect = new FloatRect(tileX * Tile.TileSize, tileY * Tile.TileSize, Tile.TileSize, Tile.TileSize);

                if (!floatPlayerRect.Intersects(floatTileRect))
                {
                    return true;
                }
                return false;
            }
            return true;
        }

        private bool CheckRightTiles()
        {
            var playerTileX = (int)Math.Round(Position.X / Tile.TileSize);
            var playerTileY = (int)Math.Round(Position.Y / Tile.TileSize);

            var tileX = playerTileX + 1;
            var tileY = playerTileY;

            var tile = gameBoard.GetTile(tileX, tileY);

            if (tile == null)
            {
                return false;
            }

            if (tile.TileType == TileType.DESTROYED_BLOCK || tile.TileType == TileType.INDESTRUCTIBLE_BLOCK)
            {
                var floatPlayerRect = new FloatRect(Position.X, Position.Y, Tile.TileSize, Tile.TileSize);
                var floatTileRect = new FloatRect(tileX * Tile.TileSize, tileY * Tile.TileSize, Tile.TileSize, Tile.TileSize);

                if (!floatPlayerRect.Intersects(floatTileRect))
                {
                    return true;
                }
                return false;
            }
            return true;
        }
    }
}
