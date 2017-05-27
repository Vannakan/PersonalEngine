using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine34.Entities.Animation
{
    public class AnimationTable : IAnimationTable
    {
       protected Dictionary<object, IAnimationState> table = new Dictionary<object, IAnimationState>();

        public IAnimationState getState(object anim)
        {
            return table[anim];
        }

        public virtual void Initialize()
        {
           
            // table.Add(AnimationEvent.IDLE, new PlayerIdle(c));
        }

        public int getTableCount()
        {
            return table.Count;
        }

      
    }
}
