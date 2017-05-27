using Engine.Interfaces.ServiceLocator;
using Engine.States.Engine;
using Microsoft.Xna.Framework;

namespace Engine.Interfaces.Sound
{
    /// <summary>
    /// INTERFACE: Holds the implementation for the sound manager which is responsible for the storage and playback
    /// of sound files.
    /// </summary>
    public interface ISoundManager : IUpdService
    {
        /// <summary>
        /// METHOD: Initialises the logic of the Manager
        /// </summary>
        void Initialize();
        /// <summary>
        /// METHOD: Plays the song of the name provided
        /// </summary>
        /// <param name="name">The name of the song to be played</param>
        void Play(string name);
        /// <summary>
        /// METHOD: Plays the sound effect of the name provided
        /// </summary>
        /// <param name="name">The name of the sound effect to be played</param>
        void PlayEffect(string name);
        /// <summary>
        /// METHOD: Mutes all sound
        /// </summary>
        void Mute();

        bool isMuted { get; set; }
        /// <summary>
        /// METHOD: Unmutes all sound
        /// </summary>
        void unMute();
        /// <summary>
        /// METHOD: changes the volume of sound played
        /// </summary>
        /// <param name="Volume">The volume to be changed to</param>
        void Volume(float Volume);
        /// <summary>
        /// METHOD: Stops the current sound being played
        /// </summary>
        void Stop();
        /// <summary>
        /// METHOD: the update loop cycled through each frame
        /// </summary>
        /// <param name="gameTime">Monogame GameTime property</param>
        void Update(GameTime gameTime);
        /// <summary>
        /// METHOD: Increases the volume slightly
        /// </summary>
        void volUp();
        /// <summary>
        /// METHOD: Decreases the volume slightly
        /// </summary>
        void volDown();
        /// <summary>
        /// METHOD: Plays the sound of the screen that is being changed to
        /// </summary>
        /// <param name="screen">The screen being changed to</param>
        void onScreenChanged(BaseScreen screen);
    }
}