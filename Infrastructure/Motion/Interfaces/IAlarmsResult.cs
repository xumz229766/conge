﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMotion.Interfaces
{
    public  interface IAlarmsResult
    {
         bool IsAlarms { get; set; }
         bool IsPrompt { get; set; }
         bool IsWarning { get; set; }
    }
}
