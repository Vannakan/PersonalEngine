using Engine34.Entities.Animation.Debug;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine34.Entities.Animation
{
    public interface IAnimationState
    {
        void Enter();
        void Exit();
        void Handle(Vector2 p,SpriteBatch sp);
        void Handle(GameTime gt);
    }
}
