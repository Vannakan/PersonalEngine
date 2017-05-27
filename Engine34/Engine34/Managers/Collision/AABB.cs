using System;
using Microsoft.Xna.Framework;

namespace Engine.Managers.Collision
{
    /// <summary>
    /// CLASS: A collision class used to calculate whether two axis aligned bounding boxes are intersecting or not.
    /// If they are intersecting this class can return true and state which side is intersecting and by how much.
    /// </summary>
    public enum CollisionSide
    {
        left,
        right,
        top,
        bottom,
        none
    }
    public static class AABB
    {
        /// <summary>
        /// A method to check whether two Axis Aligned Rectangles are itnersecting or not. If they are, this method
        /// will return true.
        /// </summary>
        /// <param name="a">Object A to be tested</param>
        /// <param name="b">Object B to be tested</param>
        /// <returns>bool</returns>
        public static bool Collision(Rectangle a, Rectangle b)
        {
            return !(b.Left > a.Right ||
             b.Right < a.Left ||
             b.Top > a.Bottom ||
             b.Bottom < a.Top);
        }

        /// <summary>
        /// This method finds out which side of the rectangles are intersecting
        /// </summary>
        /// <param name="a">Object A to be tested</param>
        /// <param name="b">Object B to be tested</param>
        /// <returns>enum CollisionSide</returns>
        public static CollisionSide getLeftOrRight(Rectangle a, Rectangle b)
        {
            if (b.Left < a.Right)
                return CollisionSide.left;
            if (b.Right > a.Left)
                return CollisionSide.right;


            else return CollisionSide.none;
        }

        /// <summary>
        /// This method finds out whether the top or the bottom of the rectangle is colliding.
        /// </summary>
        /// <param name="a">Object A to be tested</param>
        /// <param name="b">Object B to be tested</param>
        /// <returns>enum CollisionSide</returns>
        public static CollisionSide getUpOrDown(Rectangle a, Rectangle b)
        {
            if (b.Top < a.Bottom)
                return CollisionSide.top;
            if (b.Bottom > a.Top)
                return CollisionSide.bottom;

            else
                return CollisionSide.none;
        }

      
    }
}