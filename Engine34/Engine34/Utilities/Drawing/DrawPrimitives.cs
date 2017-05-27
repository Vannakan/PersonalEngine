using Engine.Interfaces.Render;
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
    public static class DrawPrimitives
    {
        //Temporary
        public static void DrawRect(SpriteBatch spriteBatch, Rectangle Bounds, Color color)
        {
            var t = new Texture2D(Engine.Constants.g, 1, 1);
            t.SetData(new[] {
    Color.White
   });
            spriteBatch.Begin();
            spriteBatch.Draw(t, new Rectangle(Bounds.Left, Bounds.Top, 1, Bounds.Height), color); // Left
            spriteBatch.Draw(t, new Rectangle(Bounds.Right, Bounds.Top, 1, Bounds.Height), color); // Right
            spriteBatch.Draw(t, new Rectangle(Bounds.Left, Bounds.Top, Bounds.Width, 1), color); // Top
            spriteBatch.Draw(t, new Rectangle(Bounds.Left, Bounds.Bottom, Bounds.Width, 1), color); // Bottom
            spriteBatch.End();
        }

        public static void DrawOutCamRect( Rectangle Bounds, Color color)
        {
            square s = new square();
            s.setBounds(Bounds, color);
            Locator.Instance.getService<IRenderManager>().addDrawable(s);
            
              
            
        }

        public static square DrawCamRect(Rectangle Bounds, Color color)
        {
            square s = new square();
            s.setBounds(Bounds, color);
            Locator.Instance.getService<IRenderManager>().addCamDrawable(s);

            return s;

        }

    }

    public class square : IDrawableComponent
    {
       public Rectangle Bounds;
        Color color;

        public void setBounds(Rectangle b, Color c)
        {
            Bounds = b;
            color = c;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            var t = new Texture2D(Engine.Constants.g, 1, 1);
            t.SetData(new[] {
    Color.White
   });
            spriteBatch.Draw(t, new Rectangle(Bounds.Left, Bounds.Top, 1, Bounds.Height), color); // Left
            spriteBatch.Draw(t, new Rectangle(Bounds.Right, Bounds.Top, 1, Bounds.Height), color); // Right
            spriteBatch.Draw(t, new Rectangle(Bounds.Left, Bounds.Top, Bounds.Width, 1), color); // Top
            spriteBatch.Draw(t, new Rectangle(Bounds.Left, Bounds.Bottom, Bounds.Width, 1), color); // Bottom
        }
    }
    }
