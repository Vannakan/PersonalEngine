﻿using Engine;
using Engine.Interfaces.Resource;
using Engine.Managers.ServiceLocator;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Utility
{
    public static class DrawString
    {

        public static void Draw(string str, SpriteBatch spr, Vector2 pos, Color color, float size)
        {
            if (str != null)
                spr.DrawString(Locator.Instance.getService<IResourceLoader>().GetFont("mFont"), str, pos, color, 0f, new Vector2(0, 0), size, SpriteEffects.None, 1f);
            else
                spr.DrawString(Locator.Instance.getService<IResourceLoader>().GetFont("mFont"), "NULL STRING", pos, color, 0f, new Vector2(0, 0), size, SpriteEffects.None, 1f);

        }

        public static void Draw(string str, string font, SpriteBatch spr, Vector2 pos, Color color, float size)
        {
            spr.DrawString(Locator.Instance.getService<IResourceLoader>().GetFont(font), str, pos, color, 0f, new Vector2(0, 0), size, SpriteEffects.None, 1f);
        }
    }
}