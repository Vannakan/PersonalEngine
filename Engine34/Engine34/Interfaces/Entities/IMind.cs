using Engine.Interfaces.Collision;
using Engine.Managers.Collision;
using Engine34.Interfaces.Collision;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Engine.Interfaces.Entities
{
    /// <summary>
    /// INTERFACE: An interface that provides Implementation for any object that is a mind which controls the behaviour
    /// of an entity.
    /// </summary>
    public interface IMind
    {
        /// <summary>
        /// GET: SET: A bool for whether the mind is currently attached to an Entity
        /// </summary>
        bool Active
        {
            get;
            set;
        }

        int Health
        {
            get;
            set;

        }

        int Dmg
        {
            get;
            set;
        }

        float MaxSpeed
        {
            get;
            set;
        }

        int Team
        {
            get;
            set;
        }

        List<IHitbox> Hits
        {
            get;
            set;
        }

        ICollidable Parent
        {
            get;
            set;
        }

        /// <summary>
        /// METHOD: Update, is run every frame and is the main loop for any mind.
        /// </summary>
        /// <param name="gameTime">the Monogame GameTime Property</param>
        void Update(GameTime gameTime);

        /// <summary>
        /// GET: SET: The velocity of the mind
        /// </summary>
        Vector2 Velocity
        {
            get;
            set;
        }

        Vector2 Acceleration
        {
            get;
            set;
        }

        /// <summary>
        /// GET: SET: The position of the mind.
        /// </summary>
        Vector2 Position
        {
            get;
            set;
        }

        /// <summary>
        /// GET: The unique ID for the mind
        /// </summary>
        int UniqueID
        {
            get;
        }

        /// <summary>
        /// METHOD: Initialise the position and texture
        /// </summary>
        /// <param name="Position">The position the mind will be initialised at</param>
        /// <param name="t">The string that the texture will be created from</param>
        void Initialize(Vector2 Position, string t);

        /// <summary>
        /// METHOD: Initialise just the position in case a texture has already been set.
        /// </summary>
        /// <param name="Position">The position the mind will be iniitalised at</param>
        void Initialize(Vector2 Position);

        /// <summary>
        /// METHOD: Unload the mind and send it to the garbage collector.
        /// </summary>
        void Unload();

        /// <summary>
        /// METHOD: Get the entity which a mind controls.
        /// </summary>
        /// <returns>The IENtity this mind controls</returns>
        IEntity getEntity();

        /// <summary>
        /// METHOD: Return an object that will be calculated for collision
        /// </summary>
        /// <returns>An ICollidable of the current object</returns>
        ICollidable getCollidable();

        /// <summary>
        /// METHOD: Link the mind to an entity.
        /// </summary>
        /// <param name="e">The entity to be linked to this mind</param>
        void Link(IEntity e);

        void ApplyForce(Vector2 force);
        void UpdatePhysics();
        void ApplyImpulse(Vector2 cVelocity);

        void Talk(IMind e);
    }
}