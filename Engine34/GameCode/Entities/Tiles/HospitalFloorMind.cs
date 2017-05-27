using Engine.Entities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameCode.Entities.Tiles
{
    class HospitalFloorMind : Mind
    {
        public override void Initialize(Vector2 Position)
        {
            ///SET: the Texture of the entity this controls.
            texPath = "Tile5";
            Team = 4;
            ///SET: The physics properties of this object.
            Mass = 0f;
            Restitution = 1f;
            Damping = 0f;
            Health = int.MaxValue;
            Dmg = 0;
            MaxSpeed = 0;
            ///CALL: The initialise of the Abstract Mind class.
            base.Initialize(Position);
        }
    }
}
