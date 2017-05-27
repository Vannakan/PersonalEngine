using Engine.Interfaces.Cam;
using Engine.Interfaces.Resource;
using Engine.Managers.Cam;
using Engine.Managers.ServiceLocator;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameCode.Screens.Menu
{
    /// <summary>
    /// CLASS: MenuOption contains the data and logic for creating new options to be highlighted on a menu
    /// </summary>
    public class MenuOption
    {
        /// <summary>
        /// DECLARE: The position, texture and centre of the screen
        /// </summary>
        private Vector2 position;
        private Vector2 _texCenter;
        private Vector2 _screenCenter = Locator.Instance.getService<ICameraManager>().getWorldPosition(new Vector2(Game1.Instance.graphics.PreferredBackBufferWidth / 2, Game1.Instance.graphics.PreferredBackBufferHeight / 2));

        /// <summary>
        /// GET: SET: If this option is currently selected
        /// </summary>
        private bool selected = false;
        public bool IsSelected
        {
            get
            {
                return selected;
            }
            set
            {
                selected = value;
            }
        }

        /// <summary>
        /// GET: SET: The name of this option
        /// </summary>
        private string name = "";
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        /// <summary>
        /// GET: SET: The index of this option in the current list of options
        /// </summary>
        private int index;
        public int Index
        {
            get
            {
                return index;
            }
            set
            {
                index = value;
            }
        }

        /// <summary>
        /// SET: The scale of the option
        /// </summary>
        private float scale = 1.5f;

  /// <summary>
  /// GET: The spritefont for this option
  /// </summary>
  private SpriteFont sf = Locator.Instance.getService<IResourceLoader>().GetFont("mFont");

        /// <summary>
        /// SET: The width and height of the space the Menu option takes
        /// </summary>
        int width = Game1.Instance.GraphicsDevice.Viewport.Width;
        int height = Game1.Instance.GraphicsDevice.Viewport.Height;
   
  /// <summary>
  /// CONSTRUCTOR
  /// </summary>
  /// <param name="_name">The name of the options</param>
  /// <param name="offset">The offset of the option from the previous option when this is created</param>
  /// <param name="_index"> The index of this option in the list of options when this is created</param>
        public MenuOption(string _name, float offset, int _index)
        {

        }
        /// <summary>
        /// METHOD: Draws the menu Option
        /// </summary>
        public void Draw(SpriteBatch spriteBatch)
        {
            Color color = Color.Black;
            if (IsSelected)
            {
                color = Color.DarkRed;
                spriteBatch.DrawString(sf, Name, _screenCenter, color, 0, new Vector2(_texCenter.X / 7, _texCenter.Y / 2), scale + 0.25f, SpriteEffects.None, 0.5f);
            }
            else spriteBatch.DrawString(sf, Name, _screenCenter, color, 0, new Vector2(_texCenter.X / 7, _texCenter.Y / 2), scale, SpriteEffects.None, 0.5f);

        }
    }
}