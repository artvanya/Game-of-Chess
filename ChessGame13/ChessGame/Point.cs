using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{
    class Point // class that is used to access every cell on the board
    {
        private int row;
        private int col;
        private bool fightDirection = true;

        public Point(int row, int col)
        {
            this.row = row;
            this.col = col;
        }

        public Point(int row, int col, bool fightDirection) : this(row, col)
        {
            this.fightDirection = fightDirection;
        }

        public void SetRow(int row)
        {
            this.row = row;
        }
        public int GetRow()
        {
            return row;
        }

        public void SetCol(int col)
        {
            this.col = col;
        }
        public int GetCol()
        {
            return col;
        }
        public void SetFightDirection(bool fightDirection)
        {
            this.fightDirection = fightDirection;
        }
        public bool IsFightDirection()
        {
            return fightDirection;
        }

    }
}
