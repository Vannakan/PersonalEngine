using Engine34.Entities.Animation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameCode.Entities.Misc.Scenery.Animated.Torch
{
    public class TorchAnimationTable : AnimationTable
    {
        public override void Initialize()
        {
            table.Add(1, new TorchAnim());
            base.Initialize();
        }
    }
}
