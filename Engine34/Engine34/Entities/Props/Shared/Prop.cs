using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine34.Entities.Props.Shared
{
   abstract class Prop : IProp
    {
        Texture2D tex;
        Vector2 Pos;
      

        public void Initialize(Vector2 pos)
        {
            Pos = pos;
        }
        public virtual void Draw(SpriteBatch sp)
        {
            sp.Draw(tex, Pos, Color.White);
        }

        public virtual void Destroy()
        {
            //Unload
        }
     
    }
}
