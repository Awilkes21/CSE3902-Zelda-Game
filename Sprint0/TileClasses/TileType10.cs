﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sprint0.TileClasses
{
    public class TileType10 : TileType
    {
        public TileType10(int x, int y)
        {
            this.SetLocation(x, y);
            this.SetTextureCoords(4, 1);
            this.SetCollidable(true);
            type = ICollidable.objectType.Tile;
        }

        public override Type GetObjectType()
        {
            return typeof(TileType);
        }
    }
}
