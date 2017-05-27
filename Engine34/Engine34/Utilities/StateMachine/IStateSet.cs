using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine34.Utilities.StateMachine
{
   public interface IStateSet
    {
        void Initialize(IStateTable table, Vector2 Pos);
        void SwitchState(int state);
        void Update(GameTime gt);
        void Update(object gameTime1, object gameTime2);
    }
}
