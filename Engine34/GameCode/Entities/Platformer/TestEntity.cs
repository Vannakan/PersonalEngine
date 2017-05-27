using Engine.Entities;
using Engine.Interfaces.Behaviour;
using Engine.Managers.Behaviour;
using Engine.Managers.ServiceLocator;
using Microsoft.Xna.Framework;

namespace GameCode.Entities.Platformer
{
    /// <summary>
    /// CLASS: A Test Entity used for various demos.
    /// </summary>
    class TestEntity : Entity
    {
        /// <summary>
        /// METHOD: Initialise the basic properties of the entity.
        /// </summary>
        /// <param name="Pos"></param>
        public override void Initialize(Vector2 Pos)
        {
            
            ///SET: the mind controlling this entity
            mind = Locator.Instance.getService<IBehaviourManager>().Create<TestMind>(this);

            ///CALL: The initialise of the abstract entity class.
            base.Initialize(Pos);

            ///SET: The name of this entity.
            Name = "TestEntity";
        }
    }
}