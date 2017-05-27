using Engine.Interfaces.ServiceLocator;
using Engine.Managers;
using Engine.Managers.Cam;
using Microsoft.Xna.Framework;


namespace Engine.Interfaces.Cam
{
    /// <summary>
    /// INTERFACE:The interface for the Camera manager. This controls the implementation for the manager of the camera.
    /// </summary>
    public interface ICameraManager : IUpdService
    {
        /// <summary>
        /// METHOD: Contains initialisation logic
        /// </summary>
        void Initialize();

        /// <summary>
        /// METHOD: Returns the camera
        /// </summary>
        /// <returns>the current Camera</returns>
        Camera getCam();

        /// <summary>
        /// METHOD: The update loop which is cycled through every frame
        /// </summary>
        /// <param name="gameTime"></param>
        void Update(GameTime gameTime);

        /// <summary>
        /// METHOD: returns the current world position in relation to the Vector2 provided
        /// </summary>
        /// <param name="Position">The position to be compared to the world position</param>
        /// <returns>The world position in relation to the Vector2 provided</returns>
        Vector2 getWorldPosition(Vector2 Position);
    }
}