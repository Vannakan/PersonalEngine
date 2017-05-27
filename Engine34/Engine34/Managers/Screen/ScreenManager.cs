﻿using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Engine.Managers.Sound;
using Engine.Interfaces.Screen;
using Engine.States.Engine;
using Engine.Interfaces.Entities;
using Engine.Managers.ServiceLocator;
using Engine.Managers.Input;
using Engine.Managers.Cam;
using Engine.Interfaces.Sound;
using Engine.Interfaces.Cam;
using Engine.Interfaces.InputManager;
using Engine.Events.KeyboardEvent;
using Engine34.States.Engine;

namespace Engine
{
    public class ScreenManager : IScreenManager
    {
        #region Variables
        //The Screenmanagers screen stack which contains all screens, the top most
        //screen is the only screen that will be drawn and updated
        //**FUTURE IMPLEMENTATION**
        //Control whether screens get drawn and updated based on properties
        //such as whether it is a pop up screen or not
        private Stack<BaseScreen> screenStack = new Stack<BaseScreen>();
        private Stack<PopupScreen> popScreenStack = new Stack<PopupScreen>();
        private List<BaseScreen> trashScreens = new List<BaseScreen>();


        public delegate void sManagerEvent(BaseScreen screen);

        public event sManagerEvent ScreenChange;



        #endregion
        #region Properties    
        //Reference to the Game1 content manager to load resources
        public ContentManager content { get; set; }

        public List<IEntity> EntityList { get; set; }
        #endregion
        #region Initialize & Unload

        /// <summary>
        /// Initialize the screenmanager and add the menu to the stack to start the game off.
        /// Soon this will be replaced with a splashscreen which will transition to the menu
        /// </summary>
        public void Initialize()
        {
            Locator.Instance.getService<KeyHandler>().KeyDown += OnKeyDown;
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.key == Microsoft.Xna.Framework.Input.Keys.Back)
                RemoveTopScreen();
        }

        /// <summary>
        /// Unload any screens which need to be unloaded
        /// </summary>
        public void UnloEnginecreens()
        {
            foreach (BaseScreen screen in trashScreens)
            {
                screen.UnloadContent();
            }
        }
        #endregion
        #region Update & Draw

        public void Update(GameTime gameTime)
        {
            CheckScreenManagerInput();
            UpdateTopScreen(gameTime);
            CheckScreens();
        }


        /// <summary>
        /// Currently draws the topmost screen, needs to be improved to support things such as pop ups
        /// It'll be a simple fix to check to see which screens are requesting to be drawn
        /// and draw them in the order of the stack.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {

            BaseScreen DrawScreen = screenStack.Peek();
            if(DrawScreen != null)
            DrawScreen.Draw(spriteBatch);

            if (popScreenStack.Count > 0)
            {
                PopupScreen DrawPop = popScreenStack.Peek();
                if (DrawPop != null)
                    DrawPop.Draw(spriteBatch);
            }

        }



        #endregion
        #region Add & Remove
        /// <summary>
        /// This method is self explanitory, it asks for the class name you wish to add to the screenstack and pushes 
        /// it onto the top. 
        /// </summary>
        /// <param name="screenName"></param>
        public void Add<T>(string ScreenName) where T : BaseScreen, new()
        {
            //Halt the sound manager
            Locator.Instance.getService<ISoundManager>().Stop();
            //Create the new requested screen and initialize it
            BaseScreen myScreen = new T();
            myScreen.Initialize();
            //Push the initializedj screen onto the screenstack
            screenStack.Push(myScreen);
            //Call the screen change method for subscribers
            onScreenChange(myScreen);


        }

        public void AddPop<T>(string ScreenName) where T : PopupScreen, new ()
        {
            PopupScreen myScreen = new T();
            myScreen.Initialize();
            myScreen.setBounds(Constants.g.Viewport.Width/2-250, Constants.g.Viewport.Height/ 2 - 175 , 500, 300);
            myScreen.setColour(Color.White);
            popScreenStack.Push(myScreen);
            
        }


        /// <summary>
        /// This method is similar to the add method, but rather than just removing the screen to go back to the 
        /// previous screen, this method replaces a screen (If you wanted to replace level1 with level2
        /// </summary>
        /// <param name="screenName"></param>
        public void ReplaceScreen<T>(string ScreenName) where T : BaseScreen, new()
        {
            //Take the top screen off the stack
            RemoveTopScreen();
            //Add the replacement screen
            Add<T>(ScreenName);
            //Call the screen change event to notify subscribers
            onScreenChange(screenStack.Peek());
        }





        /// <summary>
        /// Checks each of the screens in the screenstack and checks if its the same screen as the current screen
        /// if so then declare the screen active, ready for drawing and updating.
        /// 
        /// TO DO
        /// 
        /// Give screens individual IDs just like entities.
        /// </summary>
        public void CheckScreens()
        {


            foreach (BaseScreen screen in screenStack)
            {
                if (screen == screenStack.Peek())
                {
                    // Console.WriteLine(screen.GetType());
                    screen.Active = true;
                }
                else
                    screen.Active = false;
            }
        }

        /// <summary>
        /// Checks for input, more specifically if the ESCAPE key has been pressed and if the screen count
        /// is more than 1, go back a screen. Otherwise there will be no screens present and the screenmanager
        /// will be broken.
        /// </summary>
        public void CheckScreenManagerInput()
        {

            if (Locator.Instance.getService<IInputManager>().CheckKeyPressed(Microsoft.Xna.Framework.Input.Keys.Escape))
            {
                //Make sure we don't delete the menu!
                if (screenStack.Count > 1)
                {
                    RemoveTopScreen();
                }
            }
        }


        /// <summary>
        /// Removes the top screen from the stack. Should be used in situations such as if
        /// the 
        /// er has lost all of his lives and the state should be reverted back to the menu screen
        /// this will be your god. It basically goes back one screen :)
        /// </summary>
        public void RemoveTopScreen()
        {
            IScreen screen = screenStack.Peek();
            screen.Unload();
            screenStack.Pop();
            Locator.Instance.getService<ICameraManager>().getCam().reset();

            BaseScreen myScreen = screenStack.Peek();


            onScreenChange(myScreen);


        }

        public IScreen getTopScreen()
        {
            return screenStack.Peek();
        }



        /// <summary>
        /// This is probably the worst way of going about it but this method checks the top screen and updates it
        /// Although this will not be relevant soon since screen updates will be decided through ENUMS.
        /// </summary>
        /// <param name="gameTime"></param>

        public void UpdateTopScreen(GameTime gameTime)
        {
            BaseScreen updateScreen = screenStack.Peek();
            updateScreen.Update(gameTime);

        }

        #endregion
        #region EventRelated
        protected virtual void onScreenChange(BaseScreen screen)
        {
            if (ScreenChange != null)
            {
                ScreenChange(screen);
            }
        }
        #endregion
    }

   
}
