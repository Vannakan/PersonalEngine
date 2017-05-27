using Engine.Interfaces.Collision;
using Microsoft.Xna.Framework;

namespace Engine.Events.CollisionEvent
{
    class SATEventArgs
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

    }
}