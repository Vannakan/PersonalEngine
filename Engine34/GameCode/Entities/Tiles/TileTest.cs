﻿using Engine.Entities;
using Engine.Interfaces.Behaviour;
using Engine.Managers.ServiceLocator;
using Microsoft.Xna.Framework;

namespace GameCode.Entities.Tiles
{
    class TileTest : Entity
    {


        public override void Initialize(Vector2 Pos)
        {
            ///SET: The mind of this entity using the Service Locator and then Behaviour Manager to create a PlayerMind.
            mind = Locator.Instance.getService<IBehaviourManager>().Create<TileTestMind>(this);

            ///CALL: The Abstract class Entity Initialise Method.
            base.Initialize(Pos);

            ///SET: The name of this Entity to Player.
            Name = "TestTile";
        }

    }
}
