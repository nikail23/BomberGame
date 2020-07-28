using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace BomberGame.Classes
{
    public enum TileType
    {
        INDESTRUCTIBLE_BLOCK,
        GRASS,
        DESTROYED_BLOCK
    }

    public class Tile : Transformable, Drawable
    {
        public static int TileSize = 16;

        private const int GrassTileHorizontalNumber = 0;
        private const int GrassTileVerticalNumber = 4;
        private const int IndestructibleBlockTileHorizontalNumber = 3;
        private const int IndestructibleBlockTileVerticalNumber = 3;
        private const int DestroyedBlockTileHorizontalNumber = 4;
        private const int DestroyedBlockTileVerticalNumber = 3;

        public TileType TileType { get; private set; }

        private RectangleShape rectangleShape;

        public Tile(TileType tileType)
        {
            rectangleShape = new RectangleShape(new Vector2f(TileSize, TileSize));

            this.TileType = tileType;
            switch (tileType)
            {
                case TileType.GRASS:
                    rectangleShape.Texture = ContentHandler.Texture;
                    rectangleShape.TextureRect = new IntRect(
                        GrassTileHorizontalNumber * TileSize, 
                        GrassTileVerticalNumber * TileSize, 
                        TileSize, 
                        TileSize
                   );
                    break;
                case TileType.INDESTRUCTIBLE_BLOCK:
                    rectangleShape.Texture = ContentHandler.Texture;
                    rectangleShape.TextureRect = new IntRect(
                        IndestructibleBlockTileHorizontalNumber * TileSize, 
                        IndestructibleBlockTileVerticalNumber * TileSize, 
                        TileSize, 
                        TileSize
                    );
                    break;
                case TileType.DESTROYED_BLOCK:
                    rectangleShape.Texture = ContentHandler.Texture;
                    rectangleShape.TextureRect = new IntRect(
                        DestroyedBlockTileHorizontalNumber * TileSize,
                        DestroyedBlockTileVerticalNumber * TileSize,
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
