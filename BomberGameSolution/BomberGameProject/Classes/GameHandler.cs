using SFML.Window;
using System;
using System.Collections.Generic;
using System.Text;

namespace BomberGame.Classes
{
    public class GameHandler
    {
        private GameBoard gameBoard;
        
        public GameHandler(int boardSize)
        {
            gameBoard = new GameBoard(boardSize);
        }

        public void Update()
        {

        }

        public void Draw()
        {
            Program.Window.Draw(gameBoard);
        }
    }
}
