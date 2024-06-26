﻿using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace sprint0.Sprites
{
    public class Sprite : ISprite
    {
        
        protected Texture2D spriteSheet;
        protected int delay;
        protected int delayCount;
        protected int frame;
        protected int frameCount;
        protected float scale;
        protected List<Rectangle> frameSources;
        protected Vector2 origin;

        public virtual int GetWidth(int animationFrame = -1)
        {
            if (animationFrame != -1) {
                frame = animationFrame;
            }
            return (int) (scale * frameSources[frame % frameCount].Width);
        }

        public virtual int GetHeight(int animationFrame = -1)
        {
            if (animationFrame != -1) {
                frame = animationFrame;
            }
            return (int) (scale * frameSources[frame % frameCount].Height);
        }

        public virtual void Draw(SpriteBatch spriteBatch, Vector2 position, SpriteEffects spriteEffect, Color color)
        {
            spriteBatch.Draw(spriteSheet, position, frameSources[frame % frameCount], color, 0f, origin, scale, spriteEffect, 0f);

            // delay change in frames
            delayCount++;
            if (delayCount % delay == 0) frame++;
        }

        // manual frame change
        public virtual void Draw(SpriteBatch spriteBatch, Vector2 position, int currentFrame, SpriteEffects spriteEffect, Color color)
        {
            spriteBatch.Draw(spriteSheet, position, frameSources[currentFrame % frameCount], color, 0f, origin, scale, spriteEffect, 0f);
            frame = currentFrame;
        }
    }
}
