using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Engine.Managers.ServiceLocator;
using Engine;
using Engine.Interfaces.Resource;

namespace GameCode.Shaders
{
    public class LightShader : Shader
    {
        public LightShader()
        {
            tex = Locator.Instance.getService<IResourceLoader>().GetTex("Rad");
            effect1 = Constants.cm.Load<Effect>("TestShader");
        }
      
    }
}
