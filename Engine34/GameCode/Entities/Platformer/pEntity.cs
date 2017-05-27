using Engine.Entities;
using Engine.Interfaces.Behaviour;
using Engine.Interfaces.Render;
using Engine.Managers.Behaviour;
using Engine.Managers.ServiceLocator;
using Microsoft.Xna.Framework;

namespace GameCode.Entities.Platformer
{
    /// <summary>
    /// CLASS: The player Entity class
    /// </summary>
    class pEntity : Entity, IDrawableComponent
    {

        /// <summary>
        /// METHOD: Initialise the player by setting it's position and mind
        /// </summary>
        /// <param name="Pos"></param>
        public override void Initialize(Vector2 Pos)
        {
            ///SET: The mind of this entity using the Service Locator and then Behaviour Manager to create a PlayerMind.
            mind = Locator.Instance.getService<IBehaviourManager>().Create<PlayerMind>(this);

            ///CALL: The Abstract class Entity Initialise Method.
            base.Initialize(Pos);

            ///SET: The name of this Entity to Player.
            Name = "Player";
        }





    }
}