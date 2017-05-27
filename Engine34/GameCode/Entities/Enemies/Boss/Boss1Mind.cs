using Engine.Entities;
using Engine.Interfaces.Collision;
using Engine.Interfaces.Entities;
using Engine.Interfaces.Resource;
using Engine.Interfaces.Sound;
using Engine.Managers.Collision;
using Engine.Managers.ServiceLocator;
using GameCode.Entities.Bullets;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameCode.Entities.Enemies.Boss
{
    class Boss1Mind : Mind
    {
        int bulletTimer;
        int dashTimer;
        private IEntity target;
        IEntity bul;
        private bool dashing;
        private bool hasDash;
        int waitTime;
        public override void Initialize(Vector2 Position)
        {
            ///SET: The texture of the entity this mind controls.
            texPath = "KingCholera";
            Team = 2;
            ///SET: The basic physics properties
            Mass = 0.3f;
            Restitution = 1f;
            Damping = 0.99f;
            Health = 1000;
            Dmg = 1;
            MaxSpeed = 100f;
            ///ADD: Hitboxes for the entity
            Hits.Add(new Hitbox(new Vector2(Position.X , Position.Y ), 50, 100, 0, this));
           
            dashTimer = 0;
            target = Locator.Instance.getService<IEntityManager>().getEntity("Player");
            dashing = false;

            ///CALL: The initialise of the abstract mind class
            base.Initialize(Position);
        }

        /// <summary>
        /// METHOD: The update method which is called and ran through every frame.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            ///CALL: The update physics method to simulate movement
            dashTimer++;

            if (dashTimer >= 250 && dashing == false && hasDash == false)
            {
                prepareDash();
                dashTimer = 0;
            }

            if (dashTimer >= 50 && dashing == true && hasDash == false)
            {
                waitTime++;
                Velocity = Vector2.Zero;
                if (waitTime > 50)
                {
                    doDash();
                    dashTimer = 0;
                    waitTime = 0;
                }
            }

            if (dashTimer >= 250 && dashing == true && hasDash == true)
            {
                dashing = false;
                hasDash = false;
            }

            if (bulletTimer >= 100)
            {
                ShootBullet();
            }

            else
            {
                bulletTimer++;
            }
            
            ///CALL: The update of the abstract class mind.
            base.Update(gameTime);
        }

        public override void Unload()
        {
            int offset = 100;
            for (int i = 0; i < 3; i++)
            {
                GameTools.GetItemManager.AddItem(new Vector2(Position.X + offset,Position.Y));
                offset += 100;
            }
            base.Unload();
        }

        public void prepareDash()
        {
            Velocity = Vector2.Zero;
            Vector2 direction = target.Position - Position;
            direction.Normalize();
            ApplyForce(-direction * (MaxSpeed / 5));
            dashing = true;
        }

        public void doDash()
        {
            Locator.Instance.getService<ISoundManager>().PlayEffect("swish");

            Velocity = Vector2.Zero;
            Velocity = Vector2.Zero;
            Vector2 direction = target.Position - Position;
            direction.Normalize();
            ApplyForce(direction * MaxSpeed);
            hasDash = true;
        }

        public void ShootBullet()
        {
            if (bulletTimer >= 10)
            {
                Locator.Instance.getService<ISoundManager>().PlayEffect("playerShoot");

                Vector2 direction = target.Position - Position;
                direction.Normalize();
                bul = Locator.Instance.getService<IEntityManager>().createEntityDrawable<Bullet>(Position);
                //Locator.Instance.getService<IDetectionManager>().addCollidable((ICollidable)bul.Mind);
                bul.Mind.Parent = this;
                //bul.Texture = Locator.Instance.getService<IResourceLoader>().GetTex("virus1");
                bul.Mind.Team = 3;
                bul.Mind.ApplyForce(direction * (MaxSpeed / 2));
                bulletTimer = 0;
            }
        }

    }
}
