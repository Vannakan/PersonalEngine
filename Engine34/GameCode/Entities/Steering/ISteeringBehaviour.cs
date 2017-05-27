using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCode.Entities.Steering
{
    interface ISteeringBehaviour
    {
        Vector2[] ApplyBehaviour();
    }
}