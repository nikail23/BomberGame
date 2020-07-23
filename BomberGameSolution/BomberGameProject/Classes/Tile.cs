using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace BomberGame.Classes
{
    public enum TileType
    {
        GRASS,
        BLOCK
    }

    public class Tile : Transformable, Drawable
    {
        private const int TileSize = 16;
        private const int GrassTileHorizontalNumber = 0;
        private const int GrassTileVerticalNumber = 4;
        private const int BlockTileHorizontalNumber = 4;
        private const int BlockTileVerticalNumber = 3;

        public TileType tileType { get; private set; }

        private RectangleShape rectangleShape;

        public Tile(TileType tileType)
        {
            rectangleShape = new RectangleShape(new SFML.System.Vector2f(TileSize, TileSize));

            this.tileType = tileType;
            switch (tileType)
            {
                case TileType.GRASS:
                    rectangleShape.Texture = ContentHandler.texture;
                    rectangleShape.TextureRect = new IntRect(
                        GrassTileHorizontalNumber * TileSize, 
                        GrassTileVerticalNumber * TileSize, 
                        TileSize, 
                        TileSize
                   );
                    break;
                case TileType.BLOCK:
                    rectangleShape.Texture = ContentHandler.texture;
                    rectangleShape.TextureRect = new IntRect(
                        BlockTileHorizontalNumber * TileSize, 
                        BlockTileVerticalNumber * TileSize, 
                        TileSize, 
                        TileSize
                    );
                    break;
            }
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            states.Transform *= Transform;

            target.Draw(rectangleShape, states);
        }
    }
}
