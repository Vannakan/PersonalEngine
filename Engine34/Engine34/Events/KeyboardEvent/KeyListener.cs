﻿using System;

namespace Engine.Events.KeyboardEvent
{
    public class KeyListener
    {

        public void OnKeyDown(object sender, KeyEventArgs e)
        {
            Console.WriteLine("Key Event fired, KEY PRESSED : " + e.key);
        }
    }
}