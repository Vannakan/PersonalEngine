using Engine.Grid;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameCode.MapGeneration
{
    class Maze
    {
        int gridX;
        int gridY;
        Grids grid;
        List<Node> visited = new List<Node>();

        public Grids GenerateLevel(Grids gride)
        {
            grid = gride;
            gridX = grid.getGrid.GetLength(0);
            gridY = grid.getGrid.GetLength(1);

            for (int x = 0; x < gridX; x++)
            {

                for (int y = 0; y < gridY; y++)
                {


                    if ((x % 2 != 0) && (y % 2 != 0))
                    {
                        grid.getGrid[x, y].Color = Color.White;
                        grid.getGrid[x, y].Blocked = false;
                    }
                    else
                    {
                        grid.getGrid[x, y].Color = Color.Black;
                        grid.getGrid[x, y].Blocked = true;
                    }

                }
            }
            CreatePath(1, 1);
            CreateRooms(20);


            return grid;
        }

        public void CreatePath(int col, int row)
        {
            Node current = grid.getGrid[col, row];

            Vector2[] directions = new Vector2[] { new Vector2(0, -2), new Vector2(2, 0), new Vector2(0, 2), new Vector2(-2, 0) };

            for (int i = 0; i < 4; i++)
            {
                int dx = (int)current.getGrid.X + (int)directions[i].X;
                int dy = (int)current.getGrid.Y + (int)directions[i].Y;

                if (grid.checkMap(dx, dy))
                {
                    Node n = grid.getGrid[dx, dy];
                    if (!visited.Contains(n))
                    {
                        n.Color = Color.White;
                        n.Blocked = false;
                        visited.Add(n);

                        int ddx = (int)current.getGrid.X + ((int)directions[i].X / 2);
                        int ddy = (int)current.getGrid.Y + ((int)directions[i].Y / 2);

                        Node wall = grid.getGrid[ddx, ddy];
                        wall.Color = Color.White;
                        wall.Blocked = false;

                        CreatePath(dx, dy);
                    }
                }
            }
        }

        public void CreateRooms(int rooms)
        {
            Random random = new Random();


            for (int i = 0; i < rooms; i++)
            {
                int roomWidth = random.Next() % 2 + 1;
                int roomHeight = random.Next() % 2 + 1;

                int startX = random.Next() % (gridX - 2) + 1;
                int startY = random.Next() % (gridY - 2) + 1;

                for (int j = -1; j < roomWidth; j++)
                {
                    for (int z = -1; z < roomHeight; z++)
                    {
                        int newX = startX + j;
                        int newY = startY + z;

                        if (grid.checkMap(newX, newY))
                        {
                            if ((newX != 0) && (newX != (gridX - 1)) && (newY != 0) && (newY != gridY - 1))
                            {
                                grid.getGrid[newX, newY].Color = Color.White;
                                grid.getGrid[newX, newY].Blocked = false;
                            }
                        }
                    }
                }

            }
        }
    }
}

