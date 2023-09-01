using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ChessGame
{
    class Cell : Button, ICloneable // class, used to know which cell the user clicks on, on the board
    {
        private int row;
        private int col;
        private Piece piece;

        public Cell(int row, int col, Piece piece)
        {
            this.row = row;
            this.col = col;
            this.piece = piece;
            BorderThickness = new System.Windows.Thickness(0);

        }
        public bool IsEmpty()
        {
            return piece == null;
        }

        public object Clone()
        {
            Piece pieceClone = null;
            if (piece != null)
            {
                pieceClone = (Piece)piece.Clone();
            }
            Cell cellClone = new Cell(row, col, pieceClone);
            return cellClone;
        }

        //Get and Set methods for the used variables in order to make them private
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
        public void SetPiece(Piece piece)
        {
            this.piece = piece;
        }
        public Piece GetPiece()
        {
            return piece;
        }
    }
}
