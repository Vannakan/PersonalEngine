using Engine.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine.Events.CollisionEvent;

namespace GameCode.Entities.Types
{
    public abstract class DamagableMind : Mind
    {

        public override void OnSATCollision(object sender, CollisionEventArgs e)
        {
            if(e.A == this && e.B.GetType() == typeof(Mind))
            {
                e.B.Hits[0].Mind.Health -= Dmg;
                Health -= e.B.Hits[0].Mind.Dmg;
            }
            else if(e.B == this && e.A.GetType() == typeof(Mind))
            {
                e.A.Hits[0].Mind.Health -= Dmg;
                Health -= e.A.Hits[0].Mind.Dmg;
            }
           // base.OnSATCollision(sender, e);
        }
    }
}
