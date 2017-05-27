using Engine.Events.CollisionEvent;
using Engine.Interfaces.Collision;
using Engine34.Interfaces.Collision;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using Engine.Grid;
using Engine34.Grid;
using Engine.Utility;
using Engine.Managers.ServiceLocator;
using Engine.Interfaces.Sound;

namespace Engine.Managers.Collision
{
    
    /// <summary>
    /// CLASS: The Detection Manager is used for all collision calculations. Once two objects are colliding a Collision event is
    /// fired with the necessary information for two objects to react properly.
    /// </summary>
    public class DetectionManager : IDetectionManager
    {

        private bool hasBeenSet = false;
        /// <summary>
        /// EVENT: a reference to the CollisionEventHandler which will be used when two objects collide
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void CollisionEventHandler(object sender, CollisionEventArgs e);

        /// <summary>
        /// DECLARE: The event that will be fired once two objects collide
        /// </summary>
        event CollisionHandler.CollisionEventHandler Collision;

        /// <summary>
        /// DECLARE: The event that will be fired once two objects are calculated to collide or about to collide using
        /// the SAT method
        /// </summary>
        event CollisionHandler.CollisionEventHandler SATCollision;

        ///INITIALISE: The object used for SAT Calculations
        SATcheck satTest = new SATcheck();

        ///INITIALISE: a List of collidable entities
        private List<ICollidable> collision = new List<ICollidable>();
        //A reference to any collision nodes on the map (Map terrain bounds)
        private List<CollisionNode> collisionLayer = new List<CollisionNode>();

        object objectLock = new object();
        int destroyedBul;

        public event CollisionHandler.CollisionEventHandler OnSATCollision
        {
            add
            {
                lock (objectLock)
                    SATCollision += value;
            }
            remove
            {
                lock (objectLock)

                    SATCollision -= value;

            }
        }

        public event CollisionHandler.CollisionEventHandler OnCollision
        {
            add
            {
                lock (objectLock)

                    Collision += value;
            }
            remove
            {
                lock (objectLock)

                    Collision -= value;
            }
        }


        //// <summary>
        //// METHOD: Add an ICollidable interface to the collision list ready to check for collisions
        //// </summary>
        //// <param name="obj">the object to be added to the list</param>
        public void addCollidable(ICollidable obj)
        {
            Console.WriteLine("ADDED");
            collision.Add(obj);
        }

        public void addMapCollidable(CollisionNode obj)
        {
            collisionLayer.Add(obj);
        }

        public void setCollisionLayer(List<CollisionNode> value)
        {
            collisionLayer = value;
        }

        /// <summary>
        /// METHOD: Called every frame of the game loop and contains all of the necessary code for the manager to calculate
        /// Whether each object is colliding or not
        /// </summary>
        /// <param name="gameTime">Monogame GameTime</param>
        public void Update(GameTime gameTime)
        {
            checkEntityAgainstMap();
            ///FOR: each ICollidable in the collision list
            for (int i = 0; i < collision.Count; i++)
            {
                ///FOR: Each ICollidable in the collision list
                for (int x = 1; x < collision.Count; x++)
                {
                    ///IF: The two objects are not the same thing
                    if (x < collision.Count && i < collision.Count)
                    {
                        if (collision[x] != collision[i])
                        {
                            if (Vector2.Distance(collision[x].Position,collision[i].Position) < 100)
                            ///Run the SAT Calculation between the two objects.
                             CheckSAT(collision[i], collision[x]);
                        }
                    }
                }
            }

        

            
        }

        public void checkEntityAgainstMap()
        {
            bool yaysaid = false;
            for (int j = 0; j < collisionLayer.Count; j++)
            {
                for (int i = 0; i < collision.Count; i++)
                {
                    if (AABB.Collision(collision[i].Bounds, collisionLayer[j].Bounds))
                    {
                        if (yaysaid == false)
                        {
                            CheckSAT(collision[i], collisionLayer[j]);
                        }
                    }
                }
            }
        }
        
        /// <summary>
        /// TESTMETHOD
        /// </summary>
        public void entityTerrainCollisions()
        {
            foreach(CollisionNode n in collisionLayer)
            {
                for(int i = 0; i < collision.Count; i++)
                {
                    if(AABB.Collision(collision[i].Bounds, n.Bounds))
                    {
                        //OnACollision
                    }
                }
            }
        }
        //// <summary>
        //// METHOD: Collision between dynamic entities.
        //// </summary>
        public void doCollision()
        {
            if (collision.Count >= 2)
            {
                ///ITERATE: through the list twice, counting one object above the first each time
                for (int i = 0; i < collision.Count; i++)
                {
                    for (int k = i + 1; k < collision.Count; k++)
                    {
                        ///IF: they're not equal objects
                        if (collision[i] != collision[k])
                        {
                            var A = collision[i];
                            var B = collision[k];
                                ///SET: the distance between the two objects
                                Vector2 distance = B.Position - A.Position;
                                if (distance.Length() > 50)
                                    continue;
                                else
                           ///IF: there's a collision, but also checks if A is a controller character, if so then it will move A around, otherwise tiles may have problems
                           if (AABB.Collision(A.Bounds, B.Bounds))
                                {
                                    ///SET: Minimum Translation Vector
                                    Vector2 mtd = TranslationVector.GetMinimumTranslation(A.Bounds, B.Bounds);
                                    ///ADD minimum translation Vector to object.
                                    if (A.GetType() != typeof(Node))
                                    A.Position += mtd;
                                }
                        }
                    }
                }
            }
        }

        //// <summary>
        //// METHOD: Tells any entities that care about colliding with other entities that they have collided, and what entity they have collided with.
        //// </summary>
        //// <param name="A"></param>
        //// <param name="B"></param>
        public void OnACollision(ICollidable A, ICollidable B)
        {
            ///EVENT: Collision event with each shape.
            Collision(this, new CollisionEventArgs()
            {
                A = A,
                B = B
            });
        }

        /// <summary>
        /// METHOD: Call to fire an Event when SAT Calculates two shapes about to collide.
        /// </summary>
        /// <param name="a">Object A to be tested</param>
        /// <param name="b">Object B to be tested</param>
        /// <param name="mtv"></param>
        public void CallSAT(ICollidable A, ICollidable B, Vector2 mtv)
        {
            SATCollision(this, new CollisionEventArgs()
            {
                A = A,
                B = B,
                mtvRet = mtv
            });
        }

        public void resetCollisionLayer()
        {
            collisionLayer.Clear();
        }

        /// <summary>
        /// METHOD: Runs the SAT Calculations on two ICollidable objects and calls the event fire method when true.
        /// </summary>
        /// <param name="a">Object A to be tested</param>
        /// <param name="b">Object B to be tested</param>
        public void CheckSAT(ICollidable a, ICollidable b)
        {
            if (a.Hits.Count > 0 && b.Hits.Count > 0)
            {
                    if (a != b)
                    {
                        if (b != a)
                        {                    
                                ///FOR: each hitbox on the first object
                                for (int i = 0; i < a.Hits.Count; i++)
                                {
                                    ///FOR: Each hitbox on the second object
                                    for (int x = 0; x < b.Hits.Count; x++)
                                    {
                                        ///IF: the SAT Calculation returns true
                                        if (satTest.SATCollision(a.Hits[i], b.Hits[x], a.Hits[i].Velocity, b.Hits[x].Velocity))
                                        {

                                            ///CALL: CallSAT method which fires an Event and moves the objects out of each other
                                            CallSAT(a, b, -satTest.mtvRet());
                                            ///CALL: CallSAT Method which fires an Event and applies an impule to each shape to simulate physics
                                            CallSAT(a, b, ImpulseApplication(a.Hits[i], b.Hits[x]));
                                        }
                                    
                                }
                            }
                        }
                    }
                }
            }
        

        /// <summary>
        /// METHOD: Applies the impulse to the two objects to simulate physics.
        /// </summary>
        /// <param name="a">Object A to be tested</param>
        /// <param name="b">Object B to be tested</param>
        /// <returns>A Vector2 which will be applied to the object</returns>
        public Vector2 ImpulseApplication(IHitbox a, IHitbox b)
        {
            ///DECLARE: The Combined velocity of the two objects
            Vector2 combVel = a.Velocity - b.Velocity;

            ///DECLARE: The closing normal which is the combined velocities proportionally adjusted so the Vector has a length
            ///of 1
            Vector2 cNormal = Vector2.Normalize(combVel);

            ///DECLARE: THe dot product of the combined velocity and the Collision Normal to get a Closing Velocity
            float cVelocity = Vector2.Dot(combVel, cNormal);

            ///MULTIPLY: The collision normal by the closing velocity
            cNormal *= cVelocity;

            ///RETURN: the collision normal.
            return cNormal;
        }

        public void RemoveCollision(ICollidable hit)
        {
            if (collision.IndexOf(hit) != -1)
                collision.Remove(hit);
        }

        public void ClearCollisionList()
        {
            collision.Clear();
        }

    }
}