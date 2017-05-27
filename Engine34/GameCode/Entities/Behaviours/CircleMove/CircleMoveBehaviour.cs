using Engine.Utilities;
using Engine34.Entities.Behaviour;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameCode.Entities.Behaviours.CircleMove
{
    public class CircleMoveBehaviour : Behaviour
    {
        private double angle = 0;
        private double stepWidth = 0.01f;
        GameText Angle;

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void DoBehaviour()
        {
            if (angle < 2 * MathHelper.Pi)
            {
                Angle.UpdateText("Angle - " + angle.ToString());
                possessed.ApplyForce(new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)));
                // Velocity = new Vector2(Velocity.X + (float)Math.Cos(angle), Velocity.Y + (float)Math.Sin(angle)) * 0.5f;
                angle += stepWidth;

            }
            else
            {
                angle = 0;
                stepWidth -= -0.01f;
            }
            base.DoBehaviour();
        }

        
    }
}
