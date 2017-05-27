using Engine.Interfaces.Entities;
using Engine.Interfaces.Render;
using Engine34.Entities.Animation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Engine.Entities
{
    /// <summary>
    /// ABSTRACT CLASS: The Base entity class. All entities extend this class. It contains the basic setup and properties
    /// of each entity.
    /// </summary>
    public class Entity : IEntity, IDrawableComponent
    {

        //Animation Set
        protected AnimationSet set;

       
        /// DECLARE: The mind that controls the entity.
        protected IMind mind;

        public  IMind Mind
        {
            get
            {
                return mind;
            }
            set
            {
                mind = value;
            }
        }
        /// <summary>
        /// GET: The velocity of the entity.
        /// </summary>
        public Vector2 Velocity
        {
            get
            {
                return mind.Velocity;
            }
        }

        /// <summary>
        /// GET: SET: The unique ID for an Entity.
        /// </summary>
        public int UniqueID
        {
            get;
            protected set;
        }

        /// <summary>
        /// GET: SET: whether the entity needs to be drawn or not.
        /// </summary>
        public bool isVisible
        {
            get;
            set;
        }

        /// <summary>
        /// GET: SET: The name of the entity
        /// </summary>
        private string name = "";
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        /// <summary>
        /// GET: SET: The position of the entity.
        /// </summary>
        public Vector2 Position
        {
            get
            {
                return mind.Position;
            }
            set
            {
                mind.Position = value;
            }
        }

        /// <summary>
        /// GET: SET: The texture of the entity.
        /// </summary>
        public Texture2D Texture
        {
            get;
            set;
        }

        /// <summary>
        /// GET: SET: The Axis aligned bounds of the entity.
        /// </summary>
        public Rectangle Bounds
        {
            get
            {
                return new Rectangle((int)mind.Position.X, (int)mind.Position.Y, Texture.Width, Texture.Height);
            }
        }

        /// <summary>
        /// GET: SET: A bool to check whether the entity needs to be included in collision calculations
        /// </summary>
        public bool isCollidable
        {
            get;
            set;
        }

        /// <summary>
        /// CONSTRUCTOR: Sets the basic properties for the entity.
        /// </summary>
        public Entity()
        {
            ///SET: The unique ID of the entity.
            UniqueID = Constants.ID;

            ///INCREMENT: The ID count for the next entity
            Constants.ID++;

            ///SET: The name of the entity
            Name = GetType().ToString();
        }

        /// <summary>
        /// METHOD: Initialises the basic properties of this entity, such as the mind
        /// </summary>
        /// <param name="Pos"></param>
        public virtual void Initialize(Vector2 Pos)
        {
            ///IF: The mind is not null, THEN initialise the mind.
            if (mind != null)
                mind.Initialize(Pos);
        }

        /// <summary>
        /// METHOD: Draw the entitity
        /// </summary>
        /// <param name="spriteBatch"></param>
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            ///CALL: The spritebatch draw method.
            spriteBatch.Draw(Texture, mind.Position, Color.White);
        }

        public void Destroy()
        {
            Mind = null;
            mind = null;

        }
    }
}