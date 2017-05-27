using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine34.Utilities.StateMachine
{
    class StateMachineManager
    {
        //Holds and updates state machines
        List<IStateSet> stateSets = new List<IStateSet>();

        public StateMachineManager()
        {

        }

        public void Initialize()
        {

        }

        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < stateSets.Count; i++)
            {
                stateSets[i].Update(gameTime);
            }
        }

        public void AddStateSet(IStateSet set)
        {
            stateSets.Add(set);
        }

        public void RemoveStateSet(IStateSet set)
        {
            for(int i = 0; i < stateSets.Count; i++)
            {
                if(stateSets[i] == set)
                {
                    stateSets.RemoveAt(i);
                }
            }
        }
    }
}
