using Engine.Entities;
using Engine.Interfaces.Behaviour;
using Engine.Managers.ServiceLocator;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameCode.Entities.Human
{
    class FemaleNurse : Entity
    {
        /// <summary>
        /// METHOD: Initialise the basic properties of the entity.
        /// </summary>
        /// <param name="Pos"></param>
        public override void Initialize(Vector2 Pos)
        {
            ///SET: the mind controlling this entity
            mind = Locator.Instance.getService<IBehaviourManager>().Create<FemaleNurseMind>(this);

            ///CALL: The initialise of the abstract entity class.
            base.Initialize(Pos);

            ///SET: The name of this entity.
            Name = "Female";
        }

        
    }
}
