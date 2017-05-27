using Engine.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine34.Entities.Behaviour
{
   public class BehaviourSet
    {
        List<Behaviour> BehaviourList = new List<Behaviour>();

        private IMind possessed;

        public IBehaviourState current = null;

        public BehaviourTable bat = null;

        public object Event
        {
            set
            {
                if(value == null)
                {
                    current.Exit();
                    current = null;
                    return;
                }

                IBehaviourState i = bat.getState(value);

                if(i != null)
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
