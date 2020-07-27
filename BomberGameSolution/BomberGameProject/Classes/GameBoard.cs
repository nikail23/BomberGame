using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace BomberGame.Classes
{
    public class GameBoard : Transformable, Drawable
    {
        private Tile[][] tiles;
        private int boardSize;

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

        public void Draw(RenderTarget target, RenderStates states)
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
