using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine34.Utilities.StateMachine
{
    public abstract class StateTable : IStateTable
    {
        protected Dictionary<object, IState> table = new Dictionary<object, IState>();

        public IState getState(object anim)
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
