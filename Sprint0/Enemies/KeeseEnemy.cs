﻿using System;
using Microsoft.Xna.Framework;
using sprint0.Factories;
using sprint0.Interfaces;
namespace sprint0.Enemies
{
    public class KeeseEnemy : Enemy
    {
        private const int BehaviorDelay = 50;
        private const int DirectionChange = 2;
        private const int RandBound = 2;

        private int previous;
        public KeeseEnemy(Vector2 position, float speed)
        {
            initPosition = position;
            Position = position;
            PreviousPosition = position;
            Sprite = EnemySpriteFactory.Instance.CreateKeeseSprite();
            this.speed = speed;
            Velocity = Vector2.One;
            delay = BehaviorDelay;
            previous = 1;
            MaxHealth = 1;
            Health = MaxHealth;
            Damage = 1;
            damageDelay = 0;
            damaged = false;
            color = Color.White;
            deadCount = 0;
            type = ICollidable.objectType.Enemy;
        }

        protected override void Behavior(GameTime gameTime, Game1 game)
        {
            Random rand = new Random();
            Random rand2 = new Random();

            // randomly choose movement direction
            int x = rand.Next(-1, RandBound) % DirectionChange;
            int y = rand2.Next(-1, RandBound) % DirectionChange;
            if (x == 0 && y == 0) x = previous;
            Velocity = new Vector2 (x, y);
        }
    }
}
