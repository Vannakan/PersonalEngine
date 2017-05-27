using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;
using System.IO;
using Engine.Interfaces.Resource;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework;

namespace Engine.Managers.Resource
{
    /// <summary>
    /// CLASS: The Resource loader is responsible for loading and storage of external resources such as textures and sounds
    /// </summary>
    public class ResourceLoader : IResourceLoader
    {
        ///GET: SET: Monogame ContentManager 
        public ContentManager Content
        {
            get;
            set;
        }
        ///DECLARE: Dictionary for Spritefonts
        public Dictionary<string, SpriteFont> Fonts = new Dictionary<string, SpriteFont>();
        ///DECLARE: Dictionary for the type of Texture2D
        public Dictionary<string, Texture2D> Textures = new Dictionary<string, Texture2D>();
        ///DECLARE: Dictionray for the type of Song
        public Dictionary<string, Song> Music = new Dictionary<string, Song>();

        public Dictionary<string, SoundEffect> Sounds = new Dictionary<string, SoundEffect>();

     //// <summary>
     //// METHOD: Load all desired content
     //// Split into X method calls each one goes through the relevant directory (e.g \\Tiles or \\Sound 
     //// and loEngine each object into the content pipeline. Although make sure that the files have been 
     //// added to the content pipeline first pl0x
     //// </summary>
     public void Initialize()
        {
            LoadGUI();
            LoadTiles();
            LoEngineound();
            LoEngineEffect();
            LoadEntity();
            LoadMisc();
        }

  /// <summary>
  /// METHOD: Takes a string parameter and attempts to load a texture pointed to from the string.
  /// </summary>
  /// <param name="path">The path of the texture to be loaded</param>
  public void LoadTexture(string path)
        {

            Texture2D texture = Content.Load<Texture2D>(path);
            texture.Name = path;
            Textures.Add(path, texture);


        }

        /// <summary>
        /// METHOD: Takes two strings, one as the directory, if not the same as the Content pipeline, and one as the file name
        /// and attempts to load the texture
        /// </summary>
        /// <param name="start">The folder of the file</param>
        /// <param name="name">The name of the file</param>
        public void LoadTexture(string start, string name)
        {

            Texture2D texture = Content.Load<Texture2D>(start + name);
            texture.Name = name;
            Textures.Add(name, texture);
        }

        /// <summary>
        /// METHOD: returns a Texture from the string provided
        /// </summary>
        /// <param name="name">The name of the file</param>
        /// <returns>A texture2D of the same name</returns>
        public Texture2D GetTex(string name)
        {
            if (name != null)
            { 
                if (Textures.ContainsKey(name))
                {
                    Texture2D tex = Textures[name];
                    return tex;
                }
        }
            return Textures["NullTexture"];
        }

    

  /// <summary>
  /// METHOD: Load a SpriteFont from the content pipeline via a string and add it into the dictionary
  /// </summary>
  /// <param name="start">The folder of the file</param>
  /// <param name="name">The name of the file</param>
  public void LoadFont(string start, string name)
        {
            SpriteFont font = Content.Load<SpriteFont>(start + name);
            Fonts.Add(name, font);
        }

        //// <summary>
        //// METHOD: Check the given string through the dictionary and return that SpriteFont
        //// </summary>
        //// <param name="Name">The name of the file</param>
        //// <returns>A spriteFont of the same name</returns>
        public SpriteFont GetFont(string Name)
        {
            if (Fonts.ContainsKey(Name))
            {
                SpriteFont font = Fonts[Name];
                return font;
            }
            return null;
        }

  /// <summary>
  /// METHOD: Checks the current song library for an audio file with the name provided in parameters
  /// </summary>
  /// <param name="name">The name of the audio file requested</param>
  /// <returns>a boolean for if the audio file is in the library or queue</returns>
  public bool CheckLibrary(string name)
        {
            if (Music.ContainsKey(name))
            {

                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// METHOD: Load a song if it is not already in the queue
        /// </summary>
        /// <param name="start">the folder of the song</param>
        /// <param name="name">the name of the song</param>
        public void LoEngineong(string start, string name)
        {
            Song song = Content.Load<Song>(start + name);
            if (!Music.ContainsKey(name))
                Music.Add(name, song);
        }

        public void LoEngineSound(string start, string name)
        {
             SoundEffect song = Content.Load<SoundEffect>(start + name);
            if (!Music.ContainsKey(name))
                Sounds.Add(name, song);
        }

      
        /// <summary>
        /// METHOD: Return a song based on the string given in parameters
        /// </summary>
        /// <param name="name">The name of the file to be searched for in the music library</param>
        /// <returns>A song of the same name</returns>
        public Song GetSong(string name)
        {
            if (Music.ContainsKey(name))
            {
                Song song = Music[name];
                return song;
            }

            return null;
        }

        public SoundEffect GetSound(string name)
        {
            if (Sounds.ContainsKey(name))
            {
                SoundEffect song = Sounds[name];
                return song;
            }

            return null;
        }

        /// <summary>
        /// METHOD: Load the textures for any tiles to be used.
        /// </summary>
        public void LoadTiles()
        {
            string[] filePaths = Directory.GetFiles("Content\\Tiles");

            for (int i = 0; i < filePaths.Length; i++)
            {
                filePaths[i] = Path.GetFileNameWithoutExtension(filePaths[i]);
                LoadTexture("Tiles\\", filePaths[i]);
            }
        }

        /// <summary>
        /// METHOD: Load any sound files to be used
        /// </summary>
        public void LoEngineound()
        {
            string[] filePaths = Directory.GetFiles("Content\\Songs");

            for (int i = 0; i < filePaths.Length; i++)
            {

                filePaths[i] = Path.GetFileNameWithoutExtension(filePaths[i]);

                LoEngineong("Songs\\", filePaths[i]);
            }
        }

        public void LoEngineEffect()
        {
            string[] filePaths = Directory.GetFiles("Content\\Sounds");

            for (int i = 0; i < filePaths.Length; i++)
            {
                filePaths[i] = Path.GetFileNameWithoutExtension(filePaths[i]);

                LoEngineSound("Sounds\\", filePaths[i]);
            }
        }

        /// <summary>
        /// METHOD: Load any entity textures to be used
        /// </summary>
        public void LoadEntity()
        {
            string[] filePaths = Directory.GetFiles("Content\\Entity");

            for (int i = 0; i < filePaths.Length; i++)
            {
                filePaths[i] = Path.GetFileNameWithoutExtension(filePaths[i]);
                LoadTexture("Entity\\", filePaths[i]);
            }

        }

        /// <summary>
        /// METHOD: Load any miscellaneous Objects to be used 
        /// </summary>
        public void LoadMisc()
        {
            string[] filePaths = Directory.GetFiles("Content\\Misc");

            for (int i = 0; i < filePaths.Length; i++)
            {
                filePaths[i] = Path.GetFileNameWithoutExtension(filePaths[i]);
                LoadTexture("Misc\\", filePaths[i]);
            }
        }

        /// <summary>
        /// METHOD: Load any GUI Assets
        /// </summary>
        public void LoadGUI()
        {
            string[] filePaths = Directory.GetFiles("Content\\GUI");
            string[] filePaths1 = Directory.GetFiles("Content\\Fonts");

            for (int i = 0; i < filePaths.Length; i++)
            {
                filePaths[i] = Path.GetFileNameWithoutExtension(filePaths[i]);

                LoadTexture("GUI\\", filePaths[i]);
            }
            for (int i = 0; i < filePaths1.Length; i++)
            {
                filePaths1[i] = Path.GetFileNameWithoutExtension(filePaths1[i]);

                LoadFont("Fonts\\", filePaths1[i]);

            }
        }
    }
}