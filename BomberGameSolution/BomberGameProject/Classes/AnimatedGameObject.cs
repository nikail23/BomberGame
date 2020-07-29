using BomberGame.Classes;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace BomberGameProject.Classes
{
    public abstract class AnimatedGameObject : GameObject
    {
        private Clock clock;
        private int framesCount;
        private int animationTextureHeightNumber;
        private int firstFrameIndex;
        private int secondFrameIndex;
        private float currentFrame;

        protected float Time 
        { 
            get
            {
                var time = clock.ElapsedTime.AsMicroseconds();
                clock.Restart();
                return (float) time / 800;
            }
        }

        public AnimatedGameObject()
        {
            clock = new Clock();
        }

        protected void SetAnimation(int animationTextureHeightNumber, int firstFrameIndex, int secondFrameIndex)
        {
            if (!IsPreviousAnimation(animationTextureHeightNumber, firstFrameIndex, secondFrameIndex))
            {
                currentFrame = firstFrameIndex;
                this.firstFrameIndex = firstFrameIndex;
                this.secondFrameIndex = secondFrameIndex;
                framesCount = secondFrameIndex - firstFrameIndex;
                this.animationTextureHeightNumber = animationTextureHeightNumber;
            } 
        }

        private bool IsPreviousAnimation(int animationTextureHeightNumber, int firstFrameIndex, int secondFrameIndex)
        {
            return this.animationTextureHeightNumber == animationTextureHeightNumber && this.firstFrameIndex == firstFrameIndex && this.secondFrameIndex == secondFrameIndex; 
        }

        protected void HandleAnimation()
        {
            currentFrame += Time * (float) 0.005;
            if (currentFrame > firstFrameIndex + framesCount)
            {
                currentFrame = firstFrameIndex;
            }
            rectangleShape.TextureRect = new IntRect((int)currentFrame * Tile.TileSize, animationTextureHeightNumber * Tile.TileSize, Tile.TileSize, Tile.TileSize);
        }
    }
}
