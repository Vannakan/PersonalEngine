using Engine.Entities;
using Engine.Events.CollisionEvent;
using Engine.Events.KeyboardEvent;
using Engine.Events.MouseEvent;
using Engine.Grid;
using Engine.Interfaces.Collision;
using Engine.Interfaces.Entities;
using Engine.Interfaces.Render;
using Engine.Interfaces.Resource;
using Engine.Interfaces.Screen;
using Engine.Interfaces.Sound;
using Engine.Managers.Collision;
using Engine.Managers.Render;
using Engine.Managers.ServiceLocator;
using Engine.Utility;
using Engine34.Grid;
using Engine34.Managers.Render;
using GameCode.Entities.Bullets;
using GameCode.Entities.Types;
using GameCode.Items.mStats;
using GameCode.Screens.PopupScreens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace GameCode.Entities.Platformer
{
    /// <summary>
    /// CLASS: The mind that controls the player Entity.
    /// </summary>
    public class PlayerMind :  DamagableMind
    {
        ILightMask x;
        // Vector2 offset = new Vector2(-500, -500);
        Vector2 offset = new Vector2(-250, -250);

        int intro = 1000;

        int shootCounter = 0;
        ///DECLARE: The timer that will be used for the shooting mechanic.
        int bulletTimer = 0;
        IEntity bul;

        public IStats stats;
        /// <summary>
        /// CONSTRUCTOR: Initialises the basic properties of this object.
        /// </summary>
        public PlayerMind()
        {
            ///SET: This is an object that will be used for calculating collision.
            isCollidable = true;
            stats = new Stats(100,100,100 ,0,0);
            ///CREATE: Event handlers for the behaviour of this object.
            Locator.Instance.getService<MouseHandler>().MouseClick += OnMouseDown;
            Locator.Instance.getService<KeyHandler>().KeyDown += OnKeyDown;
            Locator.Instance.getService<KeyHandler>().KeyHeld += OnKeyHeld;
            Locator.Instance.getService<IDetectionManager>().OnCollision += OnCollision;


        }

        /// <summary>
        /// METHOD: Initialise the object and sets properties accordingly.
        /// </summary>
        /// <param name="Position"></param>
        /// 
        square test11;

        public override void Initialize(Vector2 Position)
        {
            ///SET: the Texture of the entity this controls.
            texPath = "AntiBody";
            Team = 1;
            ///SET: The physics properties of this object.
            Mass = 0.5f;
            Restitution = 1f;
            Damping = 0.9f;
            Health = 10000;
            Dmg = 1;
            MaxSpeed = 1f;
            ///ADD: The hitboxes of this object.
            Hits.Add(new Hitbox(new Vector2(Position.X, Position.Y + 3), 16, 6, 45, this));
            Hits.Add(new Hitbox(new Vector2(Position.X + 16, Position.Y + 3), 16, 6, -45, this));
            Hits.Add(new Hitbox(new Vector2(Position.X + 12, Position.Y + 16), 4, 16, 0, this));
            test11 = DrawPrimitives.DrawCamRect(Bounds, Color.White);

            x = new LightMask(Position + offset, Locator.Instance.getService<IResourceLoader>().GetTex("Grad"), 2f);
            Locator.Instance.getService<ILightMaskManager>().addMask(x);

            ///CALL: The initialise of the Abstract Mind class.
            base.Initialize(Position);
        }

        /// <summary>
        /// METHOD: Update, controls the behaviour of this object and is looped through every frame.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            x.Position = Position + offset;

            intro--;
            if (intro < 0)
                intro = 0;

                if (shootCounter > 0)
            shootCounter--;
            if (bulletTimer >= 15)
            {
                bulletTimer = 15;
            }

            else
            {
                bulletTimer++;
            }
      

            ///CALL: The updatePhysics method which controls the physical simulation of movement of the object.
            UpdatePhysics();
            ///CALL: the update method of the abstract mind class.
            ///
            test11.Bounds.X = (int)Position.X;
            test11.Bounds.Y = (int)Position.Y;
            base.Update(gameTime);
          
        }

        /// <summary>
        /// METHOD: Responsible for unloading everything and sending it to the garbage collector to ensure no memory leaks.
        /// </summary>
        public override void Unload()
        {
            Locator.Instance.getService<KeyHandler>().KeyDown -= OnKeyDown;
            Locator.Instance.getService<KeyHandler>().KeyHeld -= OnKeyHeld;
            Locator.Instance.getService<MouseHandler>().MouseClick -= OnMouseDown;
            Locator.Instance.getService<IRenderManager>().Remove(test11);
            Locator.Instance.getService<ILightMaskManager>().RemoveLightSource(x);
        }

        /// <summary>
        /// EVENT: When a key is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="m"></param>
        public void OnKeyDown(object sender, KeyEventArgs m)
        {
        }

        /// <summary>
        /// EVENT: When a key is held down
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="m"></param>
        public void OnKeyHeld(object sender, KeyEventArgs m)
        {
            ///INITIALISE: An array of keys on the keyboard that are pressed and held
            Keys[] keys = m.keyState.GetPressedKeys();

            ///IF: D is held
            if (m.key == Keys.D)
            {
                ///CALL: Apply Force in the right direction
                ApplyForce(new Vector2(MaxSpeed, 0));
            }
            ///IF: A is held
            if (m.key == Keys.A)
            {
                ///CALL: Apply force in the left direction
                ApplyForce(new Vector2(-MaxSpeed, 0));
            }
            ///IF: S is held
            if (m.key == Keys.S)
            {
                ///CALL: Apply force in the down direction
                ApplyForce(new Vector2(0, MaxSpeed));
            }
            ///IF: W is held
            if (m.key == Keys.W)
            {
                ///CALL: Apply force in the up direction
                ApplyForce(new Vector2(0, -MaxSpeed));
            }

            if (m.key == Keys.Left && shootCounter <= 0)
            {
                ///CALL: Apply Force in the right direction
                Locator.Instance.getService<ISoundManager>().PlayEffect("pop");
                ShootBullet(new Vector2(-10, 0));
                shootCounter = 50;
            }

            else if (m.key == Keys.Right && shootCounter <= 0)
            {
                Locator.Instance.getService<ISoundManager>().PlayEffect("pop");

                ShootBullet(new Vector2(10, 0));
                shootCounter = 50;

            }

            else if (m.key == Keys.Down && shootCounter <= 0)
            {
                Locator.Instance.getService<ISoundManager>().PlayEffect("pop");

                ShootBullet(new Vector2(0, 10) );
                shootCounter = 50;

            }

            else if (m.key == Keys.Up && shootCounter <= 0)
            {
                Locator.Instance.getService<ISoundManager>().PlayEffect("pop");

                ShootBullet(new Vector2(0, -10));
                shootCounter = 50;

            }

            if(m.key == Keys.H)
            {
                Locator.Instance.getService<IScreenManager>().AddPop<StatScreen>("Stats");
            }

        }

        public override void OnSATCollision(object sender, CollisionEventArgs e)
        {
          
              

                    ///IF: This mind is object A, Apply Impulse to simulate physics
                    //if (e.A == this && e.B.GetType() != typeof(BulletMind))
                    //{
                    //   ApplyImpulse(-e.mtvRet);

                    //}
                    /////ELSE IF: this mind is object B, apply impulse in opposite direction
                    //else if (e.B == this && e.A.GetType() != typeof(BulletMind))
                    //{
                    //    ApplyImpulse(e.mtvRet);

                    //}

                        

           // base.OnSATCollision(sender, e);
        }

        /// <summary>
        /// EVENT: When the mouse is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="m"></param>
        public void OnMouseDown(object sender, MouseEventArgs m) { }

        /// <summary>
        /// EVENT: When colliding with another object (AABB Collision).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="cae"></param>
        public void OnCollision(object sender, CollisionEventArgs cae)
        {
            ///IF: This is object A
            if (cae.A == this)
            {
                ///SET: The object is colliding
                isColliding = true;
                ///ADD: The minimum translation vector to the position.
                _pos += TranslationVector.GetMinimumTranslation(cae.A.Bounds, cae.B.Bounds);

            }
        }

        public void ShootBullet(Vector2 direction)
        {
            if (bulletTimer >= 10)
            {
                bul = Locator.Instance.getService<IEntityManager>().createEntityDrawable<Bullet>(Position);
                Locator.Instance.getService<IDetectionManager>().addCollidable((ICollidable)bul.Mind);
                BulletMind b = (BulletMind)bul.Mind;
               b.setBulletTexture("Projectile2");
                bul.Mind.Parent = this;
                bul.Mind.Dmg = stats.DMG;
                bul.Mind.Team = 3;
                bul.Mind.ApplyForce(direction);
                bulletTimer = 0;
            }
        }

       




    }
}