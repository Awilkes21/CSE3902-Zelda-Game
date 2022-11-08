﻿using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using sprint0.Interfaces;
using sprint0.PlayerClasses;
using sprint0.PlayerClasses.Abilities;
using sprint0.RoomClasses;
using sprint0.TileClasses;
using sprint0.Factories;
using sprint0.Classes;
using sprint0.Sound;

namespace sprint0.Enemies
{
    public abstract class Enemy : ICollidable
    {
        protected const int TileOffset = 5;
        protected const int DeathFrames = 4;

        public int Health { get; set; }
        public int MaxHealth { get; protected set; }
        public int Damage { get; set; }
        protected int delay;
        private int delayCount;
        protected int damageDelay;
        protected bool damaged;
        protected Color color;
        public ICollidable.objectType type { get; set; }
        public Vector2 PreviousPosition { get; set; }
        public Vector2 Position { get; set; }
        protected Vector2 initPosition;
        protected float speed;
        protected int deadCount;

        private bool canMove = true;

        public Vector2 Velocity { get; set; }
        public ISprite Sprite { get; set; }

        private void TakeDamage(int damage)
        {
            if (!damaged && damage > 0)
            {
                Health -= damage;
                damaged = true;
                color = Color.Red;
                SoundManager.Manager.enemyDamageSound().Play();
            }
        }

        protected virtual void ReverseDirection()
        {
            Velocity *= -1;
        }

        private void Move(GameTime gameTime)
        {
            if (canMove)
            {
                PreviousPosition = Position;
                Position += speed * Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                Position = PreviousPosition;
            }
        }

        private void DamageControl()
        {
            if (damaged)
            {
                damageDelay++;
                if (damageDelay % 12 == 0)
                {
                    damaged = false;
                    color = Color.White;
                }
            }
        }

        protected virtual void Death(CollisionManager manager)
        {
            if (deadCount >= DeathFrames)
            {
                manager.collidables.Remove(this);
                SoundManager.Manager.enemyDeadSound().Play();
            }
        }
        
        protected abstract void Behavior(GameTime gameTime, Game1 game);

        public void Update(GameTime gameTime, Game1 game)
        {
            Move(gameTime);

            DamageControl();     

            // Ex: change direction every delay seconds
            if (delayCount % delay == 0)
            {
                Behavior(gameTime, game);
            }
            delayCount++;

            Death(game.CollisionManager);
            
            canMove = true;
        }

        public virtual void Collide(ICollidable obj, ICollidable.Edge edge)
        {

            if (obj.type == ICollidable.objectType.Player || obj.type == ICollidable.objectType.Ability)
            {
                TakeDamage(obj.Damage);
            } else if (obj.type == ICollidable.objectType.Wall || obj.type == ICollidable.objectType.Tile || obj.type == ICollidable.objectType.Door)
            {
                ReverseDirection();
                canMove = false;
            } 
        }
        

        public Rectangle GetHitBox()
        {
            return new Rectangle((int) Position.X, (int) Position.Y, Sprite.GetWidth(), Sprite.GetHeight());
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (Health <= 0)
            {
                EnemySpriteFactory.Instance.CreateEnemyExplosionSprite().Draw(spriteBatch, Position, SpriteEffects.None, Color.White);
                deadCount++;
            }
            else
            {
                Sprite.Draw(spriteBatch, Position, SpriteEffects.None, color);
            }
        }

        public void Reset(Game1 game)
        {
            Position = initPosition;
            Health = MaxHealth;
            deadCount = 0;
        }
    }
}
