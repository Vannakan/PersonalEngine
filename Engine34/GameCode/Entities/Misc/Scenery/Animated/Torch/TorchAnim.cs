using Engine34.Entities.Animation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Engine.Interfaces.Resource;
using Engine.Managers.ServiceLocator;
using Engine34.Managers.Render;
using Engine.Managers.Render;
using Engine.Interfaces.Render;

namespace GameCode.Entities.Misc.Scenery.Animated.Torch
{
    public class TorchAnim : Animator, IAnimationState
    {
        Vector2 offset = new Vector2(-165, -135);
        LightMask x;
        bool lightset = false;
       
        public TorchAnim()
        {
            
            Texture2D a = Locator.Instance.getService<IResourceLoader>().GetTex("torchSheet");
            this.Initialize(a, 2, 3);
            this.FramesPerSecond = 5;
            this.isLooping = true;
            
        }
       
        public void Enter()
        {
  
        }

        public void Exit()
        {
            if (lightset) 
            Locator.Instance.getService<ILightMaskManager>().RemoveLightSource(x);

        }

        public void Handle(GameTime gt)
        {
            Update(gt);
            if (!lightset && currentPos.Length() != 0 )
            {
                x = new LightMask(currentPos + offset, Locator.Instance.getService<IResourceLoader>().GetTex("Grad"), 1.4f);
                Locator.Instance.getService<ILightMaskManager>().addMask(x);
                lightset = true;
            }
        }

        public void Handle(Vector2 p, SpriteBatch sp)
        {
            Draw(sp, p);
        }
    }
}
