﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0.Classes;
using sprint0.DoorClasses;
using sprint0.Enemies;
using sprint0.Interfaces;
using sprint0.TileClasses;

namespace sprint0.RoomClasses
{
    public enum Direction
    {
        UP,
        RIGHT,
        DOWN,
        LEFT
    }

    public class Room
    {
        private Game1 game;

        private List<TileType> tileList = new List<TileType>();
        private List<Door> doorList = new List<Door>();
        private Dictionary<Direction, LevelConfig> roomMap = new Dictionary<Direction, LevelConfig>();

        private Rectangle bounds = new Rectangle();

        public Point roomOffset = new Point();
        private Boolean transitioning = false;
        public Boolean RoomReady { get; set; }
        private Direction transitionDirection;

        private Room nextRoom;

        public LevelConfig levelConfig { get; set; }
        public Room(Game1 game, LevelConfig cfg)
        {
            this.game = game;
            levelConfig = cfg;

            RoomReady = false;
            bounds = new Rectangle(0, 0, 1280, 880);

            foreach (ICollidable collidable in game.CollisionManager.collidables)
            {
                if (collidable.type == ICollidable.objectType.Enemy || collidable.type == ICollidable.objectType.Wall || 
                    collidable.type == ICollidable.objectType.Tile || collidable.type == ICollidable.objectType.Door)
                    game.CollidablesToDelete.Add(collidable);
            }

            game.CollidablesToAdd.Add(new TopLeftWall());
            game.CollidablesToAdd.Add(new TopRightWall());
            game.CollidablesToAdd.Add(new RightTopWall());
            game.CollidablesToAdd.Add(new RightBottomWall());
            game.CollidablesToAdd.Add(new BottomRightWall());
            game.CollidablesToAdd.Add(new BottomLeftWall());
            game.CollidablesToAdd.Add(new LeftBottomWall());
            game.CollidablesToAdd.Add(new LeftTopWall());

            int rows = levelConfig.TileIds.Count;

            for (int y = 0; y < rows; y++)
            {
                int columns = levelConfig.TileIds[y].Count;
                for (int x = 0; x < columns; x++)
                {
                    int currentTileId = levelConfig.TileIds[y][x];

                    TileType tile = GetTileFromId(currentTileId, 160 + x * 80, 160 + y * 80);
                    tileList.Add(tile);
                    if (tile.IsCollidable)
                        game.CollidablesToAdd.Add(tile);
                }
            }

            for (int i = 0; i < 4; i++)
            {
                int currentDoorId = levelConfig.DoorIds[i];

                Door door = GetDoorFromId(i);
                door.Id = currentDoorId;

                doorList.Add(door);
                game.CollidablesToAdd.Add(door);
            }


            for (int i = 0; i < 4; i++)
            {
                int currentDestination = levelConfig.Destinations[i];

                if (currentDestination != -1)
                {
                    LevelConfig destinationLevelConfig = game.GameConfig.LevelConfigs[currentDestination];

                    roomMap.Add((Direction)i, destinationLevelConfig);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(TextureStorage.GetWallsSpritesheet(),
                             new Rectangle(bounds.Location + roomOffset, bounds.Size), Color.White);

            foreach (TileType tile in tileList)
            {
                tile.Draw(spriteBatch, roomOffset);
            }
            foreach (Door door in doorList)
            {
                if (door.HasCollided && (door.Id == 1 || door.Id == 4) && !transitioning && RoomReady)
                {
                    transitionDirection = door.TransitionDirection;
                    transitioning = true;

                    LevelConfig destinationLevelConfig = roomMap[door.TransitionDirection];

                    nextRoom = new Room(game, destinationLevelConfig);
                    nextRoom.levelConfig = destinationLevelConfig;
                } else
                {
                    door.HasCollided = false;
                }

                door.Draw(spriteBatch, roomOffset);
            }

            TryTransition(transitionDirection);

            if (nextRoom != null && transitioning)
            {
                nextRoom.Draw(spriteBatch);
                foreach (Door nextDoor in nextRoom.doorList)
                {
                    nextDoor.Draw(spriteBatch, nextRoom.roomOffset);
                }
            }

        }

        public void Initialize()
        {
            RoomReady = true;

            foreach (TileType tile in tileList)
            {
                if (tile.IsCollidable)
                    game.CollidablesToAdd.Add(tile);
            }

            foreach (KeyValuePair<int, Point> enemy in levelConfig.Enemies)
            {
                game.CollidablesToAdd.Add(GetEnemyFromId(enemy.Key, enemy.Value.X, enemy.Value.Y));
            }
        }

        public void TryTransition(Direction dir)
        {
            if (transitioning && RoomReady)
            {
                if (dir == Direction.LEFT && roomOffset.X < 1285)
                {
                    roomOffset.X += 5;

                    nextRoom.roomOffset.X = -1280 + roomOffset.X;
                    nextRoom.roomOffset.Y = 0;

                    game.Player.Position = new Vector2(1100 - game.Player.GetHitBox().Width, 880 / 2);
                }
                else if (dir == Direction.RIGHT && Math.Abs(roomOffset.X) < 1285)
                {
                    roomOffset.X -= 5;

                    nextRoom.roomOffset.X = 1280 + roomOffset.X;
                    nextRoom.roomOffset.Y = 0;

                    game.Player.Position = new Vector2(180 + game.Player.GetHitBox().Width, 880 / 2);
                }
                else if (dir == Direction.DOWN && roomOffset.Y < 885)
                {
                    roomOffset.Y += 5;

                    nextRoom.roomOffset.X = 0;
                    nextRoom.roomOffset.Y = -880 + roomOffset.Y;

                    game.Player.Position = new Vector2(1280 / 2, 180 + game.Player.GetHitBox().Height);
                }
                else if (dir == Direction.UP && Math.Abs(roomOffset.Y) < 885)
                {
                    roomOffset.Y -= 5;

                    nextRoom.roomOffset.X = 0;
                    nextRoom.roomOffset.Y = 880 + roomOffset.Y;

                    game.Player.Position = new Vector2(1280 / 2, 700 - game.Player.GetHitBox().Height);
                }
                else
                {
                    roomOffset = new Point(0, 0);
                    transitioning = false;

                    game.Room = nextRoom;
                    game.Room.roomOffset = new Point();
                    game.Room.Initialize();

                    foreach (Door door in doorList)
                    {
                        door.HasCollided = false;
                    }
                }
            }
        }

        public void Update(GameTime gameTime, Game1 game)
        {

        }

        private TileType GetTileFromId(int id, int x, int y)
        {
            switch (id)
            {
                case 0:
                    return new TileType1(x, y);
                case 1:
                    return new TileType2(x, y);
                case 2:
                    return new TileType3(x, y);
                case 3:
                    return new TileType4(x, y);
                case 4:
                    return new TileType5(x, y);
                case 5:
                    return new TileType6(x, y);
                case 6:
                    return new TileType7(x, y);
                case 7:
                    return new TileType8(x, y);
                case 8:
                    return new TileType9(x, y);
                case 9:
                    return new TileType10(x, y);
            }
            return new TileType1(x, y);
        }
        private Door GetDoorFromId(int id)
        {
            switch (id)
            {
                case 0:
                    return new TopDoor();
                case 1:
                    return new RightDoor();
                case 2:
                    return new BottomDoor();
                case 3:
                    return new LeftDoor();
            }
            return null;
        }
        private Enemy GetEnemyFromId(int id, int x, int y)
        {
            Vector2 location = new Vector2(x, y);
            switch (id)
            {
                case 0:
                    return new AquamentusBoss(location, 0);
                case 1:
                    return new GoriyaEnemy(location, 0);
                case 2:
                    return new KeeseEnemy(location, 0);
                case 3:
                    return new OldManNPC(location);
                case 4:
                    return new StalfosEnemy(location, 0);
                case 5:
                    return new ZolEnemy(location, 0);
            }
            return null;
        }
    }
}
