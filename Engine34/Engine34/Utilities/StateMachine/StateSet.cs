using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine34.Utilities.StateMachine
{
    public abstract class StateSet
    {
        IStateTable table = null;
        IState current = null;
        public object Event
        {
            set
            {
                if (value == null)
                {
                    current.Exit();
                    current = null;
                    return;
                }
                IState i = table.getState(value);

                if (i != null)
                {
                    if (current != null)
                        current.Exit();

                    current = i;
                    current.Enter();
                }
            }
        }
    }
}
