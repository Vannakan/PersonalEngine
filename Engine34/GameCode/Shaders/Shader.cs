using Engine34.Interfaces.Render;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Engine.Interfaces.Render;
using Microsoft.Xna.Framework;

namespace GameCode.Shaders
{
    public abstract class Shader : IShader, IDrawableComponent
    {
        protected Effect effect1;
        protected Texture2D tex;

        public Effect effect
        {
            get
            {
                return effect1;
            }

            set
            {
                effect1 = value;
            }
        }
        public Shader(Texture2D t, Effect e)
        {
            tex = t;
            effect1 = e;
        }

        public Shader()
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, Vector2.Zero, Color.White);

        }
    }
}
