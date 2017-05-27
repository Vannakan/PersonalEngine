using Engine34.Entities.Animation.Debug;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine34.Entities.Animation
{
    public class PlayerAnimationTable : AnimationTable
    {




        public override void Initialize()
        {
            table.Add(1, new PlayerLeft());
            table.Add(2, new PlayerRight());
            table.Add(3, new PlayerDown());
            table.Add(4, new PlayerUp());
            base.Initialize();
        }

    }
}
