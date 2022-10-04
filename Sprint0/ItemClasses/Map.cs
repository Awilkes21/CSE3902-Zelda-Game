﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sprint0.ItemClasses
{
    public class Map : ItemType
    {
        public Map()
        {
            this.SetLocation(750, 360);
            Texture2D texture = TextureStorage.GetMapSpritesheet();
            this.SetTexture(texture, new Rectangle(0, 0, 8, 16));
        }
    }
}