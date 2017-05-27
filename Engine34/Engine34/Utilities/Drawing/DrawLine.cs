using Engine.Utilities.Shapes;
using Engine.Managers.Render;
using Microsoft.Xna.Framework;
using Engine.Managers.ServiceLocator;
using Engine.Interfaces.Render;

namespace Engine.Utilities
{
    static class DrawLine
    {

        public static Line newLine(Vector2 start, Vector2 end, Color color)
        {
            Line line = new Line();
            line.newLine(start, end, color);
            Locator.Instance.getService<IRenderManager>().addShape(line);

            return line;
        }


    }
}