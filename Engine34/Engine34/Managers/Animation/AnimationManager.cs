using Engine.Interfaces.Render;
using Engine.Interfaces.ServiceLocator;
using Engine.Managers.ServiceLocator;
using Engine34.Entities;
using Engine34.Entities.Animation;
using Engine34.Interfaces.Animation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine34.Managers.Animation
{
    public class AnimationManager : IAnimationManager
    {
        public List<IAnimation> AnimList = new List<IAnimation>();

        public AnimationManager()
        {

        }

        public void Initialize()
        {

        }

        public void addAnim(IAnimation set)
        {
            if (set != null)
            {
                AnimList.Add(set);
                Locator.Instance.getService<IRenderManager>().addCamDrawable(set as IDrawableComponent);
            }
            else throw new Exception("ANIMATION SET NULL");
        }

        public void removeAnim(IAnimation set)
        {
            for (int i = 0; i < AnimList.Count; i++)
            {
                if (AnimList[i] == set)
                {
                    Locator.Instance.getService<IRenderManager>().Remove(AnimList[i] as IDrawableComponent);
                    AnimList[i].Unload();
                    AnimList.RemoveAt(i);

                }
            }
        }

        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < AnimList.Count; i++)
            {
                if(AnimList[i].hasLifecycle && AnimList[i].LifecycleCounter <= 0)
                {
                        removeAnim(AnimList[i]);
                }
                else
                AnimList[i].Update(gameTime);

            }
        }

        public void Draw(SpriteBatch spr)
        {
            for (int i = 0; i < AnimList.Count; i++)
            {
                AnimList[i].Draw(spr);
            }
        }
    }
}
