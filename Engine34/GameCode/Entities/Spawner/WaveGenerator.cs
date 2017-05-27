using Engine.Interfaces.Entities;
using Engine.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameCode.Entities.Spawner
{
    public class WaveGenerator
    {
        public List<IEntity> currentWave = new List<IEntity>();
        int waveTime = 0;
        int counter = 0;
        int waveNumber = 0;
        int maxWaves;
        int bossEvery = 2;
        bool paused = true;
        int pausedTimer = 0;
        int pauseTime = 0;
        private EntitySpawner spawner;
        private SpawnGroup entities;
        int difficulty = 1;

        public WaveGenerator()
        {
            spawner = new EntitySpawner(1500, new Microsoft.Xna.Framework.Vector2(500, 500));
        }

        public void Begin()
        {
            
            difficulty = 1;
            spawner.sendWave(2 * difficulty);
            paused = false;
        }
           
        public void setSpawnGroup(SpawnGroup spawn)
        {
            entities = spawn;
            spawner.assignGroup(spawn);
        }
        
        public void setDifficulty(int val)
        {
            difficulty = val;
        }
        public void generateWave()
        {

        } 

        public void setWaves(int val)
        {
            maxWaves = val;
        }

        public void setWaveTime(int Timer)
        {
            waveTime = Timer;
        }

        public void pause(int timer)
        {
            pauseTime = timer;
        }

        public void Update()
        {
            counter++;
          
            if (counter >waveTime)
            {
                if (difficulty < 10)
                    difficulty++;
                counter = 0;
                if (difficulty % 5 == 0)
                    spawner.sendBoss(1, true, 2 * difficulty);
                else if   (difficulty % 10 == 0)
                    spawner.sendBoss(2, true, 2 * difficulty);
                else
                    spawner.sendWave(2 * difficulty);
            }
        }
    }
}
