using Engine.Interfaces.Collision;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Engine.Interfaces.Entities;
using Engine34.Interfaces.Collision;
using Engine.Managers.Collision;
using Engine.Utility;

namespace Engine34.Grid
{
    public class CollisionNode : ICollidable
    {
        public CollisionNode(Rectangle B)
        {
            Bounds = B;
        }
        public Rectangle Bounds { get;  set; }

        public List<IHitbox> Hits
        {
            get
            {
                return new List<IHitbox>() { new Hitbox(new Vector2(Bounds.X, Bounds.Y), Bounds.Width, Bounds.Height, 0, null) };
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public bool isCollidable
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public bool isColliding
        {
            get;
            set;
        }

        public Vector2 Position
        {

            get;set;
        }

        public void ApplyImpulse(Vector2 cVelocity)
        {
            Console.WriteLine("APPLY IMPULSE FROM " + this + " CALLED");
        }

        public ICollidable getCollidable()
        {
            throw new NotImplementedException();
        }

        public IEntity getEntity()
        {
            throw new NotImplementedException();
        }
    }
}
