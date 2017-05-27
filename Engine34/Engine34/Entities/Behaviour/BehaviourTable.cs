using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine34.Entities.Behaviour
{
    public class BehaviourTable : IBehaviourTable
    {
        protected Dictionary<object, IBehaviourState> table = new Dictionary<object, IBehaviourState>();

        public IBehaviourState getState(object anim)
        {
            return table[anim];
        }

        public virtual void Initialize()
        {

        }

        public int getTableCount()
        {
            return table.Count;
        }
    }
}
