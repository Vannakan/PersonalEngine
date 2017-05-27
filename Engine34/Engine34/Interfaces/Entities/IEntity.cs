using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Engine.Interfaces.Entities
{
    /// <summary>
    /// INTERFACE: An Interface that all entities must subscribe to and handles the implementation and properties for them.
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// GET: SET: The Texture of the Entity
        /// </summary>
        Texture2D Texture
        {
            get;
            set;
        }

        IMind Mind
        {
            get;
            set;
        }

        /// <summary>
        /// GET: SET: The Position of the Entity
        /// </summary>
        Vector2 Position
        {
            get;
            set;
        }

        /// <summary>
        /// GET: The velocity of the Entity
        /// </summary>
        Vector2 Velocity
        {
            get;
        }

        /// <summary>
        /// METHOD: Draw
        /// </summary>
        /// <param name="spriteBatch"></param>
        void Draw(SpriteBatch spriteBatch);

        /// <summary>
        /// GET: The unique ID for the Entity
        /// </summary>
        int UniqueID
        {
            get;
        }

        /// <summary>
        /// GET: An Axis aligned bounding box for the bounds of the Entity.
        /// </summary>
        Rectangle Bounds
        {
            get;
        }

        /// <summary>
        /// GET: SET: A boolean to state whether the entity needs to be included in any calculations for collision
        /// </summary>
        bool isCollidable
        {
            get;
            set;
        }

        /// <summary>
        /// GET: SET: A Boolean for whether the entity needs to be drawn.
        /// </summary>
        bool isVisible
        {
            get;
            set;
        }

        /// <summary>
        /// METHOD: Initialise, give the Entity a position and texture
        /// </summary>
        /// <param name="Position"></param>
        void Initialize(Vector2 Position);

        /// <summary>
        /// GET: SET: The name of the Entity.
        /// </summary>
        string Name
        {
            get;
            set;
        }

        void Destroy();
    }
}