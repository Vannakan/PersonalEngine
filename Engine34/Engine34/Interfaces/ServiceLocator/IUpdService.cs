using Microsoft.Xna.Framework;

namespace Engine.Interfaces.ServiceLocator
{
    /// <summary>
    /// INTERFACE: All Managers that need to be updated every frame subscribe to this interface
    /// </summary>
    public interface IUpdService
    {
        /// <summary>
        /// METHOD: The update loop which is cycled through ever frame
        /// </summary>
        /// <param name="gameTime">The MonoGame Gametime property</param>
        void Update(GameTime gameTime);
    }
}