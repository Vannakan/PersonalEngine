using Engine.Interfaces.ServiceLocator;
using Engine.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Engine.Interfaces.InputManager
{
    /// <summary>
    /// INTERFACE: An interface which declares the implementation that the input manager must contain. The input manager 
    /// is used to check for what keys are pressed and held on a keyboard.
    /// </summary>
    public interface IInputManager : IUpdService
    {
        /// <summary>
        /// METHOD: Update loop which is cycled through every frame
        /// </summary>
        /// <param name="gameTime">the Monogame GameTime</param>
        void Update(GameTime gameTime);

        /// <summary>
        /// METHOD: returns true if when a key is pressed
        /// </summary>
        /// <param name="k">A key to be checked</param>
        /// <returns>a bool whether the key is pressed or not</returns>
        bool CheckKeyPressed(Keys k);

        /// <summary>
        /// METHOD: returns true when a key is held down
        /// </summary>
        /// <param name="k">the key to be checked</param>
        /// <returns>a bool on whether the key has been held down for more than one frame</returns>
        bool CheckHeldDown(Keys k);
    }
}