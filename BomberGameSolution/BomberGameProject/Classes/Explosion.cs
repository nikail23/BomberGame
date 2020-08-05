using BomberGame.Classes;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace BomberGameProject.Classes
{
    public enum DirectionType 
    {
        LEFT, 
        RIGHT,
        UP,
        DOWN
    }

    public class Explosion : GameObject
    {
        private const int TimeToLeave = 1;

        public event DeleteExplosionDelegate DeleteExplosionEvent;

        private List<ExplosionElement> elements;
        private GameBoard gameBoard;
        private Point centerCoordinates;
        private int power;

        public Explosion(Point centerCoordinates, int power, GameBoard gameBoard)
        {
            elements = new List<ExplosionElement>();
            this.centerCoordinates = centerCoordinates;
            this.gameBoard = gameBoard;
            this.power = power;

            CreateExplosionStructure();
            StartAnimation();
            var destroyingTiles = GetDestroyingTiles();
            HandleDestroying();
            HandleTilesDestroying(destroyingTiles);
        }

        private void HandleTilesDestroying(List<Tile> destroyingTiles)
        {
            var tiles = GetDestroyingTiles();
            foreach (var tile in tiles)
            {
                tile.StartAnimation();
            }
        }

        private List<Tile> GetDestroyingTiles()
        {
            var tiles = new List<Tile>();
            foreach (var element in elements)
            {
                var tile = gameBoard.GetTile(element.Coordinates);

                if (tile.Type == TileType.DESTROYED_BLOCK)
                {
                    tiles.Add(tile);
                }
            }
            return tiles;
        }

        private async void HandleDestroying()
        {
            await Task.Run(()=>WaitTime(TimeToLeave));
            DeleteExplosionEvent(this);
            Destroy();
        }

        private void WaitTime(int detonateTime)
        {
            var clock = new Clock();
            clock.Restart();
            while (clock.ElapsedTime.AsSeconds() != detonateTime) { }
        }

        private bool IsAnimatingExplosion()
        {
            var isAnimating = false;
            foreach (var element in elements)
            {
                if (element.isAnimating)
                {
                    isAnimating = true;
                    break;
                }
            }
            return isAnimating;
        }

        private void StartAnimation()
        {
            foreach (var element in elements)
            {
                element.StartAnimation();
            }
            while (IsAnimatingExplosion()) { }
        }

        private void CreateExplosionStructure()
        {
            elements.Add(new ExplosionElement(ExplosionElementType.CENTER, centerCoordinates));
            var maxLength = power;

            HandleSpread(DirectionType.LEFT, centerCoordinates, maxLength);
            HandleSpread(DirectionType.RIGHT, centerCoordinates, maxLength);
            HandleSpread(DirectionType.UP, centerCoordinates, maxLength);
            HandleSpread(DirectionType.DOWN, centerCoordinates, maxLength);
        }

        private void HandleSpread(DirectionType directionType, Point coordinates, int maxLength)
        {
            Point nextCoordinates = new Point();

            switch (directionType)
            {
                case DirectionType.DOWN:
                    nextCoordinates = new Point(coordinates.X, coordinates.Y + 1);
                    break;
                case DirectionType.UP:
                    nextCoordinates = new Point(coordinates.X, coordinates.Y - 1);
                    break;
                case DirectionType.LEFT:
                    nextCoordinates = new Point(coordinates.X - 1, coordinates.Y);
                    break;
                case DirectionType.RIGHT:
                    nextCoordinates = new Point(coordinates.X + 1, coordinates.Y);
                    break;
            }
            
            var tile = gameBoard.GetTile(nextCoordinates);
            if (tile == null)
            {
                return;
            }

            var element = GetExplosionElement(directionType, tile.Type, nextCoordinates, maxLength);
            if (element != null)
            {
                elements.Add(element);

                if (tile.Type == TileType.GRASS)
                {
                    maxLength--;
                    if (maxLength > 0)
                    {
                        HandleSpread(directionType, nextCoordinates, maxLength);
                    }
                }
            }
        }

        private ExplosionElement GetExplosionElement(DirectionType directionType, TileType tileType, Point coordinates, int maxLength)
        {
            ExplosionElement element = null;
            if (tileType == TileType.GRASS && maxLength != 1)
            {
                switch (directionType)
                {
                    case DirectionType.DOWN:
                        element = new ExplosionElement(ExplosionElementType.DOWN_CONTINIOUS, coordinates);
                        break;
                    case DirectionType.UP:
                        element = new ExplosionElement(ExplosionElementType.UP_CONTINIOUS, coordinates);
                        break;
                    case DirectionType.LEFT:
                        element = new ExplosionElement(ExplosionElementType.LEFT_CONTINIOUS, coordinates);
                        break;
                    case DirectionType.RIGHT:
                        element = new ExplosionElement(ExplosionElementType.RIGHT_CONTINIOUS, coordinates);
                        break;
                }
                return element;
            }
            else if (tileType == TileType.DESTROYED_BLOCK || maxLength == 1)
            {
                switch (directionType)
                {
                    case DirectionType.DOWN:
                        element = new ExplosionElement(ExplosionElementType.DOWN_ENDED, coordinates);
                        break;
                    case DirectionType.UP:
                        element = new ExplosionElement(ExplosionElementType.UP_ENDED, coordinates);
                        break;
                    case DirectionType.LEFT:
                        element = new ExplosionElement(ExplosionElementType.LEFT_ENDED, coordinates);
                        break;
                    case DirectionType.RIGHT:
                        element = new ExplosionElement(ExplosionElementType.RIGHT_ENDED, coordinates);
                        break;
                }
                return element;
            }
            return null;
        }

        public void Destroy()
        {
            gameBoard = null;
            foreach (var element in elements)
            {
                element.Destroy();
            }
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            foreach (var explosionElement in elements)
            {
                target.Draw(explosionElement);
            }
        }
    }
}
