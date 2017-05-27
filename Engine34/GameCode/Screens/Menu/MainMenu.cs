using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Engine.Events.KeyboardEvent;
using Engine.Managers.Cam;
using Engine.States.Engine;
using Engine.Managers.ServiceLocator;
using Engine.Interfaces.Resource;
using Engine.Interfaces.Screen;
using Engine.Managers.Screen;
using Engine.Interfaces.Cam;
using Engine.Managers.Resource;
using GameCode.Screens.Menu;
using GameCode.Screens.Levels;

namespace GameCode
{
    /// <summary>
    /// CLASS: MainMenu, this is the class that is loaded straight after the splash screen is used to access the majority
    /// of other screens in the program
    /// </summary>
    class MainMenu : BaseScreen
    {
        /// <summary>
        /// DECLARE: Timer for the transition of the screen
        /// </summary>
        int timer = 0;
     ///DECLARE: An index integer for the menus
  private int Selected;
        ///DECLARE: A list of the menu item names
        List<string> menuNames = new List<string>();
        ///DECLARE: Offset that is added to the Y position of the menu item when it is drawn
        float yOffset = -100;
        ///DECLARE: List of the menu items
        List<MenuItem> MenuItems;
        ///DECLARE: Fader
        Fader f;

     //// <summary>
     //// CONSTRUCTOR:
     //// Set the Selected Index to 1 so that there is always something selected
     //// Create a list of menu items
     //// </summary>
     public MainMenu()
        {
            Selected = 1;
            MenuItems = new List<MenuItem>();
        }

        //// <summary>
        //// METHOD: Initializes items into the string menu list and based on how many entries
        //// relevant items are then created and added to the Item list (names are taken from the string list)
        //// </summary>
        public override void Initialize()
        {
            ///INITIALISE: The fader DECLARED earlier
            f = new Fader(new Vector2(1250, 800), new Vector2(Locator.Instance.getService<ICameraManager>().getWorldPosition(new Vector2(0, 0)).X, Locator.Instance.getService<ICameraManager>().getWorldPosition(new Vector2(0, 0)).Y), 0.005f);

            ///SET: Soundtrack
            SoundTrack = "SoundTrack3";
            ///EVENT: Add Keydown event to the screen
            Locator.Instance.getService<KeyHandler>().KeyDown += OnKeyDown;
            ///ADD: Different possible selections to the menu
            menuNames.Add("Ward");
            menuNames.Add("Dungeon");
            menuNames.Add("Shaders");

            menuNames.Add("Exit");

            ///FOR: Each item in the MenuNames: Move down the screen and draw the strings
            for (int i = 0; i < menuNames.Count; i++)
            {
                yOffset += 75;
                MenuItem item = new MenuItem(menuNames[i],
                 yOffset,
                 Locator.Instance.getService<IResourceLoader>().GetFont("mFont"),
                 i + 1, Locator.Instance.getService<IResourceLoader>().GetTex("Menu3"));
                MenuItems.Add(item);
            }
            base.Initialize();

        }

     //// <summary>
     //// METHOD: Draws all menu items as well as the Main Menu font
     //// </summary>
     //// <param name="spriteBatch"></param>
     public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Locator.Instance.getService<IResourceLoader>().GetTex("MedicationBackground"), Locator.Instance.getService<ICameraManager>().getWorldPosition(Vector2.Zero), Color.White);
            spriteBatch.Draw(Locator.Instance.getService<IResourceLoader>().GetTex("MedicationLogo"), Locator.Instance.getService<ICameraManager>().getWorldPosition(new Vector2(290, Game1.Instance.graphics.PreferredBackBufferHeight / 5)), Color.White);
            foreach (MenuItem item in MenuItems)
            {
                item.Draw(spriteBatch);
            }

            f.Draw(spriteBatch);

            base.Draw(spriteBatch);
        }

        //// <summary>
        //// METHOD: Initialize Menu Input THEN menu items based on if they're selected or not
        //// </summary>
        //// <param name="gameTime"></param>
        //// 
        public override void Update(GameTime gameTime)
        {
            f.Update();
            CheckLimits();
            MenuSelection();
            timer++;



            base.Update(gameTime);
        }

        //// <summary>
        //// METHOD: A safety method to avoid removing the menu screen.
        //// </summary>

        public void CheckLimits()
        {
            if (Selected > MenuItems.Count)
            {
                Selected = MenuItems.Count;
            }
            else if (Selected < 1)
            {
                Selected = 1;
            }
        }

        //// <summary>
        //// EVENT: Listens to the KeyHandler and produces various functionalities based on the pressed key that has been
        //// returned
        //// will only apply the function if the screen is active (It's at the top of the stack)
        //// </summary>
        //// <param name="sender"></param>
        //// <param name="e"></param>
        public void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (Active)
            {
                if (e.keyState.IsKeyDown(Keys.Up))
                {
                    Selected--;
                }
                if (e.keyState.IsKeyDown(Keys.Down))
                {
                    Selected++;
                }

                if (e.keyState.IsKeyDown(Keys.Enter))
                {
                    switch (Selected)
                    {

                        case 1:
                            Locator.Instance.getService<IScreenManager>().Add<Ward>("Ward");
                            break;
                        case 2:
                            Locator.Instance.getService<IScreenManager>().Add<Dungeon1>("Dung");
                            break;
                            
                        case 3:
                            Locator.Instance.getService<IScreenManager>().Add<ShaderDemo>("Shader");

                            break;
                        case 4:
                            Game1.Instance.Exit();

                            break;




                    }
                }
            }

        }

        //// <summary>
        //// METHOD: Checks the menu items and checks if the current menu index matches them, then we can check if both indexes
        //// match then the item is currently selected and if enter is pressed, make an action.
        //// 
        //// </summary>
        public void MenuSelection()
        {

            foreach (MenuItem item in MenuItems)
            {
                if (Selected == item.Index)
                {
                    item.IsSelected = true;
                }
                else item.IsSelected = false;
            }
        }
    }
}