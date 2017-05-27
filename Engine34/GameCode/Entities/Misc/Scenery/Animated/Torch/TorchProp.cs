using Engine.Interfaces.Resource;
using Engine.Managers.Render;
using Engine.Managers.ServiceLocator;
using Engine34.Entities.Animation;
using Engine34.Interfaces.Animation;
using Engine34.Managers.Render;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameCode.Entities.Misc.Scenery.Animated.Torch
{
    class TorchProp 
    {
        AnimationSet animSet;
    

        public void Initialize(Vector2 Position)
        {
   

            animSet = new AnimationSet();
            animSet.Initialize(new TorchAnimationTable(),Position);
            animSet.Event = 1;
            animSet.hasLifecycle = false;
            animSet.LifecycleCounter = 100;
            Locator.Instance.getService<IAnimationManager>().addAnim(animSet);

            

        }

        public void Unload()
        {
            animSet.Unload();
        }

        public void Update(GameTime gt)
        {
           // animSet.Update(gt);
        }

        public void Draw(SpriteBatch sb)
        {
          //  animSet.Draw(sb);
        }

       

      
    }
}
