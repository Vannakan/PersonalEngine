using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine.Interfaces.Screen
{
    /// <summary>
    /// INTERFACE: all screens subscribe to this interface which contains the implementation for what each screen needs
    /// To do
    /// </summary>
    public interface IScreen
    {
        /// <summary>
        /// METHOD: Unloads the content of the screen
        /// </summary>
        void UnloadContent();
        /// <summary>
        /// METHOD: Initialises the logic of the screen
        /// </summary>
        void Initialize();
        /// <summary>
        /// METHOD: The update loop which is cycled through each frame
        /// </summary>
        /// <param name="gameTime">the Monogame GameTime property</param>
        void Update(GameTime gameTime);
        /// <summary>
        /// METHOD: Draws the content of the screen
        /// </summary>
        /// <param name="spriteBatch">the Monogame SpriteBatch</param>
        void Draw(SpriteBatch spriteBatch);
        /// <summary>
        /// METHOD: Unloads the screen
        /// </summary>
        void Unload();

     

    }
}