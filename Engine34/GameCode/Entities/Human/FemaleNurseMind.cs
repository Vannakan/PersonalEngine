using Engine.Entities;
using Engine.Events.CollisionEvent;
using Engine.Managers.Collision;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameCode.Entities.Human
{
    class FemaleNurseMind : Mind
    {

        /// <summary>
        /// METHOD: Initialises the basic properties of this mind.
        /// </summary>
        /// <param name="Position"></param>
        public override void Initialize(Vector2 Position)
        {
            ///SET: The texture of the entity this mind controls.
            texPath = "Nurse";
            Team = 2;
            ///SET: The basic physics properties
            Mass = 0f;
            Restitution = 0f;
            Damping = 0.8f;
            Health = 10;
            Dmg = 0;
            MaxSpeed = 1f;
            ///ADD: Hitboxes for the entity
            Hits.Add(new Hitbox(new Vector2(Position.X, Position.Y), 29, 46, 45, this));

            ///CALL: The initialise of the abstract mind class
            base.Initialize(Position);
        }

        public override void OnSATCollision(object sender, CollisionEventArgs e)
        {
            ///IF: This mind is object A, Apply Impulse to simulate physics
            if (e.A == this)
            {
                ApplyImpulse(-e.mtvRet);
                e.B.getEntity().Mind.Talk(this);
            }
            ///ELSE IF: this mind is object B, apply impulse in opposite direction
            else if (e.B == this)
            {
                ApplyImpulse(e.mtvRet);
                e.A.getEntity().Mind.Talk(this);
            }
        }
    }
}
