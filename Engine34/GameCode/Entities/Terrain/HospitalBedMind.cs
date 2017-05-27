using Engine.Entities;
using Engine.Managers.Collision;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameCode.Entities.Terrain
{
    class HospitalBedMind : Mind
    {
        public override void Initialize(Vector2 Position)
        {
            ///SET: the Texture of the entity this controls.
            texPath = "HospitalBed";
            Team = 4;
            ///SET: The physics properties of this object.
            Mass = 0f;
            Restitution = 1f;
            Damping = 0f;
            Health = int.MaxValue;
            Dmg = 0;
            MaxSpeed = 0;
            ///CALL: The initialise of the Abstract Mind class.
            Hits.Add(new Hitbox(new Vector2(Position.X, Position.Y), 38, 52, 0, this));
            base.Initialize(Position);
        }
    }
}
