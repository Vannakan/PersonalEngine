using Engine.Entities;
using Engine.Interfaces.Behaviour;
using Engine.Interfaces.Render;
using Engine.Managers.ServiceLocator;
using GameCode.Entities.Platformer;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameCode.Entities.Human
{
    class BenTipton : Entity, IDrawableComponent
    {

        public override void Initialize(Vector2 Pos)
        {
            ///SET: The mind of this entity using the Service Locator and then Behaviour Manager to create a PlayerMind.
            mind = Locator.Instance.getService<IBehaviourManager>().Create<BenTiptonMind>(this);

            ///CALL: The Abstract class Entity Initialise Method.
            base.Initialize(Pos);

            ///SET: The name of this Entity to Player.
            Name = "C";
        }

    }
}
