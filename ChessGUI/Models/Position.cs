using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace ChessGUI.Models
{
    public class Position
    {
        private int moveCount;
        private string fen;

        public Position(int moveCount, string fen)
        {
            this.moveCount = moveCount;
            this.fen = fen;
        }
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
                return true;
            if (obj == null || GetType() != obj.GetType())
                return false;

            Position other = (Position)obj;
            return moveCount == other.moveCount && fen == other.fen;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(moveCount, fen);
        }
        public static bool operator ==(Position left, Position right)
        {
            if (ReferenceEquals(left, right))
                return true;
            if (left == null || right == null)
                return false;
            if (right.moveCount == left.moveCount && right.fen == left.fen)
                return true;
            else return false;
        }
        public static bool operator !=(Position left, Position right)
        {
            return !(left == right);
        }

       
        public void doPositionsRepeat()
        {

        }
    }
}
