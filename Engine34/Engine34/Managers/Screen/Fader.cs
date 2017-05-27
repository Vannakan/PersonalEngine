using Engine.Interfaces.Render;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Engine.Managers.Screen
{
    //// <summary>
    //// CLASS: A fade transition between screens
    //// </summary>
    public enum Transition
    {
        In,
        Out
    }
    public class Fader : IDrawableComponent
    {
        /// <summary>
        /// DECLARE: Transition for moving out of a screen
        /// </summary>
        Transition trans = Transition.Out;

        /// <summary>
        /// DECLARE: bool for when the screen is ready to transition
        /// </summary>
        public bool ready = false;

        /// <summary>
        /// DECLARE: the texture and Rectangle for the dimensions
        /// </summary>
        Texture2D t;
        Rectangle draw;

        /// <summary>
        /// DECLARE: The transparency (alpha) and the speed of the transition
        /// </summary>
        float alpha = 1f;
  float Speed = 0.010f;
  int timer = 0;
        /// <summary>
        /// CONSTRUCTOR: Sets the basic properties of the fader transition
        /// </summary>
        /// <param name="Size">Size, the dimensions of the screen to be covered in the transition</param>
        public Fader(Vector2 Size)
        {
            ///SET: the texture to a constant black
            t = new Texture2D(Constants.g, 1, 1);
            t.SetData(new[] {
    Color.Black * alpha
   });
            ///SET: the bounds of the transition screen
            draw = new Rectangle(0, 0, (int)Size.X, (int)Size.Y);
        }

        /// <summary>
        /// CONSTRUCTOR: Sets the basic properties of the fader transition
        /// </summary>
        /// <param name="Size">Size, the dimensions of the screen to be covered in the transition</param>
        /// <param name="p">p, the top left corner of the fade transition</param>
        public Fader(Vector2 Size, Vector2 p)
        {
            ///SET: the texture to a constant black
            t = new Texture2D(Constants.g, 1, 1);
            t.SetData(new[] {
    Color.Black * alpha
   });
            ///SET: the bounds of the transition screen
            draw = new Rectangle((int)p.X, (int)p.Y, (int)Size.X, (int)Size.Y);
        }

        /// <summary>
        /// CONSTRUCTOR: Sets the basic properties of the fader transition
        /// </summary>
        /// <param name="Size">Size, the dimensions of the screen to be covered in the transition</param>
        /// <param name="p">p, the top left corner of the fade transition</param>
        /// <param name = "speed">speed, how quickly the screen will fade to and from black</param>
        public Fader(Vector2 Size, Vector2 p, float speed)
        {
            Speed = speed;
            t = new Texture2D(Constants.g, 1, 1);
            t.SetData(new[] {
    Color.Black * alpha
   });
            draw = new Rectangle((int)p.X, (int)p.Y, (int)Size.X, (int)Size.Y);
        }

        /// <summary>
        /// METHOD: Draw the transition
        /// </summary>
        /// <param name="sb">Monogame SpriteBatch</param>
        public void Draw(SpriteBatch sb)
        {
            sb.Draw(t, draw, Color.Black);
        }

        /// <summary>
        /// METHOD: The update of the transition which is called every frame
        /// </summary>
        public void Update()
        {
            ///INCREMENT: the timer
            timer++;
            ///IF: Transitioning in to the fade, increase the alpha making the black box more opaque
            if (trans == Transition.In)
                alpha += Speed;
            ///ELSE: Transitioning out of the fade, decrease the alpha making the black box less opaque
            else if (trans == Transition.Out)
                alpha -= Speed;

            ///IF: Fully transparent
            if (alpha <= 0 && trans == Transition.Out)
            {
                ///SET: Alpha 0 and ready true
                ready = true;
                alpha = 0;
            }

            ///IF: Fully Opaque
            if (alpha >= 1 && timer >= 100)
            {
                ///SET: Alpha 1 and transition to Out
                alpha = 1;
                trans = Transition.Out;
            }
            ///Adjust texture
            t.SetData(new[] {
    Color.Black * alpha
   });

        }



    }
}