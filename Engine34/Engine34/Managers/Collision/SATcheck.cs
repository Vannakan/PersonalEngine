using Engine34.Interfaces.Collision;
using Microsoft.Xna.Framework;
using System;

namespace Engine.Managers.Collision
{
    /// <summary>
    /// CLASS: This class runs all of the Separating Axis Theorem collision code. It calculates whether two shapes are currently
    /// intersecting or going to intersect by projecting shadows of both shapes onto an arbitrary axis perpendicular to the
    /// edges of the shape.
    /// </summary>
    class SATcheck
    {
        public Vector2 MinimumTranslationVector;

        /// <summary>
        /// METHOD: The calculation for if two objects are colliding or going to collide
        /// </summary>
        /// <param name="shape1">The first object in the calculation</param>
        /// <param name="shape2">The second object in the calculation</param>
        /// <param name="velocity">The velocity of the first object</param>
        /// <param name="velocity2">The velocity of the second object</param>
        /// <returns>A bool of whether the shapes are about to collide or are already colliding</returns>
        public bool SATCollision(IHitbox shape1, IHitbox shape2, Vector2 velocity, Vector2 velocity2)
        {
            bool Intersect = true; ///assume the shape is intersecting until calculated
            bool WillIntersect = true; ///Assume the shape will intersect until calculated

            int edgelist1 = shape1.Edges.Count; ///CREATE: an integer of how many edges are in shape 1
            int edgelist2 = shape2.Edges.Count; ///CREATE: an integer of how many edges are in shape 2
            float _mtv = float.PositiveInfinity; ///SET: Minimum translation vector is set to infinity
            Vector2 trans_axis = new Vector2(); ///INITIALISE: The axis that we will push the shape along if its colliding
            Vector2 edge; ///DECLARE: The current edge we are checking

            /// FOR: all the edges of both polygons
            for (int e = 0; e < edgelist1 + edgelist2; e++)
            {
                if (e < edgelist1)
                {
                    edge = shape1.Edges[e]; ///IF e is less than the first integer, we are checking each edge of the first shape
                }
                else
                {
                    edge = shape2.Edges[e - edgelist1]; ///Same for shape 2 
                }

                /// SET: the axis perpendicular to the current edge to project on 
                Vector2 axis = new Vector2(-edge.Y, edge.X);
                axis.Normalize(); /// Keeping the vector pointing the same direction, adjust it so it's length is 1

                ///SET: the projection of the polygon on the current axis
                float min1 = 0;
                float min2 = 0;
                float max1 = 0;
                float max2 = 0; ///These are initialised to 0 and adjusted in the method below
                Projection(axis, shape1, ref min1, ref max1); ///Project polygon onto axis
                Projection(axis, shape2, ref min2, ref max2); ///Project polygon 2 onto axis

                ///IF: the polygon projections are currently NOT intersecting
                if (IntervalDistance(min1, max1, min2, max2) > 0)
                {
                    Intersect = false; ///This if statement will only run if the first condition of the method is met, therefore we know they aren't intersecting
                }

                ///SET: the velocity projection on the current axis by using the dot product.
                float _vel_proj = Vector2.Dot(axis, velocity) + 0.1f;

                ///IF: the Projection of polygon A is less than 0
                if (_vel_proj < 0)
                {
                    ///SET: The Min variable accordingly.
                    min1 += _vel_proj;
                }
                else
                {
                    ///ELSE SET: the max variable accordingly
                    max1 += _vel_proj;
                }

                /// Do the same test as above for the new projection
                float dist = IntervalDistance(min1, max1, min2, max2);
                if (dist > 0)
                {
                    WillIntersect = false; ///Same as above but this time using a hypothetical movement
                }
                ///IF: the polygons are not intersecting and won't intersect, exit the loop
                if (!Intersect && !WillIntersect)
                {
                    MinimumTranslationVector = Vector2.Zero;
                    return false;
                }
                /// Check if the current interval distance is the minimum one. If so store
                /// the interval distance and the current distance.
                /// This will be used to calculate the minimum translation vector
                dist = Math.Abs(dist); ///Find how far from 0 the distance is
                if (dist < _mtv) ///If the minimum translation vector is not the smallest possible movement
                {
                    _mtv = dist; ///Set the vector to the smallest distance!
                    trans_axis = axis; ///We need to move the object on the axis we projected

                    Vector2 d = shape1.Centre - shape2.Centre; ///We need to find which object is on the left hand side of the other
                    if (d.Length() < 0)
                    {
                        trans_axis *= -1;
                    }
                }
            }

            /// The minimum translation vector
            /// can be used to push the polygons apart.
            if (WillIntersect) ///If the objects are colliding
            {
                MinimumTranslationVector = trans_axis * _mtv; ///Set the property to be the translation axis multiplied by the mtv
                return true;
            }

            return false;
        }

        /// <summary>
        /// METHOD: Projects an Object onto an arbitrary axis
        /// </summary>
        /// <param name="axis">The axis to be projected on to</param>
        /// <param name="myEnt">The object to be projected</param>
        /// <param name="min">The minimum value of the object on the axis</param>
        /// <param name="max">The maximum value of the object on the axis</param>
        public void Projection(Vector2 axis, IHitbox myEnt, ref float min, ref float max)
        {
            /// To project a point on an axis use the dot product
            float d = Vector2.Dot(axis, myEnt.Points[0]);
            min = d;
            max = d;
            for (int i = 0; i < myEnt.Points.Count; i++)
            {
                ///Find the minimum point along the axis that the shape can be found.
                ///Set the min and max accordingly
                d = Vector2.Dot(myEnt.Points[i], axis);
                if (d < min)
                {
                    min = d;
                }
                else
                {
                    if (d > max)
                    {
                        max = d;
                    }
                }
            }
        }

        /// <summary>
        /// METHOD: Finds the distance between two values
        /// </summary>
        /// <param name="minA">the minimum value of the first object on an axis</param>
        /// <param name="maxA">The maximum value of the first object on an axis</param>
        /// <param name="minB">The minimum value of the second object on an axis</param>
        /// <param name="maxB">The maximum value of the second object on an axis</param>
        /// <returns>A float of the distance between two values</returns>
        public float IntervalDistance(float minA, float maxA, float minB, float maxB)
        {
            ///whichever is lower on the axis
            if (minA < minB)
            {
                ///Return the one on the right minus the other
                return minB - maxA;
            }
            else
            {
                ///Return the one on the right minus the other
                return minA - maxB;
            }

        }

        /// <summary>
        /// METHOD: returns the minimum distance needed to move an object to prevent them from being in a collision
        /// </summary>
        /// <returns>A vector2 Of the minimum translation vector</returns>
        public Vector2 mtvRet()
        {
            ///Return the minimum distance required to prevent shapes from intersecting
            return MinimumTranslationVector;
        }
    }
}