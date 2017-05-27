using Engine.Interfaces.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine34.Managers.Render
{
    ///
    public class LightMask : ILightMask
    {
        public Vector2 Position { get; set; }
        public Texture2D Texture { get; set; }
        public float Scale { get; set; }
        public IMind e { get; set; }

       Vector2 offset;

        public LightMask(Vector2 pos, Texture2D tex)
        {
            Position = pos;
            Texture = tex;
            Scale = 1f;
        }

        public LightMask(Vector2 pos, Texture2D tex, float scale)
        {
            Position = pos;
            Texture = tex;
            Scale = scale;
        }

        public LightMask(IMind ent, Texture2D tex, float scale)
        {
            e = ent;
      
            Texture = tex;
            Scale = scale;

            var s = tex.Bounds.Size;
            var a = tex.Bounds.Center.ToVector2();
            var b = e.getEntity().Texture.Bounds.Center.ToVector2() - s.ToVector2() / 2;
            offset = b;
            Position = b;

        }

        public void Draw(SpriteBatch spr)
        {
            if (e != null)
            {
                var s = Texture.Bounds.Size;
                var a = Texture.Bounds.Center.ToVector2();
                var b = e.getEntity().Texture.Bounds.Center.ToVector2()- s.ToVector2() / 2 * Scale;
                Position = b + e.Position;
            }
            spr.Draw(Texture, Position, null, Color.White, 0f, Vector2.Zero, Scale, SpriteEffects.None, 1f);
        }
    }
}
