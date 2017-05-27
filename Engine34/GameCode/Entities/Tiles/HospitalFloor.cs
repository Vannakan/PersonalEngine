using Engine.Entities;
using Engine.Interfaces.Behaviour;
using Engine.Interfaces.Resource;
using Engine.Managers.ServiceLocator;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameCode.Entities.Tiles
{
    class HospitalFloor : Entity
    {

        public override void Initialize(Vector2 Pos)
        {
            ///SET: The mind of this entity using the Service Locator and then Behaviour Manager to create a PlayerMind.
            mind = Locator.Instance.getService<IBehaviourManager>().Create<HospitalFloorMind>(this);
            ///CALL: The Abstract class Entity Initialise Method.
            base.Initialize(Pos);

            ///SET: The name of this Entity to Player.
            Name = "TestTile";
        }
    }
}
