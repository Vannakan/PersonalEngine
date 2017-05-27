using GameCode.Entities.Enemies.Boss;
using GameCode.Entities.Platformer;
using GameCode.Entities.Steering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameCode.Entities.Spawner
{
    class PrototypeSpawn : SpawnGroup
    {
        public PrototypeSpawn()
        {
            addType<SteeringEntity>();
            addBossType<Boss1>(); 
        }
    }
}
