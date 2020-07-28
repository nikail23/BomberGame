using BomberGameProject.Classes;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Text;

namespace BomberGame.Classes
{
    public class GameHandler
    {
        private GameBoard gameBoard;
        private Player player;
        
        public GameHandler(int boardSize)
        {
            gameBoard = new GameBoard(boardSize);
            player = new Player(gameBoard);

            player.StartPosition = GetPlayerStartPosition(); 
            player.Spawn();
        }

        private Vector2f GetPlayerStartPosition()
        {
            while (true)
            {
                var random = new Random();
                var randomX = random.Next(0, gameBoard.boardSize);
                var randomY = random.Next(0, gameBoard.boardSize);

                var startTile = gameBoard.GetTile(randomX, randomY);

                if (startTile.TileType == TileType.GRASS)
                {
                    return new Vector2f(randomX * Tile.TileSize, randomY * Tile.TileSize);
                }
            }
        }

        public void Update()
        {
            player.Update();
        }

        public void Draw()
        {
            Program.Window.Draw(gameBoard);
            Program.Window.Draw(player);
        }
    }
}
