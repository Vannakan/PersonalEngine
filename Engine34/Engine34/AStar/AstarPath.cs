using System.Collections.Generic;
using System.Linq;
using Engine.Managers.ServiceLocator;
using Engine.Interfaces.Resource;
using Engine.Grid;

namespace Engine.Managers.AStar
{
    public class AstarPath
    {
        ///DECLARE: The graph in which the Astar will
        Grids aStarData;
        ///DECLARE A variable to hold out search
        AStarGridSearch currentDisp;

        /// <summary>
        /// METHOD: Show the debug path
        /// </summary>
        public void DebugPath()
        {
            if (currentDisp != null)
            {
                currentDisp.Show(currentDisp.getPathv().First(), currentDisp.getPathv().Last());
            }
        }

        /// <summary>
        /// METHOD: Commit a search and if it is successful, RETURN a list of nodes in the path
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="Start"></param>
        /// <param name="Goal"></param>
        /// <returns></returns>
        public List<Node> CommitPathSearch(Grids grid, Node Start, Node Goal)
        {
            AStarGridSearch search = new AStarGridSearch(grid);
            search.Search(Start, Goal, Locator.Instance.getService<IResourceLoader>().GetTex("Tile1"));
            List<Node> result = search.getPathv();
            currentDisp = search;
            return result;
        }


        /// <summary>
        /// METHOD: Commit a search and if it is successful, RETURN a queue of nodes in the path
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="Start"></param>
        /// <param name="Goal"></param>
        /// <returns></returns>
        public Queue<Node> CommitWayPointSearch(Grids grid, Node Start, Node Goal)
        {
            AStarGridSearch search = new AStarGridSearch(grid);
            search.Search(Start, Goal, Locator.Instance.getService<IResourceLoader>().GetTex("Tile1"));
            Queue<Node> result = search.getPath();
            currentDisp = search;
            return result;
        }

    }
}