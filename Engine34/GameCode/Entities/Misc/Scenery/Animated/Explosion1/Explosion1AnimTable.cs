using Engine34.Entities.Animation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameCode.Entities.Misc.Scenery.Animated.Explosion1
{
    class Explosion1AnimTable : AnimationTable
    {
        public override void Initialize()
        {
            table.Add(1, new Explosion1Anim());
            base.Initialize();
        }
    }
}
