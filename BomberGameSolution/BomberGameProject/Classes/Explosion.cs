using BomberGame.Classes;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

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

        public event DeleteExplosionDelegate DeleteBombEvent;

        private List<ExplosionElement> elements;
        private GameBoard gameBoard;
        private Point centerCoordinates;

        public Explosion(Point centerCoordinates, GameBoard gameBoard)
        {
            elements = new List<ExplosionElement>();
            this.centerCoordinates = centerCoordinates;
            this.gameBoard = gameBoard;

            CreateExplosionStructure();
            StartAnimation();
            HandleDestroying();
        }

        private async void HandleDestroying()
        {
            await Task.Run(()=>WaitTime(TimeToLeave));
            DeleteBombEvent(this);
            Destroy();
        }

        private void WaitTime(int detonateTime)
        {
            var clock = new Clock();
            clock.Restart();
            while (clock.ElapsedTime.AsSeconds() != detonateTime) { }
        }

        private void StartAnimation()
        {
            foreach (var element in elements)
            {
                element.StartAnimation();
            }
        }

        private void CreateExplosionStructure()
        {
            elements.Add(new ExplosionElement(ExplosionElementType.CENTER, centerCoordinates));

            HandleSpread(DirectionType.LEFT, centerCoordinates);
            HandleSpread(DirectionType.RIGHT, centerCoordinates);
            HandleSpread(DirectionType.UP, centerCoordinates);
            HandleSpread(DirectionType.DOWN, centerCoordinates);
        }

        private void HandleSpread(DirectionType directionType, Point coordinates)
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

            var element = GetExplosionElement(directionType, tile.Type, nextCoordinates);
            if (element != null)
            {
                elements.Add(element);

                if (tile.Type == TileType.GRASS)
                {
                    HandleSpread(directionType, nextCoordinates);
                }
            }
        }

        private ExplosionElement GetExplosionElement(DirectionType directionType, TileType tileType, Point coordinates)
        {
            ExplosionElement element = null;
            if (tileType == TileType.GRASS)
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
            else if (tileType == TileType.DESTROYED_BLOCK)
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
