using Engine.Grid;
using Engine.Interfaces.Collision;
using Engine.Interfaces.Render;
using Engine.Managers.Collision;
using Engine.Managers.ServiceLocator;
using Engine.Utility;
using Engine34.Grid;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameCode.MapGeneration
{

    class CA
    {

        int maxCavernSize = 15;
        //The grid which we will be making modifications to
        public Grids grid;

        List<Node> LandNodes = new List<Node>();
        List<Node> WallNodes = new List<Node>();

        //Percentage of the map we want filled in
        int fillPercent = 45;

        static List<List<Node>> Caverns = new List<List<Node>>();

        int mapWidth;

        int mapHeight;

        //Hashcode
        string seed = "";

        public List<Color> Colors = new List<Color>();

        bool useRandomSeed = false;

        bool EntitySpawned = false;
        /// <summary>
        /// ///////////////////////////////////////////////////// flood fill stuff
        /// </summary>
        public void makeColors()
        {
            Colors.Add(Color.Red);
            Colors.Add(Color.Blue);
            Colors.Add(Color.Green);
            Colors.Add(Color.Purple);
            Colors.Add(Color.Yellow);
            Colors.Add(Color.Brown);
            Colors.Add(Color.Teal);
            Colors.Add(Color.Pink);
            Colors.Add(Color.Plum);
            Colors.Add(Color.PeachPuff);
            Colors.Add(Color.PowderBlue);
            Colors.Add(Color.Peru);
        }/// <summary>
        /// ///////////////////////////////////////////////////////
        /// </summary>

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
        Queue<Node> q;


        public void FloodFill(Node node, Color start, Color replacement)
        {
            List<Node> cavern = new List<Node>();


            q = new Queue<Node>();
            if (node.Color != start) return;
            node.Color = replacement;
            q.Enqueue(node);
            int loopcount = 0;
            while(q.Any())
            {
                Node n = q.Dequeue();
             
                List<Node> neigh = grid.getNeighbours(n);
                
                if (neigh[1].Color == start)
                {
                    neigh[1].Color = replacement;
                    q.Enqueue(neigh[1]);
                    
                }
                if (neigh[3].Color == start)
                {
                    neigh[3].Color = replacement;

                    q.Enqueue(neigh[3]);

                }
                if (neigh[5].Color == start)
                {
                    neigh[5].Color = replacement;

                    q.Enqueue(neigh[5]);

                }
                if (neigh[7].Color == start)
                {
                    neigh[7].Color = replacement;

                    q.Enqueue(neigh[7]);

                }

           
                
                    cavern.Add(n);        
            }
            if (cavern.Count < maxCavernSize)
            {
                Console.WriteLine("Cavern Deleted");
                Console.WriteLine("Cavern Size : " + cavern.Count);
                foreach (Node e in cavern)
                {
                    e.Color = Color.Green;
                }
            }
            Caverns.Add(cavern);

            if(Caverns.Count > 1)
            {
                int highest = 0;
                int highind = 0;
                List<Node> highCav;
                for (int i = 0; i < Caverns.Count; i++)
                {
                    if (Caverns[i].Count > highest)
                    {
                        highest = Caverns[i].Count;
                        highCav = Caverns[i];
                        highind = i;
                            }
                }

                Caverns.RemoveAt(highind);
                foreach (List<Node> c in Caverns)
                {
                    foreach (Node e in c)
                    {
                        e.Color = Color.Green;
                    }
                }
            }


     
           
        }

        public void ConnectCaverns(List<Node>cav1, List<Node>cav2)
        {
            
        }

        public void ColorMap()
        {
            for (int x = 0; x < mapWidth; x++)

            {
                for (int y = 0; y < mapHeight; y++)
                {
                    if (grid.getGrid[x, y].Blocked)
                    {
                        grid.getGrid[x, y].Color = Color.DarkRed;
                        WallNodes.Add(grid.getGrid[x, y]);
                    }
                    if (grid.getGrid[x, y].Blocked == false)
                    {
                        grid.getGrid[x, y].Color = Color.PaleVioletRed;
                        LandNodes.Add(grid.getGrid[x, y]);

                    }
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
                    else if (neighbouringWalls <= 4)
                    {
                        grid.getGrid[x, y].Blocked = false;

                    }

                }
            }


        }

       public int GetSurroundingWallCount(int x, int y)
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
            for (int i = 0; i < 3; i++)

            {

                SmoothMap();

            }
            ColorMap();

            mapWidth = grid.getGrid.GetLength(0);
            mapHeight = grid.getGrid.GetLength(1);

            for (int x = 0; x < mapWidth; x++)

            {

                for (int y = 0; y < mapHeight; y++)

                {
                    if(grid.getGrid[x,y].Color == Color.DarkRed)
                    {
                        int width = grid.getGrid[x, y].Tex.Width;
                        int height = grid.getGrid[x, y].Tex.Height;
                        float newX = grid.getGrid[x, y].Position.X;
                        square sq = new square();
                        sq.setBounds(grid.getGrid[x, y].Bounds,Color.White);
                        Locator.Instance.getService<IDetectionManager>().addMapCollidable(new CollisionNode(grid.getGrid[x, y].Bounds) );
                        grid.getGrid[x, y].Color = Color.Green;


                    }
                }
            }
            makeColors();

            int currentcolour = 0;
         //  FloodFill(LandNodes[0], Color.PaleVioletRed, Color.Black);
            for (int i = 0; i < LandNodes.Count; i++)
            {
                if (LandNodes[i].Color == Color.PaleVioletRed)
                {
                    
                    if (currentcolour > Colors.Count)
                    {
                        currentcolour = Colors.Count;
                    }
                    FloodFill(LandNodes[i], Color.PaleVioletRed, Colors[currentcolour]);
                    currentcolour++;
                }
            }

            Console.WriteLine(Caverns.Count);
       

            return grid;

        }
    }
}

