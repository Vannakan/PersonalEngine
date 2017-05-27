using Engine.Entities;
using Engine.Interfaces.Behaviour;
using Engine.Managers.ServiceLocator;
using GameCode.Entities.Platformer;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Engine.Utilities;
using Engine.Interfaces.Resource;
using Engine.Utility;

namespace GameCode.Entities.Enemies.Boss
{
    class Boss1 : Entity
    {
        /// <summary>
        /// METHOD: Initialise the basic properties of the entity.
        /// </summary>
        /// <param name="Pos"></param>
        int yOffset = 25;
        public override void Initialize(Vector2 Pos)
        {
            ///SET: the mind controlling this entity
            mind = Locator.Instance.getService<IBehaviourManager>().Create<Boss1Mind>(this);

            ///CALL: The initialise of the abstract entity class.
            base.Initialize(Pos);

            ///SET: The name of this entity.
            Name = "Boss1";

     
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            DrawString.Draw("KING CHOLERA", spriteBatch,new Vector2(Position.X +15,Position.Y - yOffset), Color.White, 1f);
            base.Draw(spriteBatch);
        }
    }
}
