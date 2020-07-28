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
    public class Player: Transformable, Drawable
    {
        private const int PlayerTextureHorizontalNumber = 4;
        private const int PlayerTextureVerticalNumber = 0;

        private const float PlayerMovementSpeed = 0.8f;

        public Vector2f StartPosition;

        private Vector2f movement;
        private RectangleShape rectangleShape;
        private GameBoard gameBoard;

        public Player(GameBoard gameBoard)
        {
            rectangleShape = new RectangleShape(new Vector2f(Tile.TileSize, Tile.TileSize));
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
                    if (CheckUpTile())
                    {
                        movement.X = 0;
                        movement.Y = -1 * PlayerMovementSpeed;
                    }
                    else
                    {
                        StopMovement();
                    }
                }
                if (isMoveLeft)
                {
                    if (CheckLeftTile())
                    {
                        movement.X = -1 * PlayerMovementSpeed;
                        movement.Y = 0;
                    }
                    else
                    {
                        StopMovement();
                    }
                }
                if (isMoveDown)
                {
                    if (CheckDownTile())
                    {
                        movement.X = 0;
                        movement.Y = PlayerMovementSpeed;
                    }
                    else
                    {
                        StopMovement();
                    }
                }
                if (isMoveRight)
                {
                    if (CheckRightTile())
                    {
                        movement.X = PlayerMovementSpeed;
                        movement.Y = 0;
                    }
                    else
                    {
                        StopMovement();
                    }
                }
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
        }

        private bool CheckLeftTile()
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

        private bool CheckUpTile()
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

        private bool CheckDownTile()
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

        private bool CheckRightTile()
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

        public void Draw(RenderTarget target, RenderStates states)
        {
            states.Transform *= Transform;
            target.Draw(rectangleShape, states);
        }
    }
}
