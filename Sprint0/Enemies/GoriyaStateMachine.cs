﻿using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using sprint0.Factories;
using sprint0.Projectiles;
using sprint0.Interfaces;
using System;

namespace sprint0.Enemies
{
    public class GoriyaStateMachine
    {
        private const int DirectionChange = 4;

        public GoriyaEnemy Goriya { get; set; }
        public SpriteEffects SpriteEffect { get; private set; }
        public bool BoomerangThrown { get; set; }
        public GoriyaProjectile Boomerang { get; set; }
        public bool flipped { private get; set; }


        public GoriyaStateMachine(GoriyaEnemy goriya)
        {
            Goriya = goriya;            
            BoomerangThrown = false;
            SpriteEffect = SpriteEffects.None;
            flipped = false;
        }
        
        public void ChangeDirection(Random rand)
        {

            if (flipped)
            {
                Goriya.Physics.ReverseDirection();
                flipped = false;
            }
            else
            {
                int dir = rand.Next(0, DirectionChange);
                Goriya.Physics.ChangeDirection((Direction)dir);
            }

            switch (Goriya.Physics.Direction)
            {
                case Direction.Up:
                    // move up
                    Goriya.Sprite = EnemySpriteFactory.Instance.CreateGoriyaFacingUpStateSprite();
                    SpriteEffect = SpriteEffects.FlipHorizontally;
                    break;
                case Direction.Left:
                    // move left
                    Goriya.Sprite = EnemySpriteFactory.Instance.CreateGoriyaFacingSideStateSprite();
                    SpriteEffect = SpriteEffects.FlipHorizontally;
                    break;
                case Direction.Right:
                    // move right
                    Goriya.Sprite = EnemySpriteFactory.Instance.CreateGoriyaFacingSideStateSprite();
                    SpriteEffect = SpriteEffects.None;
                    break;
                case Direction.Down:
                    // move down
                    Goriya.Sprite = EnemySpriteFactory.Instance.CreateGoriyaFacingDownStateSprite();
                    SpriteEffect = SpriteEffects.FlipHorizontally;
                    break;
                default:
                    break;
            }
        }

        public void ThrowBoomerang()
        {
            Boomerang = new GoriyaProjectile(Goriya.Physics.CurrentPosition, Goriya.Physics.Direction, this);
            Goriya.Physics.CurrentVelocity = Vector2.Zero;
            BoomerangThrown = true;
        }
    }
}
