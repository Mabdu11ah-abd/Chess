using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGUI.Models
{
    public class Direction
    {
        public (int, int)[] SlidingDirections = { (-1, 0), (1, 0), (0, 1), (0, -1), (1, 1), (-1, -1), (1, -1), (-1, 1) };
        public (int, int)[] KnightDirections = { (2, -1), (2, 1), (-2, -1), (-2, 1), (1, 2), (-1, 2), (1, -2), (-1, -2) };
        public (int, int)[] PawnDirections = {(-1,0),(-1, 1), (-1, -1), (1, 0), (1,1), (1,-1)};
    }
}
