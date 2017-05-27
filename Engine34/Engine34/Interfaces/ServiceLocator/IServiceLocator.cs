using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Engine.Interfaces.ServiceLocator
{
    /// <summary>
    /// INTERFACE: An Interface holding the implementation for the Service Locator which removes the need for singletons
    /// throughout the engine
    /// </summary>
    public interface IServiceLocator
    {
        /// <summary>
        /// METHOD: Return a service, T is a generic type of Manager
        /// </summary>
        /// <typeparam name="T">Generic Manager</typeparam>
        /// <returns>Manager of type T</returns>
        T getService<T>();

        /// <summary>
        /// METHOD: Initialises the services upon the running of the program so that they all exist and can be accessed
        /// </summary>
        /// <param name="c"> Monogame ContentManager</param>
        /// <param name="sb"> Monogame Spritebatch for any drawing</param>
        void InitializeServices(ContentManager c, SpriteBatch sb);

        /// <summary>
        /// Loops through and updates each service in turn.
        /// </summary>
        /// <param name="gameTime"></param>
        void UpdateServices(GameTime gameTime);
        Game _Game1 { get; set; }
    }
}