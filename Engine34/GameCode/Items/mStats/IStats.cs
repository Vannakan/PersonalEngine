using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameCode.Items.mStats
{
    public interface IStats
    {
        //Experience Property
        int EXP { get; set; }
        //Damage Property
        int DMG { get; set; }
        //Move Speed Property
        int mSPD { get; set; }
        //Bullet Speed Property
        int aSPD { get; set; }
        //HP Property
        int HP { get; set; }
    }
}
