using Engine.Entities;
using Engine.Events.CollisionEvent;
using Engine.Events.KeyboardEvent;
using Engine.Events.MouseEvent;
using Engine.Interfaces.Entities;
using Engine.Interfaces.Screen;
using Engine.Managers.Collision;
using Engine.Managers.ServiceLocator;
using Engine.States.Engine;
using GameCode.Entities.Terrain;
using GameCode.Screens.Levels;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameCode.Entities.Human
{
    class BenTiptonMind : Mind
    {
        private bool talking;
        public BenTiptonMind()
        {
            talking = false;
            Locator.Instance.getService<MouseHandler>().MouseClick += OnMouseDown;
            Locator.Instance.getService<KeyHandler>().KeyDown += OnKeyDown;
            Locator.Instance.getService<KeyHandler>().KeyHeld += OnKeyHeld;
        }
        public override void Initialize(Vector2 Position)
        {
            ///SET: The texture of the entity this mind controls.
            texPath = "CharlesHasting2";
            Team = 1;
            ///SET: The basic physics properties
            Mass = 0.75f;
            Restitution = 1f;
            Damping = 0.5f;
            Health = 10000;
            Dmg = 0;
            MaxSpeed = 1f;
            ///ADD: Hitboxes for the entity
            Hits.Add(new Hitbox(new Vector2(Position.X, Position.Y), 29, 38, 0, this));

            ///CALL: The initialise of the abstract mind class
            base.Initialize(Position);
        }

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

        }
        public void OnKeyDown(object sender, KeyEventArgs m)
        {
        }

        public void OnMouseDown(object sender, MouseEventArgs m) { }

        public override void Talk(IMind e)
        {
            if (!talking)
            {
                Console.WriteLine("talking");
                talking = true;
            }
            else
            {
                Console.WriteLine("can't talk cause already talking");
                talking = false;
            }
        }
        
        public override void OnSATCollision(object sender, CollisionEventArgs e)
        {
            if(e.A == this)
            {
                Console.WriteLine("HI");
               IScreen x = Locator.Instance.getService<IScreenManager>().getTopScreen();
                Locator.Instance.getService<IScreenManager>().ReplaceScreen<Dungeon1>("2");
            }
            if (e.B == this && e.A.GetType() == typeof(HospitalBed))
            {
                Locator.Instance.getService<IScreenManager>().ReplaceScreen<Dungeon1>("2");
            }
            base.OnSATCollision(sender, e);
        }
    }
    }
