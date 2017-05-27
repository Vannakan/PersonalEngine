using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine34.Entities.Animation
{
    public interface IAnimationTable
    {
        IAnimationState getState(object anim);
        void Initialize();
        int getTableCount();
    }
}
