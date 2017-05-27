using Engine.Interfaces.Collision;
using Engine.Interfaces.Entities;
using Engine.Managers.Collision;
using Engine34.Interfaces.Collision;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Grid
{
    public class Node
    {

        private Node parent = null;
        private bool blocked = false;
        private Vector2 position = new Vector2(0, 0);
        private Texture2D tex;
        private int element;
        private Color color = Color.White;
        private int g;
        ///
        private int h;

        private int x;
        private int y;

        public Vector2 getGrid
        {
            get
            {
                return new Vector2(x, y);
            }
        }

        public Node Parent
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
        public bool Blocked
        {
            get
            {
                return blocked;
            }
            set
            {
                blocked = value;
            }
        }
        public Vector2 Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
            }
        }
        public int Element
        {
            get
            {
                return element;
            }
            set
            {
                element = value;
            }
        }
        public Texture2D Tex
        {
            get
            {
                return tex;
            }
            set
            {
                tex = value;
            }
        }

        public Rectangle Bounds
        {
            get
            {
                return new Rectangle(y * Tex.Width, x* Tex.Height, Tex.Width, Tex.Height);
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

        public bool isColliding
        {
            get;
            set;
        }

        public int G
        {
            get
            {
                return g;
            }
            set
            {
                g = value;
            }
        }
        public int H
        {
            get
            {
                return h;
            }
            set
            {
                h = value;
            }
        }
        public int F
        {
            get
            {
                return g + h;
            }
        }

        public bool isCollidable
        {
            get;
            set;
        }

        public Color Color
        {
            set
            {
                color = value;
            }
            get
            {
                return color;
            }
        }


        public Node(int el, Texture2D t)
        {
            element = el;
            tex = t;
            
        }

        public Node(Texture2D t)
        {

            tex = t;
        }

        public Node(int x, int y, int el, Texture2D t)
        {
            this.x = x;
            this.y = y;
            element = el;
            tex = t;
        }


        public Node(bool blocked, Vector2 pos)
        {

        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(tex, position, color);
        }

        public IEntity getEntity()
        {
            return null;
        }

        //// <summary>
        //// METHOD: Returns itself as an ICollidable (For collision management)
        //// </summary>
        //// <returns></returns>
        //public ICollidable getCollidable()
        //{
        //    return this;
        //}

        public void ApplyImpulse(Vector2 cVelocity)
        {
        }

    }
}