using Engine.Entities;
using Engine.Interfaces.Behaviour;
using Engine.Managers.Behaviour;
using Engine.Managers.ServiceLocator;
using Microsoft.Xna.Framework;

namespace GameCode.Entities.Steering
{
    class SteeringEntity : Entity
    {

        public override void Initialize(Vector2 Pos)
        {
            mind = Locator.Instance.getService<IBehaviourManager>().Create<SteeringMind>(this);
            Name = "Steering";

            base.Initialize(Pos);
        }
    }
}