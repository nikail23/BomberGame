using BomberGame.Classes;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace BomberGameProject.Classes.AbstractClasses
{
    public abstract class TexturedGameObject : GameObject
    {
        protected RectangleShape rectangleShape;

        public TexturedGameObject()
        {
            rectangleShape = new RectangleShape(new Vector2f(Tile.TileSize, Tile.TileSize));
        }

        public void SetTexture(Texture texture, int x, int y, int width, int height)
        {
            rectangleShape.Texture = texture;
            rectangleShape.TextureRect = new IntRect(x, y, width, height);
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            states.Transform *= Transform;
            target.Draw(rectangleShape, states);
        }
    }
}
