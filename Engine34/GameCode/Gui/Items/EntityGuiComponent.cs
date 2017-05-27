using GameCode.Items.mStats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameCode.Gui.Items
{
    class EntityGuiComponent : GuiComponent
    {

        Stats stats;
        public void setEntityVals()
        {
            numValues.Add("Health", stats.HP);
            numValues.Add("Attack Speed", stats.aSPD);
            numValues.Add("Movement Speed", stats.mSPD);
            numValues.Add("EXP", stats.EXP);
            numValues.Add("Damage", stats.DMG);
        }

        public void setStats(Stats stats)
        {
            this.stats = stats;
        }
    }
}
