using BomberGame.Classes;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace BomberGameProject.Classes
{
    public abstract class GameObject : Transformable, Drawable
    {
        protected RectangleShape rectangleShape;

        public GameObject()
        {
            rectangleShape = new RectangleShape(new Vector2f(Tile.TileSize, Tile.TileSize));
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            states.Transform *= Transform;
            target.Draw(rectangleShape, states);
        }
    }
}
