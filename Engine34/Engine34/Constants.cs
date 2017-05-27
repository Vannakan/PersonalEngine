using Engine.Managers.Cam;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Engine
{
    /// <summary>
    /// Debug Class
    /// </summary>
    public class Constants
    {
        public static ContentManager cm { get; set; }
        public static int ID = 0;
        public static GraphicsDevice g
        {
            get;
            set;
        }
        public static Vector2 ScreenCentre
        {
            get;
            set;
        }
        public static Camera cam
        {
            get;
            set;
        }

        public static SpriteBatch sp { get; set; }
        public static int TileSize = 64;
        public static Color colour = Color.Maroon;
        internal static bool Debug
        {
            get;
            set;
        }

        public static Random r
        {
            get;
            set;
        }


        public static bool isPaused
        {
            get;
            set;
        }

        public static SpriteBatch debugBatch
        {
            get;
            set;
        }

    }
}