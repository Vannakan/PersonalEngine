using GameCode.Items.Generation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameCode.Items.Management
{
    public class ItemManager
    {
        ItemGenerator _itemGenerator;
        List<Item> currentItems;

        public ItemManager()
        {
            _itemGenerator = new ItemGenerator(0,100,this);
            currentItems = new List<Item>();
        }

        public void AddItem(Vector2 pos)
        {
           currentItems.Add(_itemGenerator.generateItem(pos));
            
        }

        public void RemoveItem(string name)
        {
            bool found = false;
            foreach(Item i in currentItems )
            {
                if (name == i.name && found == false)
                {
                    currentItems.Remove(i);
                     found = true;
                }
            }
        }

        public void RemoveItem(Item item)
        {
           for(int i = 0; i < currentItems.Count; i++)
            {
                if(currentItems[i] == item)
                {
                    currentItems.Remove(currentItems[i]);
                }
            }
        }


        public void Draw(SpriteBatch spr)
        {
            for (int i = 0; i < currentItems.Count; i++)
            {
                currentItems[i].Draw(spr);
            }
        }

        public void Update(GameTime gameTime)
        {
            
        }
    }
}
