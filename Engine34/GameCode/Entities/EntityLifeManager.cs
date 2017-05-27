using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameCode.Entities
{
    class EntityLifeManager
    {


       public void OnEntityDeath(object source, EntityDeathArgs e)
        {
            e.DropItems();
        }
    }

    public class EntityDeathArgs
    {
        public void DropItems()
        {
            //Foreach item item in items
            //create these items as entities and scatter them randomly in a radius
            //then remove the items as ur looping
        }
    }
}
