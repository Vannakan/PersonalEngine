using Engine.Grid;
using Engine.Interfaces.Collision;
using Engine.Managers.Collision;
using Engine.Managers.ServiceLocator;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine.Grid
{
    /// <summary>
    /// Creates and returns a grid that has had a Cellular Automata algorithm applied to it
    /// </summary>
    public class CellularAutomata
    {
        //The grid which we will be making modifications to
        public Grids grid;

        //Percentage of the map we want filled in
        int fillPercent = 41;

        int mapWidth;

        int mapHeight;

        //Hashcode
        string seed = "";



        bool useRandomSeed = false;

        bool EntitySpawned = false;

        public void RandomFill()
        {
            seed = System.DateTime.Now.TimeOfDay.ToString();
            System.Random random = new System.Random(seed.GetHashCode());// (seed.GetHashCode());
            for (int x = 0; x < mapWidth; x++)

            {

                for (int y = 0; y < mapHeight; y++)

                {

                    if (x == 0 || x == mapWidth - 1 || y == 0 || y == mapHeight - 1)

                    {

                        grid.getGrid[x, y].Blocked = true;

                    }

                    else

                    {

                        int result = (random.Next(0, 100) < fillPercent) ? 1 : 0;
                        if (result == 1)
                        {
                            grid.getGrid[x, y].Blocked = true;

                        }
                        else if (result == 0)
                        {
                            grid.getGrid[x, y].Blocked = false;
                        }


                    }

                }

            }
        }

        public void ColorMap()
        {
            for (int x = 0; x < mapWidth; x++)

            {
                for (int y = 0; y < mapHeight; y++)
                {
                    if (grid.getGrid[x, y].Blocked)
                    {
                        grid.getGrid[x, y].Color = Color.Black;
                    }
                    if (grid.getGrid[x, y].Blocked == false)
                        grid.getGrid[x, y].Color = Color.White;
                }
            }

        }
        public void SmoothMap()
        {
            for (int x = 0; x < mapWidth; x++)

            {

                for (int y = 0; y < mapHeight; y++)

                {
                    int neighbouringWalls = GetSurroundingWallCount(x, y);
                    if (neighbouringWalls > 4)
                    {
                        grid.getGrid[x, y].Blocked = true;

                    }
                    else if (neighbouringWalls < 4)
                    {
                        grid.getGrid[x, y].Blocked = false;

                    }

                }
            }


        }

        int GetSurroundingWallCount(int x, int y)
        {
            int wallCount = 0;


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

                            if (grid.getGrid[neighX, neighY].Blocked)
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
        public void Initialize()
        {
            mapWidth = grid.getGrid.GetLength(0);
            mapHeight = grid.getGrid.GetLength(1);
        }

        public Grids GenerateCA(Grids grid)
        {

            this.grid = grid;

            Initialize();

            //Generate a random map
            RandomFill();

            //Smooth the map and create caverns
            for (int i = 0; i < 1; i++)

            {

                SmoothMap();

            }
            ColorMap();

            return grid;

        }
    }
}