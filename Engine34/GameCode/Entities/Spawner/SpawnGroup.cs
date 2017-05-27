using Engine.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameCode.Entities.Spawner
{
    public abstract  class SpawnGroup
    {
        protected List<Entity> spawnTypes = new List<Entity>();
        protected List<Entity> bossTypes = new List<Entity>();
        protected void addType<T>() where T : Entity, new()
        {
            Entity a = new T();
            spawnTypes.Add(a);
        }

        protected void addBossType<T>() where T : Entity, new()
        {
            Entity a = new T();
            bossTypes.Add(a);
        }


        public List<Entity> bossList { get { return bossTypes; } }
        public List<Entity> getList { get { return spawnTypes; } }
    }
}
