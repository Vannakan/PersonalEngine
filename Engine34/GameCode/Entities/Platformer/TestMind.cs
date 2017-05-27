using Engine.Entities;
using Engine.Interfaces.Entities;
using Engine.Interfaces.Render;
using Engine.Managers.Collision;
using Engine.Managers.ServiceLocator;
using Engine.Utilities;
using GameCode.Entities.Human;
using Microsoft.Xna.Framework;
using System;
using Engine.Events.CollisionEvent;
using Engine34.Managers.Render;
using Engine.Managers.Render;
using Engine.Interfaces.Resource;

namespace GameCode.Entities.Platformer
{
    /// <summary>
    /// CLASS: A test mind to control a test entity for demos.
    /// </summary>
    class TestMind : Mind
    {
        static bool spawned = false;
        bool originator = false;
        static Vector2 originatorDirection;

        double angle = 0;
        float stepWidth = 0.5f;
        GameText Angle;



        LightMask x;
        private Vector2 offset = new Vector2(-65, -50);

        public TestMind()
        {

        }
        /// <summary>
        /// METHOD: Initialises the basic properties of this mind.
        /// </summary>
        /// <param name="Position"></param>
        public override void Initialize(Vector2 Position)
        {
            ///SET: The texture of the entity this mind controls.
            texPath = "KingCholera";
            Team = 2;
            ///SET: The basic physics properties
            Mass = 0.5f;
            Restitution = 1f;
            Damping = 0.97f;
            Health = 10;
            Dmg = 1;
            MaxSpeed = 1.5f;
            Angle = new GameText(angle.ToString(), "mFont", Vector2.Zero, Color.Black, 1f);
            Locator.Instance.getService<IRenderManager>().addDrawable(Angle);
            ///ADD: Hitboxes for the entity
            //   Hits.Add(new Hitbox(new Vector2(Position.X, Position.Y + 3), 16, 6, 45, this));
            //  Hits.Add(new Hitbox(new Vector2(Position.X + 16, Position.Y + 3), 16, 6, -45, this));
            //   Hits.Add(new Hitbox(new Vector2(Position.X + 12, Position.Y + 16), 4, 16, 0, this));

            surroundSpawn();
            base.Initialize(Position);

            if (!originator)
            {
                texPath = "bullet1";
                x = new LightMask(this, Locator.Instance.getService<IResourceLoader>().GetTex("Grad"), 0.4f);
                Locator.Instance.getService<ILightMaskManager>().addMask(x);

            }
            base.Initialize(Position);


            if (originator)
            {
                x = new LightMask(this, Locator.Instance.getService<IResourceLoader>().GetTex("Grad"), 1f);
                Locator.Instance.getService<ILightMaskManager>().addMask(x);

            }

            ///CALL: The initialise of the abstract mind class
        }

        public void surroundSpawn()
        {
            if (spawned != true)
            {
                //TEST ENTITY PURPOSES///////////////////////
                spawned = true;
                originator = true;
                originatorDirection = Position;
                //////////////////////////////////////
                while (angle < 2 * MathHelper.Pi)
                {
                    Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<TestEntity>(new Vector2(Position.X + (float)Math.Cos(angle) * 150, Position.Y - (float)Math.Sin(angle) * 150));
                    angle += stepWidth;

                }
            }

        }

        public override void OnSATCollision(object sender, CollisionEventArgs e)
        {
            //   base.OnSATCollision(sender, e);
        }

        public override void Unload()
        {
            base.Unload();
        }


        /// <summary>
        /// METHOD: The update method which is called and ran through every frame.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            ///CALL: The update physics method to simulate movement
            //UpdatePhysics();

            double x = Velocity.X;
            double y = Velocity.Y;
            if (angle < 2 * MathHelper.Pi)
            {
                Angle.UpdateText("Angle - " + angle.ToString());
                //      ApplyForce(new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)));
                // Velocity = new Vector2(Velocity.X + (float)Math.Cos(angle), Velocity.Y + (float)Math.Sin(angle)) * 0.5f;
                angle += stepWidth;

            }
            else
            {
                angle = 0;
                stepWidth -= -0.01f;
            }

            if (!originator)
            {
                Vector2 direction = originatorDirection + Position;
                ApplyForce(direction * 0.001f);
            }

            if (originator)

            {
                ApplyForce(new Vector2(0.1f, 0.1f));
            }

            //if (angle < 256)
            //{
            //    Angle.UpdateText("Angle - " + angle.ToString());
            //    Velocity = new Vector2(Velocity.X + (float)angle, Velocity.Y + (float)Math.Sin(angle) * 10) * 0.5f;
            //    angle += stepWidth;

            //}

            //if (angle < 256)
            //{
            //    Angle.UpdateText("Angle - " + angle.ToString());
            //    Velocity = new Vector2(Velocity.X - (float)angle, Velocity.Y - (float)Math.Sin(angle) * 10) * 0.5f;
            //    angle += stepWidth;

            //}

            // angle = 0;
            ///CALL: The update of the abstract class mind.
            base.Update(gameTime);
        }
    }
}