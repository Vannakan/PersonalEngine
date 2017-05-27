using Engine.States.Engine;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Engine.Utility;

namespace Engine34.States.Engine
{
    public class PopupScreen : BaseScreen
    {
        public Rectangle Bounds;
        public Color color;

        public override void Initialize()
        {
            base.Initialize();
        }

        public void setBounds (int x, int y,int width, int height)
        {
            Bounds = new Rectangle(x, y, width, height);

        }

        public void setColour(Color color)
        {
            this.color = color;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            DrawPrimitives.DrawOutCamRect(Bounds, color);

            base.Draw(spriteBatch);
        }
    }
}
