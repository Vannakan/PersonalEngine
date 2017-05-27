using Engine.Interfaces.Entities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine34.Entities.Behaviour
{
   public abstract class Behaviour
    {
        protected IMind possessed;
        public virtual void Initialize() { }
        public virtual void Update(GameTime gameTime) { if (Active) DoBehaviour(); }
        public bool Active { get; set; }
        public virtual void DoBehaviour() { }
        
        
    }
}
