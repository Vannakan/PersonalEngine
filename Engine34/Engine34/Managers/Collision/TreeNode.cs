using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine34.Managers.Collision
{
    public class TreeNode
    {
        public Rectangle Bounds;
        public TreeNode Parent;
        public TreeNode[] treeNodes = new TreeNode[4];
        public List<object> currentObjects = new List<object>();

        public  TreeNode()
        {

        }

        public void Split()
        {
            treeNodes[0] = new TreeNode();
            treeNodes[1] = new TreeNode();
            treeNodes[2] = new TreeNode();
            treeNodes[3] = new TreeNode();

        }

        public void InsertObject(object obj)
        {

        }
    }
}
