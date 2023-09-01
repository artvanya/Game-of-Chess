using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ChessGame
{
    class Pawn : Piece // pawn class inherits from piece
    {
        public Pawn(bool directionUp) : base(directionUp) // creating a new pawn piece and giving it an image stored in Resources folder based on its colour
        {
            whiteBitmapImg = new BitmapImage(new Uri(resourceFolder + "WhitePawn.png", UriKind.RelativeOrAbsolute));
            blackBitmapImg = new BitmapImage(new Uri(resourceFolder + "BlackPawn.png", UriKind.RelativeOrAbsolute));
        }

        public override List<List<Point>> GetListOfAvailableDirections(int row, int col)
        {
            List<List<Point>> allDirectionsList = new List<List<Point>>();
            List<Point> list = new List<Point>();
            int nextRow = row;
            int nextCol = col;
            if (directionUp)
            {
                list.Add(new Point(nextRow - 1, nextCol, false)); //piecefull direction
                if (row == 6)
                {
                    list.Add(new Point(nextRow - 2, nextCol, false)); //piecefull direction
                }
                list.Add(new Point(nextRow - 1, nextCol - 1)); //fight direction
                list.Add(new Point(nextRow - 1, nextCol + 1));  //fight direction
            }
            else
            {
                list.Add(new Point(nextRow + 1, nextCol, false)); //piecefull direction
                if (row == 1)
                {
                    list.Add(new Point(nextRow + 2, nextCol, false)); //piecefull direction
                }
                list.Add(new Point(nextRow + 1, nextCol + 1));
                list.Add(new Point(nextRow + 1, nextCol - 1));
            }
            allDirectionsList.Add(list);
            RemoveNotAvailableCells(allDirectionsList); // removes all generated cells not on the board
            return allDirectionsList; // returns the list with all possible directions for a knight
        }


        public override object Clone() // cloning the position for use when "undo move" is applied
        {
            Pawn pawn = new Pawn(directionUp);
            pawn.blackBitmapImg = blackBitmapImg;
            pawn.whiteBitmapImg = whiteBitmapImg;
            pawn.currentBitmapImg = currentBitmapImg;
            pawn.isWhite = isWhite;
            return pawn;
        }
    }
}
