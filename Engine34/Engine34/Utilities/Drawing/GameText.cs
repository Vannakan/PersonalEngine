using Engine.Interfaces.Render;
using Engine.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Utilities
{
    public class GameText : IDrawableComponent
    {
        private string text, font;
        private float size;
        private Vector2 pos;
        private Color color;
        public GameText(string text, string font, Vector2 pos, Color color, float size)
        {
            this.text = text;
            this.font = font;
            this.pos = pos;
            this.color = color;
            this.size = size;
        }

        public void Draw(SpriteBatch spr)
        {
            if (font == null)
                DrawString.Draw(text, spr, pos, color, size);
            else
                DrawString.Draw(text, font, spr, pos, color, size);

        }

        public void UpdatePosition(Vector2 newpos)
        {
            pos = newpos;
        }

        public void UpdateText(string tex)
        {
            text = tex;
        }

    }
}