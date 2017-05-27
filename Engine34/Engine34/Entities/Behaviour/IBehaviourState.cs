using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine34.Entities.Behaviour
{
    public interface IBehaviourState
    {
        void Enter();
        void Exit();
        void Handle(GameTime gameTime);
    }
}
