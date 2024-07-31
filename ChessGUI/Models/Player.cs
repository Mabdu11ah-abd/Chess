using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace ChessGUI.Models
{
    public enum PlayerColor
    {
        Black, White
    }
   

    public class Player
    {
        PlayerColor playerColor {  get; set; }
        bool inCheck { get; set; }

        public Player(PlayerColor playerColor, bool inCheck)
        {
            this.playerColor = playerColor;
            this.inCheck = inCheck;
        }
        public void AlterCheck()
        {
            
        }
    }
}
