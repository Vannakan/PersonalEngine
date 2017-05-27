using Engine.Interfaces.ServiceLocator;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Engine.Events.KeyboardEvent
{
    //Read the bottom if you dont know how to make an event
    public class KeyHandler : IUpdService
    {
        //Define a delegate
        public delegate void KeyEventHandler(object sender, KeyEventArgs e);

        //Define an event based on that delegate
        public event KeyEventHandler KeyDown;
        public event KeyEventHandler KeyHeld;
        //:D

        private static KeyHandler instance;
        public static KeyHandler Instance
        {
            get
            {
                if (instance == null)
                    instance = new KeyHandler();
                return instance;
            }
        }
        //An array of type Keys that the handler will run through
        private Keys[] keys = { };
        private KeyboardState prev, current;

        private Keys[] heldKeys = { };

        public void Update(GameTime gameTime)
        {
            prev = current;
            current = Keyboard.GetState();
            keys = current.GetPressedKeys();
            foreach (Keys key in keys)
            {
                if (current.IsKeyDown(key) && prev.IsKeyUp(key))
                {
                    OnKeyPressed(current, key);
                }

                if (current.IsKeyDown(key) && prev.IsKeyDown(key))
                {
                    OnKeyHeld(current, key);
                }
            }

        }

        protected virtual void OnKeyPressed(KeyboardState m, Keys k)
        {
            if (KeyDown != null)
            {
                KeyDown(this, new KeyEventArgs()
                {
                    keyState = m,
                    key = k
                });
            }
        }

        protected virtual void OnKeyHeld(KeyboardState m, Keys k)
        {
            if (KeyHeld != null)
            {
                KeyHeld(this, new KeyEventArgs()
                {
                    keyState = m,
                    key = k
                });
            }
        }

    }

}