using GameCode.Items.ENUMS;
using GameCode.Items.Management;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameCode.Items.Generation
{
    class ItemGenerator
    {
        Random random;
        int min, max;
        ItemManager save;

        float rareScale = 90;
        float commonScale = 50;
        float unCommonScaleMax = 83;
        float unCommonScaleMin = 51;

        float offset1, offset2, offset3;

       public Dictionary<string, Dictionary<string, Rarity>> ItemList = new Dictionary<string, Dictionary<string, Rarity>>();

        public ItemGenerator(int pmin, int pmax,ItemManager e)
        {
            random = new Random();
            max = (pmax <= 100) ? pmax : 100;
            min = (pmin < pmax) ? pmin : 0;
            save = e;
        }

        public ItemGenerator()
        {
            
        }

        public Item generateItem(Vector2 pos)
        {
            Item item = new Item(save);
            int result = random.Next(min, max);

            if (result >= rareScale)
            {
                item.Initialize("RosaryBeads", pos, 20);
                if (rareScale < 100)
                    rareScale += 1;
                offset1 += 10;
                return item;

            }

            if (result < unCommonScaleMax && result >= unCommonScaleMin)
            {
                item.Initialize("Key", pos, 20);
                offset2 += 10;
                if (unCommonScaleMin < 66)
                    unCommonScaleMin += 5;
                return item;



            }

            if (result < commonScale)
            {
                int x = random.Next(0, 100);

                if (x > 50)
                {
                    item.Initialize("Chloroform", pos, 20);
                    offset3 += 10;
                    rareScale += -5;
                    unCommonScaleMin += -10;
                    return item;

                }

                if (x <= 50)
                {
                    item.Initialize("Needle", pos, 20);
                    offset3 += 10;
                    rareScale += -5;
                    unCommonScaleMin += -10;
                    return item;

                }


            }

            Console.WriteLine(item.name);

            if (item == null)
                generateItem(pos);

            return item;

        }

        public void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
