using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine34.Managers.Render
{
    public interface ILightMask
    {
        Vector2 Position { get; set; }
        Texture2D Texture { get; set; }
        float Scale { get; set; }
        void Draw(SpriteBatch spr);
    }
}
