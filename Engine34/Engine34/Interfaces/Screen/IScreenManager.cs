using Engine.Interfaces.ServiceLocator;
using Engine.States.Engine;
using Engine34.States.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Engine.Interfaces.Screen
{
    /// <summary>
    /// INTERFACE: This interface holds the implementation for the ScreenManager which is responsible for loading the content
    /// in each screen and displaying each screen. The ScreenManager contains a stack of screens which allows for multiple 
    /// screens to be prepared for loading whilst only one is an active screen
    /// </summary>
    public interface IScreenManager : IUpdService
    {
        /// <summary>
        /// METHOD: Initialise the Manager and add the first screen to be loaded
        /// </summary>
        void Initialize();
        /// <summary>
        /// METHOD: Unload any screens that need to be unloaded
        /// </summary>
        void UnloEnginecreens();
        /// <summary>
        /// METHOD: Update the manager
        /// </summary>
        /// <param name="gameTime">Monogame GameTime property</param>
        void Update(GameTime gameTime);
        /// <summary>
        /// METHOD: Draw any objects that require drawing
        /// </summary>
        /// <param name="spriteBatch">Monogame SpriteBatch property</param>
        void Draw(SpriteBatch spriteBatch);
        /// <summary>
        /// METHOD: Add a new screen to the stack of screens to be loaded and displayed
        /// </summary>
        /// <param name="screenName">The name of the screen to be added</param>
        void Add<T>(string screenName)where T : BaseScreen, new();
        void AddPop<T>(string screenName) where T : PopupScreen, new();

        /// <summary>
        /// METHOD: Replace the current screen
        /// </summary>
        /// <param name="screenName">The name of the screen which will replace the screen</param>
        void ReplaceScreen<T>(string ScreenName) where T : BaseScreen, new();
        /// <summary>
        /// METHOD: Check if any of the screens in the stack are the same as the current screen and makes them active if so
        /// </summary>
        void CheckScreens();
        /// <summary>
        /// METHOD: check for input to select different options and move between different screens
        /// </summary>
        void CheckScreenManagerInput();
        /// <summary>
        /// METHOD: Unloads and removes the top screen from the stack, essentially going back one screen
        /// </summary>
        void RemoveTopScreen();
        /// <summary>
        /// METHOD: Call the update method of the top screen
        /// </summary>
        /// <param name="gameTime"></param>
        void UpdateTopScreen(GameTime gameTime);
        /// <summary>
        /// GET: SET: Monogame ContentManager
        /// </summary>
        ContentManager content
        {
            get;
            set;
        }

        IScreen getTopScreen();

    }
}