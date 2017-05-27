using Engine.Interfaces.Entities;
using Engine34.Interfaces.Collision;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Engine.Interfaces.Collision
{
    /// <summary>
    ///INTERFACE: An interface used for objects that will need collision calculation during the game loop
    /// </summary>
    public interface ICollidable
    {
        /// <summary>
        /// GET: The axis aligned bounds of the object.
        /// </summary>
        Rectangle Bounds
        {
            get;
        }

        /// <summary>
        /// GET: SET: The position of the object.
        /// </summary>
        Vector2 Position
        {
            get;
            set;
        }

        /// <summary>
        /// GTE: SET: A boolean to tell whether an object is to be tested for collision or not.
        /// </summary>
        bool isCollidable
        {
            get;
            set;
        }

        /// <summary>
        /// GET: SET: A boolean to tell whether an object is currently colliding or not.
        /// </summary>
        bool isColliding
        {
            get;
            set;
        }

        /// <summary>
        /// ApplyImpulse is the response from collision. After the object has been moved by the minimum translation vector
        /// It will have a reactionary force applied to it in order to simulate physics.
        /// </summary>
        /// <param name="cVelocity"></param>
        void ApplyImpulse(Vector2 cVelocity);

        /// <summary>
        /// A list of hitboxes used for calculating collision.
        /// </summary>
        List<IHitbox> Hits
        {
            get;
            set;
        }

        ICollidable getCollidable();

        IEntity getEntity();



    }
}