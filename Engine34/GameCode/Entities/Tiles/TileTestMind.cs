using Engine.Entities;
using Engine.Events.CollisionEvent;
using Engine.Interfaces.Collision;
using Engine.Interfaces.Entities;
using Engine.Managers.Collision;
using Engine.Managers.ServiceLocator;
using GameCode.Entities.Bullets;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameCode.Entities.Tiles
{
    class TileTestMind : Mind
    {
        public ICollidable parent;
        float Dist;
        public TileTestMind()
        {


        }

        public override void Initialize(Vector2 Position)
        {
            ///SET: the Texture of the entity this controls.
            texPath = "Tile1";
            Team = 4;
            ///SET: The physics properties of this object.
            Mass = 0f;
            Restitution = 1f;
            Damping = 0f;
            Health = int.MaxValue;
            Dmg = 0;
            MaxSpeed = 0;

            Hits.Add(new Hitbox(new Vector2(Position.X, Position.Y), 65, 65, 0, this));
            ///CALL: The initialise of the Abstract Mind class.
            base.Initialize(Position);
        }

        /// <summary>
        /// METHOD: The update method which is called and ran through every frame.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            ///CALL: The update of the abstract class mind.
            base.Update(gameTime);
        }



    }
}

