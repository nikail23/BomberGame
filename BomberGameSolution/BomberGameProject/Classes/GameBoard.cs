using BomberGameProject.Classes;
using Microsoft.VisualBasic;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace BomberGame.Classes
{
    public class GameBoard : GameObject, IDisposable
    {
        private Tile[][] tiles;
        public int boardSize { get; private set; }

        public GameBoard(int boardSize)
        {
            GameBoardInitialize(boardSize);
            GameBoardTilesStructureCreate();
        }

        private void GameBoardInitialize(int boardSize)
        {
            this.boardSize = boardSize;
            tiles = new Tile[boardSize][];
            for (int i = 0; i < boardSize; i++)
            {
                tiles[i] = new Tile[boardSize];
            }
        }

        private void GameBoardTilesStructureCreate()
        {
            for (int i = 0; i < boardSize; i++)
            {
                SetTile(TileType.INDESTRUCTIBLE_BLOCK, i, 0);
                SetTile(TileType.INDESTRUCTIBLE_BLOCK, i, boardSize - 1);
            }
            for (int i = 0; i < boardSize; i++)
            {
                SetTile(TileType.INDESTRUCTIBLE_BLOCK, 0, i);
                SetTile(TileType.INDESTRUCTIBLE_BLOCK, boardSize - 1, i);
            }
            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    if ((i % 2 == 0) && (j % 2 == 0))
                    {
                        SetTile(TileType.INDESTRUCTIBLE_BLOCK, i, j);
                    }
                    else if (tiles[i][j] == null)
                    {
                        SetTile(GetRandomTileType(), i, j);
                    }
                }
            }
        }

        private TileType GetRandomTileType()
        {
            var random = new Random();
            int value = random.Next((int)TileType.GRASS, (int)TileType.DESTROYED_BLOCK + 1);
            return (TileType)(Enum.GetValues(typeof(TileType))).GetValue(value);
        }

        private void SetTile(TileType type, int x, int y)
        {
            tiles[x][y] = new Tile(type);
            tiles[x][y].Position = new Vector2f(x * Tile.TileSize, y * Tile.TileSize);
        }

        public Tile GetTile(Point tileCoordinates)
        {
            if (tileCoordinates.X > -1 && tileCoordinates.X < boardSize && tileCoordinates.Y > -1 && tileCoordinates.Y < boardSize)
            {
                return tiles[tileCoordinates.X][tileCoordinates.Y];
            }
            return null;
        }

        public void Clear()
        {
            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; i < boardSize; i++)
                {
                    tiles[i][j].Destroy();
                    tiles[i][j] = null;
                }
            }
            boardSize = 0;
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    if (tiles[i][j] != null)
                    {
                        target.Draw(tiles[i][j]);
                    }
                }
            }
        }
    }
}
