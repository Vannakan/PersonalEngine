﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine34.Utilities.StateMachine
{
    public interface IStateTable
    {
        IState getState(object obj);
        int getTableCount();
        void Initialize();
    }
}
