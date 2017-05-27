using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameCode.Gui.Items
{
    abstract class GuiComponent
    {
        protected Vector2 Position;
        protected Rectangle Bounds;
        public string Tag;

        protected Dictionary<string, string> stringValues = new Dictionary<string, string>();
        protected Dictionary<string, int> numValues = new Dictionary<string, int>();

        public virtual void Initialize(Vector2 pos, Rectangle bou, string t)
        {
            Position = pos;
            Bounds = bou;
            Tag = t;
        }
        public virtual void Draw(SpriteBatch spr)
        {

        }

        public virtual void Update(GameTime gt)
        {

        }
            
    }
}
