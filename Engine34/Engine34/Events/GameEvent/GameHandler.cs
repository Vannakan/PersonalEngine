using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine34.Events.GameEvent
{
    class GameHandler
    {
        //Define a delegate
        public delegate void GameEventHandler(object sender, GameEventArgs e);

        //Define an event based on that delegate
        public event GameEventHandler EntitySpawned;
        public event GameEventHandler EntityDestroyed;
        public event GameEventHandler ActionCalled;
        public event GameEventHandler ActionExited;
        

        public GameHandler()
        {

        }

        public void Update(GameTime gameTime)
        {

        }
        //:D
    }
}
