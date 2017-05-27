using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Engine.Interfaces.InputManager;

namespace Engine.Managers.Input
{
    /// <summary>
    /// CLASS: The manager for input. This manager is responsbile for storing an array of keys pressed down and also held
    /// down on a keyboard. 
    /// </summary>
    public class InputManager : IInputManager
    {
     ///DECLARE: The current keys pressed on the Keyboard
  private KeyboardState currentKeyState;
        ///DECLARE: The previous state of keys which will be used when checking for if a key has been released
        private KeyboardState previousKeyState;

     //// <summary>
     //// METHOD: This Update method simply gets the current keystate, as well as updating the previous key state to what the
     //// key state was at the last update
     //// </summary>
     //// <param name="gameTime">Monogame GameTime property</param>
     public void Update(GameTime gameTime)
        {
            previousKeyState = currentKeyState;

            currentKeyState = Keyboard.GetState();

            MouseState m = Mouse.GetState();
        }

  /// <summary>
  /// METHOD:Check if the left key is down and return true if this is the case
  /// </summary>
  /// <param name="k">The key to be checked</param>
  /// <returns>a bool of whether the key is held down or not</returns>
  /// 
  /// 
  public bool CheckHeldDown(Keys k)
        {
            bool heldDown = false;

            if (currentKeyState.IsKeyDown(k))
            {
                heldDown = true;
            }

            return heldDown;

        }

        /// <summary>
        /// METHOD: check if a key has been pressed when it wasn't in the last loop
        /// </summary>
        /// <param name="k">The key to be checked</param>
        /// <returns>a bool of whether the key is pressed or not</returns>
        public bool CheckKeyPressed(Keys k)
        {
            bool checkLeft = false;

            if (currentKeyState.IsKeyDown(k) && previousKeyState.IsKeyUp(k))
            {
                checkLeft = true;
            }

            return checkLeft;
        }
    }
}