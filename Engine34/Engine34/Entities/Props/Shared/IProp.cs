using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine34.Entities.Props.Shared
{
    public interface IProp
    {
        void Initialize(Vector2 Position);
        void Draw(SpriteBatch sp);
        void Destroy();
        
    }
}
