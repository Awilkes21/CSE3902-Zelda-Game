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
    public class Boomerang : ItemType
    {
        public Boomerang(int xCoord, int yCoord)
        {
            this.SetLocation(xCoord, yCoord);
            Texture2D texture = TextureStorage.GetBoomerangSpritesheet();
            this.SetTexture(texture, new Rectangle(0, 0, 5, 8));
        }
    }
}
