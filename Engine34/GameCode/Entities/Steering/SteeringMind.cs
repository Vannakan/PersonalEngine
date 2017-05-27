using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Engine.Events.MouseEvent;
using Engine.Events.CollisionEvent;
using Engine.Interfaces.Entities;
using Engine.Managers.ServiceLocator;
using Engine.Managers.Cam;
using Engine.Managers.Entities;
using Engine.Grid;
using Engine.Interfaces.Cam;
using Engine.Events.KeyboardEvent;
using Engine.Entities;
using Engine;
using Engine.Managers.Collision;
using Engine.Interfaces.Collision;
using Engine.Interfaces.Sound;
using Engine34.Grid;
using GameCode.Entities.Bullets;
using Engine34.Managers.Render;
using Engine.Managers.Render;
using Engine.Interfaces.Resource;
using Engine.Interfaces.Render;

namespace GameCode.Entities.Steering
{
    public enum SteerState { Seek, Flee, Evade, Pursue, Arrival, None }
    class SteeringMind : Mind
    {

        //Steering behaviour weighting for the alignment
        private float alignmentWeight = 0.1f;
        //Steering behaviour weighting for the cohesion
        private float cohesionWeight = 0.1f;
        //steering behaviour weighting for the seperation
        private float seperationWeight = 0.1f;

        //Player Texture
        private Texture2D tex;
        private Vector2 direction;
        //private Vector2 velocity;
        private Vector2 DesiredVelocity;
        public Vector2 steeringForce;
        private Vector2 position;
        private SpriteBatch sb;
        // private Player target;
        private float rotationAngle;
        private Vector2 origin = Vector2.Zero;
        private float MaxVelocity = 2f;
        private float MaxForce = 0.5f;
        private float mass = 10f;
        private int counter = 0;
        int neighbourCount = 0;
        Vector2 _velocity;
        Vector2 vel;

        public IEntity current;

        Vector2 mousPos;

        public Queue<Node> waypoints = new Queue<Node>();
        Node currentWaypoint;

        SteerState currentState = SteerState.None;

        /// <summary>
        /// A list of all
        /// </summary>
        private static List<SteeringMind> neighbours = new List<SteeringMind>();
        private Vector2 wonderTarget;
        private bool wonderSet = false;
        private int wanderRange = 300;
        private bool flockset;
        private Vector2 flockGoal;

        LightMask x;
        Vector2 offset = new Vector2(-100, -100);

        public List<Node> Pathway
        {
            get;
            private set;
        }

        public override void Initialize(Vector2 Position)
        {
            isCollidable = true;
            Hits.Add(new Hitbox(new Vector2(Position.X, Position.Y + 3), 16, 6, 45, this));
            Hits.Add(new Hitbox(new Vector2(Position.X + 16, Position.Y + 3), 16, 6, -45, this));
            Hits.Add(new Hitbox(new Vector2(Position.X + 12, Position.Y + 16), 4, 16, 0, this));

            texPath = "Cholera";
            //Allow other steering entities to acknowledge this entity
            neighbours.Add(this);
            Locator.Instance.getService<MouseHandler>().MouseMoved += OnMouseMoved;
            Locator.Instance.getService<KeyHandler>().KeyDown += OnKeyDown;

            current = Locator.Instance.getService<IEntityManager>().getCamEntity("Player");
            position = Position;

            Mass = 0.1f;
            Restitution = 0.00001f;
            Damping = 0.97f;


            Health = 235;
            Dmg = 1;
            MaxSpeed = 2f;
            this.currentState = SteerState.None;
            base.Initialize(Position);


            x = new LightMask(Position + offset, Locator.Instance.getService<IResourceLoader>().GetTex("Grad"), 1f);
            Locator.Instance.getService<ILightMaskManager>().addMask(x);
        }

        public void Wander()
        {

            int ran = Constants.r.Next(50, 130);

            if (!wonderSet)
            {

                wonderTarget = new Vector2(Constants.r.Next(0, wanderRange), Constants.r.Next(0, wanderRange));
                wonderSet = true;
            }
            counter++;

            if (counter >= ran)
            {
                wonderTarget = new Vector2(Constants.r.Next(0, wanderRange), Constants.r.Next(0, wanderRange));
                counter = 0;
            }
            Seek(wonderTarget);

        }

        public void flock()
        {
            Random random = new Random();
            int ran = 1000;

            if (!flockset)
            {

                flockGoal = new Vector2(random.Next(0, wanderRange), random.Next(0, wanderRange));
                flockset = true;
            }
            counter++;

            if (counter >= ran)
            {
                flockGoal = new Vector2(random.Next(0, wanderRange), random.Next(0, wanderRange));
                counter = 0;
            }

            Seek(flockGoal);


        }

        public void setPath(List<Node> path)
        {
            Pathway = path;
        }

        public void followWayPoints()
        {
            if (waypoints.Count > 0)
            {


                currentWaypoint = waypoints.Peek();

                for (int i = 0; i < waypoints.Count; i++)
                {
                    if (currentWaypoint != null)
                    {
                        Vector2 Distance = new Vector2(Math.Abs(currentWaypoint.Position.X - position.X), Math.Abs(currentWaypoint.Position.Y - position.Y));
                        if (Distance.X < 40 && Distance.Y < 40)
                        {
                            waypoints.Dequeue();
                            if (waypoints.Count > 1)
                                currentWaypoint = waypoints.Peek();

                        }
                        else
                            continue;
                    }
                }
            }
        }

        public override void Unload()
        {

            ///HANDLER: Add handler for SAT collision  
            Locator.Instance.getService<IDetectionManager>().OnSATCollision -= OnSATCollision;
            Locator.Instance.getService<ISoundManager>().PlayEffect("enemyDie");
            Locator.Instance.getService<ILightMaskManager>().RemoveLightSource(x);
        }

        public void OnMouseMoved(object sender, MouseEventArgs k)
        {
            mousPos = Locator.Instance.getService<ICameraManager>().getWorldPosition(new Vector2(k.X, k.Y));
        }

        public override void Update(GameTime gameTime)
        {
            x.Position = Position + offset;
            Vector2 after = Vector2.Zero;
            //velocity = Velocity;
            //position = Position;

            if (current != null)
            {
                if (Health > 150)
                {
                    Vector2 move = Pursue(current.Position, current.Velocity);
                    move.Normalize();
                    after += move;
                }
                else if (Health < 150)
                {

                    Vector2 move = Flee(current.Position);
                    move.Normalize();
                    after += move;

                }
            }

            if (current != null)
            {
                switch (currentState)
                {
                    case SteerState.Seek:
                        Vector2 move6 = Seek(current.Position);
                        move6.Normalize();
                        after += move6;
                        //ApplyForce(move);

                        //velocity.Normalize();

                        break;

                    case SteerState.Flee:
                        Vector2 move2 = Flee(current.Position);
                        move2.Normalize();
                        after += move2;
                        //ApplyForce(move2);

                        break;

                    case SteerState.Pursue:
                        Vector2 move3 = Pursue(current.Position, current.Velocity);
                        move3.Normalize();
                        after += move3;
                        //ApplyForce(move3);

                        break;

                    case SteerState.Evade:
                        Vector2 move4 = Evade(current.Position, current.Velocity);
                        move4.Normalize();
                        after += move4;
                        //ApplyForce(move4);

                        break;

                    case SteerState.Arrival:
                        Arrival(current.Position, 150f);
                        // velocity.Normalize();
                        break;



                }
            }

            //   followWayPoints();

            //    if (waypoints.Count > 0)
            //  {

            //    Vector2 move = Seek(waypoints.Peek().Position);
            //move.Normalize();
            //    after += move;

            // }
            //after += flockBehaviour(velocity);
            //after.Normalize();
            ApplyForce(after);
            //Velocity = velocity;
            //Position = position;
            base.Update(gameTime);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public Vector2 flockBehaviour(Vector2 v)
        {
            v += calcAlignment() * alignmentWeight + calcCohesion() * cohesionWeight + calcSeperation() * seperationWeight;
            return v;

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="Target"></param>
        public Vector2 Seek(Vector2 Target)
        {
            //Find the direction
            direction = (Target - Position);
            //Find the desired Velocity
            DesiredVelocity = direction * MaxVelocity;
            //calculate the force we want to apply 
            steeringForce = DesiredVelocity - Velocity;
            //Check that the steering force hasnt exceeded the max force
            steeringForce = CheckMax(steeringForce, MaxForce);
            //Apply mass
            //steeringForce = steeringForce *  (1/mass);
            //Adjust velocity with the steering force
            //velocity += steeringForce;

            //Apply velocity to pos
            //position += velocity;
            return steeringForce;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Target"></param>
        public Vector2 Flee(Vector2 Target)
        {
            //Find the direction
            direction = Vector2.Normalize(Position - Target);
            //Find the desired Velocity
            DesiredVelocity = direction * MaxVelocity;
            //calculate the force we want to apply 
            steeringForce = DesiredVelocity - Velocity;
            //Check that the steering force hasnt exceeded the max force
            steeringForce = CheckMax(steeringForce, MaxForce);
            //Apply mass
            //steeringForce = steeringForce * (1/mass);
            //Adjust velocity with the steering force
            //velocity += steeringForce;
            //Apply velocity to pos
            //position += velocity;
            return steeringForce;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Target"></param>
        /// <param name="TargetsVelocity"></param>
        public Vector2 Pursue(Vector2 Target, Vector2 TargetsVelocity)
        {
            //Find distance from player to missle
            Vector2 distance = Target - Position;
            //Time until missle hits target. Will be used to steer closer into the target when gap is close. The further away, we try to steer in front of the target
            float T = distance.Length() / MaxVelocity;
            Vector2 futurePosition = Target + TargetsVelocity * T;

            Vector2 steering = Seek(futurePosition);
            return steering;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Target"></param>
        /// <param name="TargetsVelocity"></param>
        public Vector2 Evade(Vector2 Target, Vector2 TargetsVelocity)
        {
            //Find distance from player to missle
            Vector2 distance = Position - Target;
            //Time until missle hits target. Will be used to steer closer into the target when gap is close. The further away, we try to steer in front of the target
            float T = distance.Length() / MaxVelocity;
            Vector2 futurePosition = Target + TargetsVelocity * T;

            Vector2 steering = Flee(futurePosition);
            return steering;
        }


        public Vector2 Arrival(Vector2 Target, float radius)
        {

            //Find distance from player to missle
            Vector2 distance = Target - Position;
            var T = distance.Length();
            if (T < radius)
            {
                Vector2 steer = Seek(Truncate(Target * T, MaxVelocity));
                return steer;

            }
            else
            {
                Vector2 steer = Seek(Target);
                return steer;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Vector2 calcSeperation()
        {

            foreach (SteeringMind m in neighbours)
            {
                if (m != this)
                {
                    if (Vector2.Distance(position, m.position) < 50)
                    {
                        velocity += m.getPosition() - position;
                        neighbourCount++;
                    }
                }
            }

            if (neighbourCount == 0)
            {
                return velocity;
            }

            velocity.X /= neighbourCount;
            velocity.Y /= neighbourCount;
            velocity.X *= -1;
            velocity.Y *= -1;
            velocity.Normalize();
            return velocity;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public Vector2 calcCohesion()
        {
            foreach (SteeringMind m in neighbours)
            {
                if (m != this)
                {
                    if (Vector2.Distance(position, m.position) < 50)
                    {
                        velocity += m.getPosition();
                        neighbourCount++;
                    }
                }
            }

            if (neighbourCount == 0)
                return velocity;

            velocity.X /= neighbourCount;
            velocity.Y /= neighbourCount;

            velocity = new Vector2(velocity.X - position.X, velocity.Y - position.Y);
            velocity.Normalize();
            return velocity;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public Vector2 calcAlignment()
        {
            foreach (SteeringMind m in neighbours)
            {
                if (m != this)
                {
                    if (Vector2.Distance(position, m.position) < 50)
                    {
                        velocity += m.getVelocity();
                        neighbourCount++;
                    }
                }
            }

            if (neighbourCount == 0)
                return velocity;

            velocity.X /= neighbourCount;
            velocity.Y /= neighbourCount;
            velocity.Normalize();
            return velocity;
        }

        public void setNeighbourhood(List<SteeringMind> list)
        {
            neighbours = list;
        }

        public Vector2 CheckMax(Vector2 v, float maxValue)
        {
            float x = maxValue / v.Length();

            //Check if X is loswer than the maximum value
            if (x < 1.0)
            {
                x = 1.0f;
            }


            v.X = v.X + x;
            v.Y = v.Y + x;
            return v;
        }

        public Vector2 getVelocity()
        {
            return velocity;
        }

        public Vector2 getPosition()
        {
            return position;
        }

        public Vector2 getNormalPos()
        {
            return Vector2.Normalize(position);
        }

        public Vector2 getNormalVel()
        {
            return Vector2.Normalize(velocity);

        }

        public Vector2 getNormalDesVel()
        {
            return Vector2.Normalize(DesiredVelocity);
        }

        public Vector2 getNormalSteering()
        {
            return Vector2.Normalize(steeringForce);
        }



        private Vector2 Truncate(Vector2 A, float scale)
        {

            float i = scale / A.Length();
            i = i < 1.0f ? i : 1.0f;

            Vector2 result = new Vector2(Matrix.CreateScale(i).Translation.X, Matrix.CreateScale(i).Translation.Y);
            return result;
        }

        public void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.key == Microsoft.Xna.Framework.Input.Keys.Z)
                currentState = SteerState.Seek;
            if (e.key == Microsoft.Xna.Framework.Input.Keys.X)
                currentState = SteerState.Flee;

            if (e.key == Microsoft.Xna.Framework.Input.Keys.C)
                currentState = SteerState.Pursue;

            if (e.key == Microsoft.Xna.Framework.Input.Keys.V)
                currentState = SteerState.Evade;

            if (e.key == Microsoft.Xna.Framework.Input.Keys.B)
                currentState = SteerState.Arrival;


        }

        public override void OnSATCollision(object sender, CollisionEventArgs e)
        {
            if (e.A == this && e.B.GetType() == typeof(CollisionNode))
            {
                ApplyForce(-e.mtvRet);
            }
            ///ELSE IF: this mind is object B, apply impulse in opposite direction
            else if (e.B == this && e.A.GetType() == typeof(CollisionNode))
            {
                ApplyForce(e.mtvRet);

            }

            //    base.OnSATCollision(sender, e);

        }


    }
}