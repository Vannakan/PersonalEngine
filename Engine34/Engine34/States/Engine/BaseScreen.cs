using Engine.Interfaces.Entities;
using Engine.Interfaces.Screen;
using Engine.Managers.Entities;
using Engine.Managers.ServiceLocator;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Engine.States.Engine
{
    /// <summary>
    /// ABSTRACT CLASS: The base Screen which all levels and screens will extend from. This contains the properties and
    /// methods that all screens need
    /// </summary>
    public enum State
    {
        Entering,
        Running,
        Exiting
    }
    public abstract class BaseScreen : IScreen
    {
  /// <summary>
  /// GET: SET: bool for if screen is active screen
  /// </summary>
  public bool Active
        {
            get;
            set;
        }

        /// <summary>
        /// GET: SET: Does the screen have a soundtrack
        /// </summary>
        public string SoundTrack
        {
            get;
            set;
        }

 

  /// <summary>
  /// CONSTRUCTOR:
  /// </summary>
  public BaseScreen() { }

        //// <summary>
        //// METHOD:Initialization logic here (For child screens)
        //// </summary>
        public virtual void Initialize() { }

        /// <summary>
        /// METHOD: Unload the screen
        /// </summary>
        public virtual void UnloadContent() { }

     //// <summary>
     //// METHOD: Update the screen
     //// </summary>
     //// <param name="gameTime">Monogame GameTime Property</param>
     public virtual void Update(GameTime gameTime) { }

        //// <summary>
        //// METHOD: Draw the screen
        //// </summary>
        //// <param name="spriteBatch">Monogame SpriteBatch</param>
        public virtual void Draw(SpriteBatch spriteBatch) { }

  /// <summary>
  /// METHOD: When the screen is to be unloaded, clear any entities from the screen,
  /// remove the entities from the detection manager as well
  /// </summary>
  public virtual void Unload()
        {
            Locator.Instance.getService<IEntityManager>().clearList();
        }
    }
}