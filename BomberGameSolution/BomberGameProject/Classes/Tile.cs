using BomberGameProject.Classes;
using BomberGameProject.Classes.AbstractClasses;
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

    public class Tile : TexturedGameObject
    {
        public static int TileSize = 16;

        private const int GrassTileHorizontalNumber = 0;
        private const int GrassTileVerticalNumber = 13;
        private const int IndestructibleTileHorizontalNumber = 3;
        private const int IndestructibleTileVerticalNumber = 3;
        private const int DestroyedTileHorizontalNumber = 4;
        private const int DestroyedTileVerticalNumber = 3;

        public TileType Type { get; private set; }

        public Tile(TileType tileType)
        {
            rectangleShape = new RectangleShape(new Vector2f(TileSize, TileSize));

            this.Type = tileType;
            switch (tileType)
            {
                case TileType.GRASS:
                    SetTexture(
                        ContentHandler.Texture, 
                        GrassTileHorizontalNumber * TileSize,
                        GrassTileVerticalNumber * TileSize, 
                        TileSize,
                        TileSize
                    );
                    break;
                case TileType.INDESTRUCTIBLE_BLOCK:
                    SetTexture(
                        ContentHandler.Texture,
                        IndestructibleTileHorizontalNumber * TileSize,
                        IndestructibleTileVerticalNumber * TileSize,
                        TileSize,
                        TileSize
                    );
                    break;
                case TileType.DESTROYED_BLOCK:
                    SetTexture(
                        ContentHandler.Texture,
                        DestroyedTileHorizontalNumber * TileSize,
                        DestroyedTileVerticalNumber * TileSize,
                        TileSize,
                        TileSize
                    );
                    break;
            }
        }

        public void Destroy()
        {
            rectangleShape.Dispose();
            rectangleShape = null;
        }
    }
}
