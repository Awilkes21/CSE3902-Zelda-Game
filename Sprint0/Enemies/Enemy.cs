﻿using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using sprint0.Interfaces;
using sprint0.Factories;
using sprint0.Sound;
using sprint0.Managers;
using System;
using Microsoft.Xna.Framework.Audio;
using sprint0.Utility;

namespace sprint0.Enemies
{
    public abstract class Enemy : ICollidable
    {
        protected const int DeathFrames = 5;       

        protected static Random rand = new();
        protected readonly SoundEffect sound = SoundManager.Manager.enemyDamageSound();

        public PhysicsManager Physics { get; protected set; }
        protected HealthManager health;

        public int Damage { get; set; }
        protected Timer behaviorTimer;
        protected Timer deathTimer;
        public ICollidable.ObjectType Type { get; set; }
        public ISprite Sprite { get; set; }

        protected virtual void BoomerangBehavior() {}

        protected virtual void WallBehavior()
        {
            Physics.ReverseDirection();
            Physics.Freeze();
        }

        protected virtual void Death()
        {
            if (deathTimer.Status() && deathTimer.HasStarted())
            {
                CollisionManager.Collidables.Remove(this);
                SoundManager.Manager.enemyDeadSound().Play();
            }
        }
        
        protected abstract void Behavior(GameTime gameTime);

        public virtual void Update(GameTime gameTime)
        {
            health.UpdateCounters();

            if (Physics.NotStunned())
            {
                Physics.Move(gameTime);

                // Ex: change direction every delay seconds
                if (behaviorTimer.UnconditionalUpdate())
                {
                    Behavior(gameTime);
                }
            }

            Death();
        }

        public virtual void Collide(ICollidable obj, ICollidable.Edge edge)
        {
            switch (obj.Type)
            {
                case ICollidable.ObjectType.Sword:
                case ICollidable.ObjectType.Bomb:
                case ICollidable.ObjectType.Ability:
                    health.TakeDamage(obj.Damage);
                    break;
                case ICollidable.ObjectType.Wall:
                case ICollidable.ObjectType.Tile:
                case ICollidable.ObjectType.Door:
                    WallBehavior();
                    break;
                case ICollidable.ObjectType.Boomerang:
                    BoomerangBehavior();
                    break;
            }
        }

        public Vector2 GetVelocity()
        {
            return Physics.CurrentVelocity;
        }

        public Rectangle GetHitBox()
        {
            return new Rectangle((int) Physics.CurrentPosition.X, (int) Physics.CurrentPosition.Y, Sprite.GetWidth(), Sprite.GetHeight());
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (!deathTimer.ConditionalUpdate(health.Dead()))
            {
                EnemySpriteFactory.Instance.CreateEnemyExplosionSprite().Draw(spriteBatch, Physics.CurrentPosition, SpriteEffects.None, Color.White);
            }
            else
            {
                Sprite.Draw(spriteBatch, Physics.CurrentPosition, SpriteEffects.None, health.Color);
            }
        }

        public void Reset()
        {
            Physics.Reset();
            health.Reset();
            behaviorTimer.Reset();
            deathTimer.Reset();
        }
    }
}
