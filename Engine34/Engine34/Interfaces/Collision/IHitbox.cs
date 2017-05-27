using Engine.Interfaces.Entities;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Engine34.Interfaces.Collision
{
    /// <summary>
    /// INTERFACE: The interface for all hitboxes used for SAT Collision
    /// </summary>
    public interface IHitbox
    {

        List<Vector2> Points { get; set; }
        List<Vector2> Edges { get; set; }
        Vector2 Velocity { get; set; }
        IMind Mind { get; set; }
        void createMatrix();
        Vector2 createRotation(Vector2 _point);
        void CreateEdges();
        void UpdatePoint(Vector2 velocity);
        Vector2 centrePoint();
        void Update();
        Vector2 Centre { get; set; }
    }
}
