﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sprint0.TileClasses
{
    public class TileType1 : TileType
    {
        public TileType1(int x, int y)
        {
            Type = ICollidable.ObjectType.Tile;
            IsCollidable = false;

            this.SetLocation(x, y);
            this.SetTextureCoords(0, 0);
        }
        
    }
}
