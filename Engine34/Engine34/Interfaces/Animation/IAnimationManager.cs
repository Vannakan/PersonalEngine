using Engine.Interfaces.ServiceLocator;
using Engine34.Entities;
using Engine34.Entities.Animation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine34.Interfaces.Animation
{
    public interface IAnimationManager : IUpdService
    {

        void addAnim(IAnimation set);

        void removeAnim(IAnimation set);

        void Initialize();

        void Update(GameTime gameTime);

        void Draw(SpriteBatch spriteBatch);



    }
}
