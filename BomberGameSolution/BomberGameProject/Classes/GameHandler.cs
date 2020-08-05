using BomberGameProject.Classes;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace BomberGame.Classes
{
    public delegate void AddBombDelegate(Point coordinates, int power);
    public delegate void DeleteBombDelegate(Bomb bomb);
    public delegate void AddExplosionDelegate(Point coordinates, int power);
    public delegate void DeleteExplosionDelegate(Explosion explosion);

    public class GameHandler
    {
        private const int MaxPlayerBombsCount = 1;

        private GameBoard gameBoard;
        private Player player;
        private List<Bomb> bombs;
        private List<Explosion> explosions;

        public GameHandler(int boardSize)
        {
            gameBoard = new GameBoard(boardSize);
            bombs = new List<Bomb>();
            explosions = new List<Explosion>();
            player = new Player(gameBoard);
            player.AddBombEvent += CreateBomb;

            player.StartPosition = GetPlayerStartPosition();
            player.Spawn();
        }

        private void CreateBomb(Point bombCoordinates, int bombPower)
        {
            if (CheckActiveBombs(bombCoordinates))
            {
                var bomb = new Bomb(bombCoordinates, bombPower);
                bomb.DeleteBombEvent += DeleteBomb;
                bomb.AddExplosionEvent += CreateExplosion;
                bombs.Add(bomb);
            }
        }

        private void CreateExplosion(Point coordinates, int power)
        {
            var explosion = new Explosion(coordinates, power, gameBoard);
            explosion.DeleteExplosionEvent += DeleteExplosion;
            explosions.Add(explosion);
        }

        private void DeleteBomb(Bomb bomb)
        {
            lock (bombs)
            {
                bombs.Remove(bomb);
            }
        }

        private void DeleteExplosion(Explosion explosion)
        {
            lock (explosions)
            {
                explosions.Remove(explosion);
            }
        }

        private bool CheckActiveBombs(Point newBombCoordinates)
        {
            if (bombs.Count >= MaxPlayerBombsCount)
            {
                return false;
            }

            foreach (var bomb in bombs)
            {
                if (bomb.Coordinates == newBombCoordinates)
                {
                    return false;
                }
            }

            return true;
        }

        private Vector2f GetPlayerStartPosition()
        {
            while (true)
            {
                var random = new Random();
                var randomX = random.Next(0, gameBoard.boardSize);
                var randomY = random.Next(0, gameBoard.boardSize);
                var randomPlayerCoordinates = new Point(randomX, randomY);

                var startTile = gameBoard.GetTile(randomPlayerCoordinates);

                if (startTile.Type == TileType.GRASS)
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
            lock (gameBoard)
            {
                Program.Window.Draw(gameBoard);
            }
            lock (bombs)
            {
                foreach (var bomb in bombs)
                {
                    Program.Window.Draw(bomb);
                }
            }
            lock (explosions)
            {
                foreach (var explosion in explosions)
                {
                    Program.Window.Draw(explosion);
                }
            }
            lock (player)
            {
                Program.Window.Draw(player);
            }
        }

        public void CloseGame()
        {
            lock (gameBoard)
            {
                gameBoard.Clear();
                gameBoard = null;
            }
            lock (player)
            {
                player.Destroy();
                player = null;
            }
            lock (bombs)
            {
                foreach (var bomb in bombs)
                {
                    bomb.Destroy();
                }
                bombs.Clear();
                bombs = null;
            }
            lock (explosions)
            {
                foreach (var explosion in explosions)
                {
                    explosion.Destroy();
                }
                explosions.Clear();
                explosions = null;
            }
        }
    }
}
