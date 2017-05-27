using Engine.Interfaces.Resource;
using Engine.Managers.ServiceLocator;
using Engine34.Entities.Animation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameCode.Entities.Misc.Scenery.Animated.Explosion1
{
    class Explosion1Anim : Animator, IAnimationState
    {

        public Explosion1Anim()
        {
            Texture2D a = Locator.Instance.getService<IResourceLoader>().GetTex("explosion1sheet");
            this.Initialize(a, 1, 5);
            this.FramesPerSecond = 5;
            this.isLooping = true;
        }
        public void Enter()
        {
        }

        public void Exit()
        {
        }

        public void Handle(GameTime gt)
        {
            Update(gt);
        }

        public void Handle(Vector2 p, SpriteBatch sp)
        {
            Draw(sp, p);
        }



    }
}
