using BomberGame.Classes;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace BomberGameProject.Classes.AbstractClasses
{
    public abstract class AnimatedGameObject : TexturedGameObject
    {
        private Clock clock;
        private List<AnimationFrame> frames;
        private float currentFrameNumber;

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
            frames = new List<AnimationFrame>();
        }

        protected void SetAnimation(List<AnimationFrame> frames)
        {
            if (!IsPreviousAnimation(frames))
            {
                currentFrameNumber = 0;
                this.frames = frames;
            } 
        }

        private bool IsPreviousAnimation(List<AnimationFrame> frames)
        {
            if (this.frames.Count != frames.Count)
            {
                return false;
            }

            for (int i = 0; i < frames.Count; i++)
            {
                if (this.frames[i].Coordinates != frames[i].Coordinates)
                {
                    return false;
                } 
                if (this.frames[i].Width != frames[i].Width)
                {
                    return false;
                }
                if (this.frames[i].Height != frames[i].Height)
                {
                    return false;
                }
            }
            return true;
        }

        protected void HandleAnimation(float speedNumber)
        {
            currentFrameNumber += Time * speedNumber;
            if (currentFrameNumber > frames.Count - 0.5)
            {
                currentFrameNumber = 0;
            }

            var currentFrame = frames[(int)currentFrameNumber];

            if (rectangleShape != null)
            {
                rectangleShape.TextureRect = new IntRect(currentFrame.Coordinates.X, currentFrame.Coordinates.Y, currentFrame.Width, currentFrame.Height);
            }
        }
    }
}
