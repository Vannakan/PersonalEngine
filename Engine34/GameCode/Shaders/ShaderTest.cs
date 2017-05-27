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
    public class ShaderTest : Shader
    {

        int counter = 0;
        public ShaderTest()
        {

            tex = Locator.Instance.getService<IResourceLoader>().GetTex("KingCholera");
            effect1 = Constants.cm.Load<Effect>("Negative");
        }

        public override void Draw(SpriteBatch sp)
        {
            Update();
            if (counter > 100)
                effect1.CurrentTechnique.Passes[0].Apply();
            if (counter > 110)
                counter = 0;
            sp.Draw(tex, Vector2.Zero, Color.White);

        }

        public void Update()
        {
            counter++;
        }
    }
}
