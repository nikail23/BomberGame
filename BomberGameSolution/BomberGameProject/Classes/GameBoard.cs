using SFML.Graphics;
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
            this.boardSize = boardSize;
            tiles = new Tile[boardSize][];
            for (int i = 0; i < boardSize; i++)
            {
                tiles[i] = new Tile[boardSize];
            }

            tiles[0][0] = new Tile(TileType.BLOCK); // тестовая плитка
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
