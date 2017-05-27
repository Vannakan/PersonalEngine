using Engine.Utilities;
using Engine34.Entities.Behaviour;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameCode.Entities.Behaviours.Wave
{
    public enum WaveDirection { Up,Down,Left,Right}
    public class WaveBehaviour : Behaviour
    {
        private WaveDirection Direction = WaveDirection.Right;
        private double angle = 0;
        private double stepWidth = 0.01f;
        GameText Angle;
        bool Forwards = true;

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void DoBehaviour()
        {
            if (Forwards)
            {
                if (angle < 256)
                {
                    Angle.UpdateText("Angle - " + angle.ToString());
                    possessed.Velocity = new Vector2(possessed.Velocity.X + (float)angle, possessed.Velocity.Y + (float)Math.Sin(angle) * 10) * 0.5f;
                    angle += stepWidth;

                }
            }else if (angle < 256)
            {
                Angle.UpdateText("Angle - " + angle.ToString());
                possessed.Velocity = new Vector2(possessed.Velocity.X - (float)angle, possessed.Velocity.Y - (float)Math.Sin(angle) * 10) * 0.5f;
                angle += stepWidth;

            }
            base.DoBehaviour();
        }


    }
}
