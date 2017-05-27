using Engine.Events.CollisionEvent;
using Engine.Grid;
using Engine.Interfaces.Collision;
using Engine.Interfaces.Entities;
using Engine.Interfaces.Resource;
using Engine.Managers.Collision;
using Engine.Managers.ServiceLocator;
using Engine34.Entities.Behaviour;
using Engine34.Interfaces.Collision;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Engine.Entities
{
    /// <summary>
    /// ABSTRACT CLASS: Mind controls the behaviour for a specific entity. The entity is simply the sprite that is drawn
    /// whilst Mind controls how it acts.
    /// </summary>
    /// 
    public abstract class Mind : IMind, ICollidable
    {
        /// <summary>
        /// GET: SET: The unique ID for the mind
        /// </summary>
        /// 
        protected BehaviourSet set;
        public int UniqueID
        {
            get;
            set;
        }

        private ICollidable parent;

        public ICollidable Parent
        {
            get
            {
                return parent;
            }
            set
            {
                parent = value;
            }
        }

        private int team;

        public int Team
        {
            get
            {
                return team;
            }
            set
            {
                team = value;
            }
        }

        /// <summary>
        /// GET: SET: Whether the mind is active and in use or not.
        /// </summary>
        public bool Active
        {
            get;
            set;
        }

        ///DECLARE: The entity this mind will control
        protected IEntity e;

        /// <summary>
        /// GET: SET: The velocity of the mind 
        /// </summary>
        protected Vector2 velocity;
        public Vector2 Velocity
        {
            get
            {
                return velocity;
            }
            set
            {
                velocity = value;
            }
        }

        /// <summary>
        /// GET: SET: The acceleration of the mind
        /// </summary>
        protected Vector2 acceleration = new Vector2(0, 0);
        public Vector2 Acceleration
        {
            get
            {
                return acceleration;
            }
            set
            {
                acceleration = value;
            }
        }

        /// <summary>
        /// GET: SET: The inverted Mass of the mind
        /// </summary>
        protected float iMass;
        public float Mass
        {
            get
            {
                return iMass;
            }
            set
            {
                iMass = value;
            }
        }

        /// <summary>
        /// GET: SET: The restitution of the mind
        /// </summary>
        protected float restitution;
        public float Restitution
        {
            get
            {
                return restitution;
            }
            set
            {
                restitution = value;
            }
        }

        /// <summary>
        /// GET: SET: the damping acting on the mind
        /// </summary>
        protected float damping;
        public float Damping
        {
            get
            {
                return damping;
            }
            set
            {
                damping = value;
            }
        }

        /// <summary>
        /// GET: SET: The list of hitboxes
        /// </summary>
        protected List<IHitbox> hits = new List<IHitbox>();
        public List<IHitbox> Hits
        {
            get
            {
                return hits;
            }
            set
            {
                hits = value;
            }
        }

        /// <summary>
        /// GET: SET: a bool to tell whether the mind is currently colliding
        /// </summary>
        public bool isColliding
        {
            get;
            set;
        }

        /// <summary>
        /// GET: SET: The position of the mind
        /// </summary>
        protected Vector2 _pos = new Vector2();
        public Vector2 Position
        {
            get
            {
                return _pos;
            }
            set
            {
                _pos = value;
            }
        }

        private int health;
        int baseHealth;
        public int Health
        {
            get
            {
                return health;
            }
            set
            {
                health = value;
            }
        }

        private int dmg;

        public int Dmg
        {
            get
            {
                return dmg;
            }
            set
            {
                dmg = value;
            }
        }

        private float maxSpeed;

        public float MaxSpeed
        {
            get
            {
                return maxSpeed;
            }

            set
            {
                maxSpeed = value;
            }
        }

        /// <summary>
        /// DECLARE: The Texture path
        /// </summary>
        protected string texPath = "";

        /// <summary>
        /// GET: SET: The Axis aligned bounds for the mind and entity
        /// </summary>
        public Rectangle Bounds
        {
            get
            {
                if (e.Texture != null)
                    return new Rectangle((int)Position.X, (int)Position.Y, e.Texture.Width, e.Texture.Height);
                return new Rectangle((int)Position.X, (int)Position.Y, 32, 32);
            }
        }

        /// <summary>
        /// GET: SET: Whether the object should be included in collision calculations
        /// </summary>
        public bool isCollidable
        {
            get;
            set;
        }

        /// <summary>
        /// CONSTRUCTOR:
        /// </summary>
        public Mind()
        {

        }

        //// <summary>
        //// METHOD: Returns the entity this mind controls
        //// </summary>
        //// <returns></returns>
        public IEntity getEntity()
        {
            return e;
        }

        //// <summary>
        //// METHOD: Returns itself as an ICollidable (For collision management)
        //// </summary>
        //// <returns></returns>
        public ICollidable getCollidable()
        {
            return this;
        }

        /// <summary>
        /// METHOD: Initialise the basic properties of the Mind
        /// </summary>
        /// <param name="Position"></param>
        /// <param name="t"></param>
        public virtual void Initialize(Vector2 Position, string t)
        {
            ///SET: THe unique ID
            UniqueID = e.UniqueID;

            ///CALL: Set texture to set the texture
            setTexture(t);

            ///SET: The position of the entity and whether the entity is visible
            e.Position = Position;
            e.isVisible = true;

            ///SET: This mind to active
            Active = true;

        }

        /// <summary>
        /// METHOD: Initialises the basic properties of the mind when a texture has already been set
        /// </summary>
        /// <param name="Position"></param>
        public virtual void Initialize(Vector2 Position)
        {
            ///HANDLER: Add handler for SAT collision  
            Locator.Instance.getService<IDetectionManager>().OnSATCollision += OnSATCollision;
            baseHealth = Health;
            ///SET: THe unique ID for this mind
            UniqueID = e.UniqueID;

            ///IF: Texture path is not null
            if (texPath != null)
            {
                ///CALL: Set texture
                setTexture(texPath);
            }

            ///SET: position and visibility of Entity controlled by this mind.
            e.Position = Position;
            e.isVisible = true;

            ///SET: The position of this mind and whether it is active.
            _pos = Position;
            Active = true;

        }

        /// <summary>
        /// EVENT: When SAT Collision returns true.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void OnSATCollision(object sender, CollisionEventArgs e)
        {

            ///IF: This mind is object A, Apply Impulse to simulate physics
            if (e.A == this)
            {
                ApplyImpulse(-e.mtvRet);

            }
            ///ELSE IF: this mind is object B, apply impulse in opposite direction
            else if (e.B == this)
            {
                ApplyImpulse(e.mtvRet);

            }



        }

        /// <summary>
        /// METHOD: Responsible for unloading anything added to this mind and sending it to the garbage collector
        /// </summary>
        public virtual void Unload()
        {


        }

        /// <summary>
        /// METHOD: Set the texture of the entity this mind controls
        /// </summary>
        /// <param name="t"></param>
        public void setTexture(string t)
        {
            e.Texture = Locator.Instance.getService<IResourceLoader>().GetTex(t);
        }

        /// <summary>
        /// METHOD: Update is called every frame and controls the main behaviour of the Mind.
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void Update(GameTime gameTime)
        {
            ///SET: Position of controlled entity
            e.Position = _pos;

            ///IF: Mind is no longer active
            if (!Active)
            {
                ///SET: entity to not be drawn
                e.isVisible = false;
            }

            ///CALL: Method to simulate physical movement
            UpdatePhysics();

            if (Health <= 0)
            {
                Health = 0;
                Locator.Instance.getService<IEntityManager>().DestroyEnt(e, baseHealth);
            }
        }

        /// <summary>
        /// METHOD: Link the mind to an entity
        /// </summary>
        /// <param name="e"></param>
        public void Link(IEntity e)
        {
            this.e = e;
        }

        public void ApplyForce(Vector2 force)
        {
            ///F = ma
            ///Therefore a = F/m but we are using inverse mass because multiplication is quicker than division, thus m is set to be equal to 1/m so we can do a = F * m
            Acceleration += (force * Mass);
        }

        /// <summary>
        /// METHOD: Simulates movement obeying the laws of physics
        /// </summary>
        public void UpdatePhysics()
        {
            ///MULTIPLY: the velocity by the damping force
            Velocity *= Damping;
            ///ADD: The acceleration onto the velocity
            Velocity += Acceleration;

            velocity = Velocity;
            if (Velocity.Length() > MaxSpeed)
            {
                //Velocity /= MaxSpeed;
                //Velocity.Normalize();

                //velocity.X = 1;
            }
            ///ADD: The velocity onto the position
            _pos += Velocity;

            ///FOR: Each hitbox this mind controls
            for (int i = 0; i < Hits.Count; i++)
            {
                ///SET: the velocity of the hitbox to this velocity
                Hits[i].Velocity = Velocity;
            }
            ///FOR: Each hitbox this mind controls
            for (int i = 0; i < Hits.Count; i++)
            {
                ///SET: the points on each hitbox relative to the entity
                Hits[i].UpdatePoint(Velocity);
            }

            ///SET: acceleration to 0
            Acceleration = Vector2.Zero;
        }

        /// <summary>
        /// METHOD: Apply an impulse to an object once it has collided in order to simulate physics
        /// </summary>
        /// <param name="cVelocity"></param>
        public void ApplyImpulse(Vector2 cVelocity)
        {
            ///MULTIPLY: The parameter closing velocity by the restitution
            cVelocity *= Restitution;
            ///CALL: Apply force with the closing velocity
            ApplyForce(cVelocity);
        }

        public virtual void Talk(IMind e)
        {

        }
    }
}