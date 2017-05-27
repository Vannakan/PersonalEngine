using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace Engine.Interfaces.Resource
{
    /// <summary>
    /// INTERFACE: Holds the implementation for the Resource Loader which is responsible for loading and storing any
    /// external resources such as textures and sounds.
    /// </summary>
    public interface IResourceLoader
    {
        /// <summary>
        /// GET: SET: The Monogame ContentManager
        /// </summary>
        ContentManager Content
        {
            get;
            set;
        }

        /// <summary>
        /// METHOD: Get a Texture from a string directory
        /// </summary>
        /// <param name="name">the name of the texture file to be loaded</param>
        /// <returns>a Texture2D file of the image at the name provided</returns>
        Texture2D GetTex(string name);

        /// <summary>
        /// METHOD: Get a font from a string directory
        /// </summary>
        /// <param name="name">The name of the font file to be loaded</param>
        /// <returns>a SpriteFont of the font at the name provided</returns>
        SpriteFont GetFont(string name);

        /// <summary>
        /// METHOD: Get an audio file from a string directory
        /// </summary>
        /// <param name="path">The name of the audio file to be loaded</param>
        /// <returns>A song of the audio at the name provided</returns>
        Song GetSong(string path);

        /// <summary>
        /// METHOD: Initialises and Loads appropriate resources
        /// </summary>
        void Initialize();

        /// <summary>
        /// METHOD: checks if a music file in the current library is valid and returns a bool
        /// </summary>
        /// <param name="name">The name of the audio file to look for</param>
        /// <returns>a boolean if the audio file exists in the queue</returns>
        bool CheckLibrary(string name);

        void LoEngineEffect();
        void LoEngineSound(string start, string name);
        SoundEffect GetSound(string name);

    }
}