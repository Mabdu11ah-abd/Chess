﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGUI.Models
{
    public class GameResult
    {
        WinType WinType { get; set; }
        Player Color { get; set; }
    }
}
