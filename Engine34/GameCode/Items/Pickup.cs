using Engine.Events.CollisionEvent;
using Engine34.Utilities.Shapes;
using GameCode.Entities.Platformer;
using GameCode.Items.mStats;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameCode.Items
{
   public  class Pickup : IPickup
    {

        string Name;
        string Texture;
        protected Texture2D Tex;
        private Circle Radius;
        protected Vector2 Pos;

        public Pickup()
        {

        }

        public virtual void Initialize(string name, Vector2 pos, int radius)
        {
            Name = name;
            Pos = pos;
            Radius = new Circle(radius, Pos);
        }



        public virtual void PickUp(IStats stats)
        {

        }

        public virtual void Action()
        {

        }

      

        public virtual void Draw(SpriteBatch spr)
        {
            if (Tex != null)
                spr.Draw(Tex, Pos, Color.White);
        }

    }
}
