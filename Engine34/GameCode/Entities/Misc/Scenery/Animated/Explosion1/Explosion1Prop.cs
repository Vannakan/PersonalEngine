using Engine.Interfaces.Resource;
using Engine.Managers.Render;
using Engine.Managers.ServiceLocator;
using Engine34.Entities.Animation;
using Engine34.Managers.Animation;
using Engine34.Managers.Render;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameCode.Entities.Misc.Scenery.Animated.Explosion1
{
    class Explosion1Prop
    {
        AnimationSet animSet;
        Vector2 offset = new Vector2(-75, -55);
        LightMask x;

        public void Initialize(Vector2 Position)
        {
            x = new LightMask(Position + offset, Locator.Instance.getService<IResourceLoader>().GetTex("Grad"), 0.7f);
            Locator.Instance.getService<ILightMaskManager>().addMask(x);

            animSet = new AnimationSet();
            animSet.Initialize(new Explosion1AnimTable(),Position);
            animSet.Event = 1;
            Locator.Instance.getService<AnimationManager>().addAnim(animSet);

        }
    }
}
