using Engine.Interfaces.Collision;
using Microsoft.Xna.Framework;

namespace Engine.Events.CollisionEvent
{
    public class CollisionEventArgs
    {
        //entity
        public ICollidable A
        {
            get;
            set;
        }
        //Object in which the entity is colliding with
        public ICollidable B
        {
            get;
            set;
        }

        public Vector2 mtvRet
        {
            get;
            set;
        }

        //Used for Direction tiles
        // public Direction D { get; set; }
    }
}