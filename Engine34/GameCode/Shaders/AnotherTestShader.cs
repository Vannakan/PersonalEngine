using Engine;
using Engine.Interfaces.Render;
using Engine.Managers.ServiceLocator;
using Engine.Interfaces.Resource;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using Engine34.Interfaces.Render;

namespace GameCode.Shaders
{
    public class AnotherTestShader : Shader
    {

        public AnotherTestShader()
        {

            tex = Locator.Instance.getService<IResourceLoader>().GetTex("KingCholera");
            effect1 = Constants.cm.Load<Effect>("GBR");
        }

     

       
    }
}
