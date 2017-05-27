using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine34.Entities.Animation
{
  public class Animator
    {
        Texture2D tex { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }

        private int currentFrame;
        private int totalFrames;

        private float timeElapsed;
        public bool isLooping { get; set; }
        protected Vector2 currentPos;
        protected bool DrawCall = false;
        //Default to 20 fps
        private float timeToUpdate = 0.05f;

        public int FramesPerSecond
        {
            set { timeToUpdate = (1f / value); }
        }



        public void Initialize(Texture2D t, int rows, int cols)
        {
            tex = t;
            Rows = rows;
            Columns = cols;
            currentFrame = 0;
            totalFrames = Rows * Columns;
            isLooping = false;
       
        }

        public void Update(GameTime gameTime)
        {
            //Get the total time 
            timeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;
            //
            if (timeElapsed > timeToUpdate)
            {

                timeElapsed -= timeToUpdate;

                if (currentFrame < totalFrames - 1)
                    currentFrame++;
                else if (isLooping)
                    currentFrame = 0;
            }

            //currentFrame++;
            //If we've exceeded the amount of frames the spritesheet contains, reset our current frame (start again)
            // if (currentFrame == totalFrames)
            //   currentFrame = 0;
        }

        public void Draw(SpriteBatch sp, Vector2 pos)
        {
            currentPos = pos;
            int width = tex.Width / Columns;
            int height = tex.Height / Rows;

            int row = (int)((float)currentFrame / (float)Columns);
            int column = currentFrame % Columns;

            Rectangle source = new Rectangle(width * column, height * row, width, height);
            Rectangle des = new Rectangle((int)pos.X, (int)pos.Y, width, height);



            sp.Draw(tex, des, source, Color.White);
        }

    }
}

  