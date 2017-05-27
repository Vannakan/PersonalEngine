using Engine.Entities;
using Engine.Interfaces.Entities;
using Engine34.Interfaces.Collision;
using Microsoft.Xna.Framework;
using System.Collections.Generic;


namespace Engine.Managers.Collision
{
    /// <summary>
    /// CLASS: Hitboxes that make up the boundaries for each entity to allow for SAT collision to work. Each hitbox
    /// covers a portion
    /// of the texture of an entity and is stored in a List of Hitboxes inside the entity class.
    /// </summary>
    public class Hitbox : IHitbox
    {
        /// DECLARE: A List of each corner and edge of the shape
        /// <remarks>points are not the same as the Monogame object Point. This was just the name given to them</remarks>
        protected List<Vector2> points;
        protected List<Vector2> edges;

        /// <summary>
        /// RETURN: A list of a Vector2 co-ordinates of the corners of the shape and set the local variable to this property.
        /// </summary>
        public List<Vector2> Points
        {
            get
            {
                return points;
            }
            set
            {
                points = value;
            }
        }

        /// <summary>
        /// RETURN: A list of a Vector2 co-ordinates of the edges of the shape and set the local variable to this property.
        /// </summary>
        public List<Vector2> Edges
        {
            get
            {
                return edges;
            }
            set
            {
                edges = value;
            }
        }

        /// <summary>
        /// RETURN: A Vector2 co-ordinate of the Velocity of the shape and set the local variable to this property.
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
        /// DECLARE: the local variables width height and rotation used for the creation of the shape
        /// </summary>
        private float width;
        private float height;
        private float rotation;

        ///DECLARE: A reference to the mind.
        protected IMind _mind;
        public IMind Mind
        {
            get
            {
                return _mind;
            }
            set
            {
                _mind = value;
            }
        }

        /// DECLARE: A vector2 co-ordinate for the centre of the shape.
        private Vector2 centre;
        public Vector2 Centre { get { return centre; } set { centre = value; } }

        /// <summary>
        /// CONSTRUCTOR: Takes 5 parameters used to create the List of points and edges and then a rotation used for 
        /// rotating the shape as necessary to allow for advanced SAT Collision
        /// </summary>
        /// <param name="_pos">The position of the first point</param>
        /// <param name="pWidth">The width of the shape</param>
        /// <param name="pHeight">The height of the shape</param>
        /// <param name="pRot">The rotation in degrees of the shape</param>
        /// <param name="parent">The mind that this hitox is linked to</param>
        public Hitbox(Vector2 _pos, float pWidth, float pHeight, float pRot, IMind parent)
        {
            ///SET: The dimensions of the shape
            width = pWidth;
            height = pHeight;
            rotation = pRot;

            ///INITIALISE: The lists of Vector2 used for the creation of the shape
            points = new List<Vector2>(4);
            edges = new List<Vector2>(4);

            ///ADD: 4 Vector2 co-ordinates to the list of points based on the dimensions of the shape and the position
            points.Add(_pos);
            points.Add(new Vector2(_pos.X + width, _pos.Y));
            points.Add(new Vector2(_pos.X + width, _pos.Y + height));
            points.Add(new Vector2(_pos.X, _pos.Y + height));

            ///INITIALISE: The centre point using the method centrePoint()
            Centre = centrePoint();

            ///ROTATE: Rotate the vector2 for the centre of the shape
            Centre = createRotation(centrePoint());

            ///ROTATE: Call the method to rotate each corner of the shape around the Centre of the shape
            createMatrix();

            ///CREATE: The edges of the shape by subtracting Corners from each other
            CreateEdges();

            ///SET: The mind that controls this hitbox, used for the movement of the Hitboxes.
            _mind = parent;
        }

        /// <summary>
        /// METHOD: A method used to rotate the 4 corners of the shape so that our SAT is not just limited to two axes.
        /// </summary>
        public void createMatrix()
        {
            ///FOR: Each point in the shape
            for (int i = 0; i < points.Count; i++)
            {
                ///CALL: The rotation method used for the Vector2s.
                points[i] = createRotation(points[i]);
            }
        }

        /// <summary>
        /// METHOD: Rotates a Vector2 around a centre point. The parameter is the point being rotated. In order to rotate
        /// the co-ordinates around the centre of the shape we must subtract the origin of the rotation from the co-ordinate
        /// and then add it back on, essentially "moving the object and moving it back".
        /// </summary>
        /// <param name="_point">The point to be rotated</param>
        /// <returns>Vector2 of the new location of the co-ordinate</returns>
        public Vector2 createRotation(Vector2 _point)
        {
            ///Move the point by subtracting the origin of rotation, in this case the centre, and use a Matrix to rotate 
            ///the Vector2 upon the Z axis by the rotation set in the constructor converted to radians.
            _point = Vector2.Transform(_point - Centre, Matrix.CreateRotationZ(MathHelper.ToRadians(rotation)));

            ///Move the point back to it's new location in relation to the origin
            _point += Centre;

            ///RETURN: the new location of the point.
            return _point;
        }

        /// <summary>
        /// METHOD: Creates edges of the shape by subtracting the previous point if one were to trace the outline of the shape
        /// </summary>
        public void CreateEdges()
        {
            ///DECLARE: Two Vector2 co-ordinates
            Vector2 point1;
            Vector2 point2;

            ///For each point the shape currently has
            for (int i = 0; i < points.Count; i++)
            {
                ///SET: the first point to be the point currently being iterated through in the list.
                point1 = points[i];

                ///IF: the next point would give an IndexOutOfRange Exception
                if (i + 1 == points.Count)
                {
                    ///SET: the next point to be equal to the first point in the list.
                    point2 = points[0];
                }
                else
                {
                    ///ELSE: The next point is equal to the next one in the list.
                    point2 = points[i + 1];
                }

                ///CREATE: an Edge by subtracting the second point from the first. This gives us a Vector2 of the translation
                ///Between two points.
                Edges.Add(point2 - point1);
            }
        }

        /// <summary>
        ///METHOD:  This is used to move the hitboxes with their parent entities. If this is not done then the collision will not work
        /// because the hitbox hasn't moved.
        /// </summary>
        /// <param name="velocity">The velocity to move the point by</param>
        public void UpdatePoint(Vector2 velocity)
        {
            ///FOR: each point in the shape.
            for (int i = 0; i < points.Count; i++)
            {
                ///Move the position of the point by the velocity provided in the parameters.
                points[i] += velocity;
            }
        }

        /// <summary>
        /// METHOD: The centre point of the shape. Used for the rotation and for calculating the Minimum Translation Vector
        /// When colliding with another Hitbox.
        /// </summary>
        /// <returns>A Vector2 of the Central co-ordinate between all of the points.</returns>
        public Vector2 centrePoint()
        {
            float midX = points[0].X + (width / 2); /// x coordinate for the first entity
            float midY = points[0].Y + (height / 2); /// y coordinate for the first entity

            Vector2 centre = new Vector2(midX, midY); /// making coordinates into a new vector for the first entity

            return centre;
        }

        public virtual void Update()
        {

        }

    }
}