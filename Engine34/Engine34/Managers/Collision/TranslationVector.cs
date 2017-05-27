using Engine.Interfaces.Collision;
using Microsoft.Xna.Framework;
using System;

namespace Engine.Managers.Collision
{
    /// <summary>
    /// CLASS: The vector that an object will need to be moved along once it is colliding in order to no longer be colliding.
    /// This vector needs to be the smallest possible amount an object can be moved by.
    /// </summary>
    public static class TranslationVector
    {
        /// <summary>
        /// METHOD: Calculate the minimum translation required for tan object to no longer be colliding with another
        /// </summary>
        /// <param name="A1">The first object to be tested on</param>
        /// <param name="B1">The second object to be tested on</param>
        /// <returns>Vector2</returns>
        public static Vector2 GetMinimumTranslation(Rectangle A1, Rectangle B1)
        {
            ///DECLARE: A new Vector that will be returned
            Vector2 mtd = new Vector2();

            ///DECLARE: An axis aligned Rectangle equal to the bounds of each object.
            Rectangle A = A1;
            Rectangle B = B1;

            ///DECLARE: The minimum and maximum values of the Rectangle on the X and Y axis.
            float xAMin = A.X;
            float xAMax = A.X + A.Width;
            float yAMin = A.Y;
            float yAMax = A.Y + A.Height;

            float xBMin = B.X;
            float xBMax = B.X + B.Width;
            float yBMin = B.Y;
            float yBMax = B.Y + B.Height;

            ///SET: 4 floats equal to the two Rectangles subtracted from each other on each axis to test for collisions.
            float left = (xBMin - xAMax);
            float right = (xBMax - xAMin);
            float top = (yBMin - yAMax);
            float bottom = (yBMax - yAMin);



            ///IF: An object should be moved to the left
            if (Math.Abs(left) < right)
            {
                mtd.X = left;
            }
            ///ELSE: Move it right
            else
            {
                mtd.X = right;
            }

            ///IF: An object should be moved upwards
            if (Math.Abs(top) < bottom)
            {
                mtd.Y = top;
            }
            ///ELSE: Move it down
            else
                mtd.Y = bottom;

            ///IF: a smaller movement to prevent collision can be made on the X axis
            if (Math.Abs(mtd.X) < Math.Abs(mtd.Y))
            {
                mtd.Y = 0;
            }
            ///ELSE: Move on the Y axis
            else
            {
                mtd.X = 0;

            }
            Console.WriteLine(mtd);
            ///RETURN: the minimum Vector
            return mtd;
        }
    }
}