using Engine.Entities;
using Engine.Events.CollisionEvent;
using Engine.Interfaces.Collision;
using Engine.Interfaces.Entities;
using Engine.Interfaces.Render;
using Engine.Interfaces.Resource;
using Engine.Managers.Collision;
using Engine.Managers.Render;
using Engine.Managers.ServiceLocator;
using Engine34.Managers.Render;
using GameCode.Entities.Enemies.Boss;
using GameCode.Entities.Platformer;
using GameCode.Entities.Steering;
using GameCode.Entities.Tiles;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameCode.Entities.Bullets
{
    class BulletMind : Mind
    {
        public ICollidable parent;
        float Dist;
        LightMask x;
        Vector2 offset = new Vector2(-30, -30);

        public BulletMind()
        {
 
            
        }

        public override void Initialize(Vector2 Position)
        {
            ///SET: the Texture of the entity this controls.
            texPath = "bullet1";

            ///SET: The physics properties of this object.
            Mass = 0.9f;
            Restitution = 1f;
            Damping = 0.98f;
            Health = 1;
            Dmg = 1;
            MaxSpeed = 1.5f;

            Hits.Add(new Hitbox(new Vector2(Position.X, Position.Y), 16, 16, 0, this));
            ///CALL: The initialise of the Abstract Mind class.
            ///
            x = new LightMask(Position + offset, Locator.Instance.getService<IResourceLoader>().GetTex("Grad"),0.5f);
            Locator.Instance.getService<ILightMaskManager>().addMask(x);
            base.Initialize(Position);
        }

        public void setBulletTexture(string tex)
        {
            setTexture(tex);
        }

        /// <summary>
        /// METHOD: The update method which is called and ran through every frame.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            Dist += Velocity.Length();
            x.Position = Position + offset;


            ///CALL: The update of the abstract class mind.
            base.Update(gameTime);
            if (Dist >= 250)
            {
                Locator.Instance.getService<IEntityManager>().DestroyEnt(e, 0);
            }
        }

        public override void Unload()
        {
            

                Locator.Instance.getService<IDetectionManager>().OnSATCollision -= OnSATCollision;
            Locator.Instance.getService<IDetectionManager>().RemoveCollision(this);
            Locator.Instance.getService<ILightMaskManager>().RemoveLightSource(x);

        }

        public override void OnSATCollision(object sender, CollisionEventArgs ev)
        {

            if (ev.A.GetType() == typeof(SteeringMind) && ev.B == this || ev.A.GetType() == typeof(Boss1) && ev.B == this  )
            {
                Dmg = 0;
                Dist = 251;
            }
            else if (ev.B.GetType() == typeof(SteeringMind) && ev.A == this || ev.B.GetType() == typeof(Boss1) && ev.A == this )
            {
                Dmg = 0;
                Dist = 251;
            }


             if(ev.A.GetType() != typeof(PlayerMind) && ev.B.GetType() != typeof(PlayerMind))
                base.OnSATCollision(sender, ev);

        }

    }
}
