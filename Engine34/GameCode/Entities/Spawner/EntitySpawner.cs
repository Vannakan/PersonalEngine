using Engine.Entities;
using Engine.Grid;
using Engine.Interfaces.Behaviour;
using Engine.Interfaces.Entities;
using Engine.Interfaces.Screen;
using Engine.Managers.ServiceLocator;
using Engine.States.Engine;
using Engine34.Utilities.Shapes;
using GameCode.Entities.Enemies.Boss;
using GameCode.Entities.Platformer;
using GameCode.Entities.Steering;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameCode.Entities.Spawner
{
    class EntitySpawner
    {
    
        //Radius for spawning
        Circle radius;
        //Position of the spawner
        Vector2 position;
        //Entity Group for spawner
        SpawnGroup spawns;

        BaseScreen screen;

        Grids searchSpace;

        public EntitySpawner(int Radius, Vector2 Position)
        {
            position = Position;
            radius = new Circle(Radius, Position);
        }

        public EntitySpawner(int Radius, Vector2 Position,BaseScreen scr)
        {
            position = Position;
            radius = new Circle(Radius, Position);
            screen = scr;
        }


        public void randomWave()
        {
            int x = 10, y = 10;
            Node[,] ss = null;

            if(isSuitableSpawn(x,y,ss))
            {
                for (int xx = x-3; xx < ss.GetLength(0); xx++)
                {
                    for (int yy = y - 3; yy < ss.GetLength(1); yy++)
                    {
                        Vector2 pos = radius.randomPos();
                        Rectangle checker = new Rectangle((int)pos.X, (int)pos.Y, 64, 64);
                      //  if(!checker.Intersects(ss[xx,yy].Bounds))
                            //spawn entity
                    }
                }
            }
        }

     
        bool isSuitableSpawn(int x, int y, Node[,] ss)
        {

            for (int i = 0; i < ss.GetLength(0); i++)
            {
                for (int j = 0; j < ss.GetLength(1); j++)
                {
                    if (GetSurroundingWallCount(x, y, ss, ss.GetLength(0), ss.GetLength(1)) >= 4)
                        return true;
                }
            }


            return false;
        }


        public int GetSurroundingWallCount(int x, int y, Node[,] ss,int width, int height)
        {
            int wallCount = 0;
            int mapWidth = width;
            int mapHeight = height;
            Node[,] searchSpace = ss;

            for (int neighbourX = -1; neighbourX < 2; neighbourX++)
            {

                for (int neighbourY = -1; neighbourY < 2; neighbourY++)
                {

                    int neighX = neighbourX + x;
                    int neighY = neighbourY + y;

                    if (neighX == 0 && neighY == 0)
                    {
                    }

                    //Make sure in bounds of index && Check for walls

                    if (neighX >= 0 && neighX < mapWidth && neighY >= 0 && neighY < mapHeight)
                    {

                        if (neighbourX != x || neighbourY != y)
                        {

                            if (!searchSpace[neighX, neighY].Blocked)
                            {
                                wallCount++; ;
                            }

                        }


                    }
                    else if (neighX < 0 || neighY < 0 || neighX >= mapWidth || neighY >= mapHeight)
                    {
                        wallCount++;
                    }



                }

            }
            return wallCount;
        }


        /// <summary>
        /// Assigns a group of types to be spawned by the spawner
        /// </summary>
        /// <param name="m"></param>
        public void assignGroup(SpawnGroup m)
        {
            spawns = m;
        }

        public void sendBoss(int amount, bool otherEntities, int eAmount)
        {
            for (int i = 0; i < amount; i++)
            {
                foreach (Entity e in spawns.bossList)
                {
                    switch (e.ToString().Split('.').Last())
                    {
                        case "Boss1":
                            {
                                int x = Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<Boss1>(radius.randomPos()).UniqueID;
                             
                            }
                            break;
                    }


                }

             
            }
            if (otherEntities)
                sendWave(eAmount);
        }


        //Send a wave of a predefined amount of Entities in a random radius
        public void sendWave(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                foreach (Entity e in spawns.getList)
                {

                    switch (e.ToString().Split('.').Last())
                    {
                        case "TestEntity":
                            {
                               int x = Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<TestEntity>(radius.randomPos()).UniqueID;
                          
                            }
                            break;
                        case "SteeringEntity":
                            {
                                int x = Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<SteeringEntity>(radius.randomPos()).UniqueID;
                              
                            }

                            break;

                    }
                }

            }

        }


    }
}