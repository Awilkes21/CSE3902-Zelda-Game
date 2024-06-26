﻿using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using sprint0.Classes;
using sprint0.Interfaces;
using sprint0.TileClasses;

namespace sprint0
{
    public class TileSpriteFactory
    {

        private Texture2D tileSpriteSheet;
        // any other textures needed

        private static TileSpriteFactory instance = new TileSpriteFactory();

        public static TileSpriteFactory Instance
        {
            get { return instance; }
        }

        private TileSpriteFactory()
        {
            this.tileSpriteSheet = TextureStorage.GetTilesSpritesheet();
        }

        public ICollidable CreateTileType1()
        {
            return new TileType1(0, 0);
        }
        public ICollidable CreateTileType2()
        {
            return new TileType2(0, 0);
        }
        public ICollidable CreateTileType3()
        {
            return new TileType3(0, 0);
        }
        public ICollidable CreateTileType4()
        {
            return new TileType4(0, 0);
        }
        public ICollidable CreateTileType5()
        {
            return new TileType5(0, 0);
        }
        public ICollidable CreateTileType6()
        {
            return new TileType6(0, 0);
        }
        public ICollidable CreateTileType7()
        {
            return new TileType7(0, 0);
        }
        public ICollidable CreateTileType8()
        {
            return new TileType8(0, 0);
        }
        public ICollidable CreateTileType9()
        {
            return new TileType9(0, 0);
        }
        public ICollidable CreateTileType10()
        {
            return new TileType10(0, 0);
        }

        /*
         * Add create Block functions when classes have been made
        public ISprite CreateBlockSprite()
        {
            return new BlockSprite();
        }
        */
    }
}
