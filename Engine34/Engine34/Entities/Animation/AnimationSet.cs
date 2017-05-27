using Engine.Interfaces.Render;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine34.Entities.Animation
{
   public class AnimationSet : IAnimation, IDrawableComponent
    {
     
        //position for the animations to be drawn relative to
        public Vector2 Position { get; set; }
        //The current animation state
        public IAnimationState current = null;
        //Reference to the animation table we use to animate our character
        public AnimationTable pat = null;

        public bool hasLifecycle { get; set; }
        public int LifecycleCounter { get { if (!hasLifecycle) { return 0; } else return Lifecycle; } set { Lifecycle = value; } }
        protected int Lifecycle;

        /// <summary>
        /// 
        /// </summary>
        public object Event
        {
            set
            {
                if (value == null)
                {
                    current.Exit();
                    current = null;
                    return;
                }
                IAnimationState i = pat.getState(value);

                if (i != null)
                {
                    if (current != null)
                        current.Exit();

                    current = i;
                    current.Enter();
                }
            }
        }

        /// <summary>
        /// Initialize the animation set
        /// </summary>
        /// <param name="table"></param>
        public void Initialize(AnimationTable table,Vector2 pos)
        {
            pat = table;
            pat.Initialize();
            Position = pos;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sp"></param>
        public void UpdateState(SpriteBatch sp)
        {
            if (current != null)
            {
                current.Handle(Position, sp);
            }
        }

        public void Draw(SpriteBatch sp)
        {
            UpdateState(sp);
        }

        public void Unload()
        {
            current.Exit();
        }

        public void Update(GameTime gt)
        {
            if (current != null)
            {
                current.Handle(gt);
            }

            if (hasLifecycle)
                Lifecycle--;
        }

        public void SwitchState(int state)
        {
            if(pat.getTableCount() > 0)
            {
                Event = state;
            }
        }


    }
}
