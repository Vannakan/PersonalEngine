using Engine34.Entities.Props.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Engine34.Entities.Animation;

namespace Engine34.Entities.Props.Dynamic
{
    abstract class DynamicProp : Prop
    {
        protected AnimationSet animSet;
        protected Vector2 offSet;

        public virtual void Update(GameTime gameTime)
        {
        
        }
    }
}
