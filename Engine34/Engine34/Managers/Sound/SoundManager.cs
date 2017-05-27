using Microsoft.Xna.Framework.Media;
using System;
using Microsoft.Xna.Framework;
using Engine.Interfaces.Sound;
using Engine.Managers.ServiceLocator;
using Engine.Interfaces.Resource;
using Engine.Managers.Screen;
using Engine.Interfaces.Screen;
using Engine.States.Engine;
using Microsoft.Xna.Framework.Audio;

namespace Engine.Managers.Sound
{
    /// <summary>
    /// CLASS: The sound manager is responsible for the storage and playback of sound files
    /// </summary>
    public class SoundManager : ISoundManager
    {
        /// DECALRE: bool for if playback is muted
        private bool IsMuted = false;
        public bool isMuted { get { return IsMuted; } set { IsMuted = value; } }


        /// DECLARE: Volume sound plays at by default
        float defaultVolume = 0.01f;

  /// <summary>
  /// METHOD: Initialise the Monogame MediaPlayer and ScreenManager
  /// </summary>
  public void Initialize()
        {
            MediaPlayer.Volume = defaultVolume;
            ScreenManager a = Locator.Instance.getService<IScreenManager>() as ScreenManager;
            a.ScreenChange += onScreenChanged;

        }
        /// <summary>
        /// METHOD: Play a song that is located within the resource loaders song library
        /// </summary>
        /// <param name="name">Name of the sound file to be played</param>
        public void Play(string name)
        {
            MediaPlayer.Play(Locator.Instance.getService<IResourceLoader>().GetSong(name));
        }

        //// <summary>
        //// METHOD:Play a sound effect
        //// </summary>
        //// <param name="name">The name of the sound effect to be played</param>
        public void PlayEffect(string name)
        {
            SoundEffect ef = Locator.Instance.getService<IResourceLoader>().GetSound(name);
                if(ef!= null)
            ef.Play();
        }

        //// <summary>
        //// METHOD: Mute the Sound Manager
        //// </summary>
        public void Mute()
        {
            if (!isMuted)
            {
                isMuted = true;
                defaultVolume = 0;

            }
        }

        public void PlaySoundEffect(string sound)
        {
            SoundEffect sounde = Locator.Instance.getService<IResourceLoader>().GetSound(sound);
            sounde.Play();
        }
        //// <summary>
        //// METHOD: Unmute the sound manager
        //// </summary>
        public void unMute()
        {
            if (isMuted)
            {
                isMuted = false;
                defaultVolume = 0.05f;
            }
        }

        /// <summary>
        /// METHOD: Adjust the volume of sound playback
        /// </summary>
        /// <param name="Volume">The volume the playback will be set to play at</param>
        public void Volume(float Volume)
        {
            defaultVolume = Volume;
        }

        /// <summary>
        /// METHOD: Stop playback
        /// </summary>
        public void Stop()
        {
            MediaPlayer.Stop();
        }

        //// <summary>
        //// Method: Plays screens songs.
        //// </summary>
        public void Update(GameTime gameTime)
        {
            if (MediaPlayer.Volume != defaultVolume)
                MediaPlayer.Volume = defaultVolume;
        }

        //// <summary>
        //// Method: Turn the Volume Up
        //// </summary>
        public void volUp()
        {
            if (defaultVolume <= 1)
                defaultVolume += 0.02f;
        }

        //// <summary>
        //// Method: Turn the Volume Down
        //// </summary>
        public void volDown()
        {
            if (defaultVolume >= 0.1f)
    defaultVolume -= 0.02f;
        }



        //// <summary>
        //// METHOD: Play the soundtrack for the new screen when the screen is changed
        //// </summary>
        //// <param name="screen"></param>
        public void onScreenChanged(BaseScreen screen)
        {
            if (screen.SoundTrack != null)
                Play(screen.SoundTrack);
        }
    }
}