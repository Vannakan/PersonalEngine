using Engine34.Entities.Animation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine34.Entities
{
    public interface IAnimation
    {
        bool hasLifecycle { get; set; }
        int LifecycleCounter { get; set; }

        void Draw(SpriteBatch spriteBatch);
        void Update(GameTime gameTime);
        void Initialize(AnimationTable animation,Vector2 pos);
        void Unload();
       
    }
}
