using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ChessGame
{
    class Rook : Piece // king class inherits from piece
    {
        private bool isCastleAvailable = true;
        public Rook(bool directionUp) : base(directionUp) // creating a new king piece and giving it an image stored in Resources folder based on its colour
        {
            whiteBitmapImg = new BitmapImage(new Uri(resourceFolder + "WhiteRook.png", UriKind.RelativeOrAbsolute));
            blackBitmapImg = new BitmapImage(new Uri(resourceFolder + "BlackRook.png", UriKind.RelativeOrAbsolute));
        }


        public override List<List<Point>> GetListOfAvailableDirections(int row, int col)
        {
            List<List<Point>> allDirectionsList = new List<List<Point>>();
            List<Point> list = new List<Point>();
            int nextRow = row;
            int nextCol = col;
  

            //movement vertically down
            for (int i = row + 1; i < 8; i++)
            {
                list.Add(new Point(i, col)); //piecefull direction
            }
            allDirectionsList.Add(list); //add direction
            list = new List<Point>(); //clear data for next direction

            //movement vertically up
            for (int i = row - 1; i >= 0; i--)
            {
                list.Add(new Point(i, col)); //piecefull direction
            }
            allDirectionsList.Add(list); //add direction
            list = new List<Point>(); //clear data for next direction

            //movement horizontally to the right
            for (int i = col + 1; i < 8; i++)
            {
                list.Add(new Point(row, i)); //piecefull direction
            }
            allDirectionsList.Add(list); //add direction
            list = new List<Point>(); //clear data for next direction

            //movement horizontally to the left
            for (int i = col - 1; i >= 0; i--)
            {
                list.Add(new Point(row, i)); //piecefull direction
            }
            allDirectionsList.Add(list);

            RemoveNotAvailableCells(allDirectionsList); // removes all generated cells not on the board
            return allDirectionsList; // returns the list with all possible directions for a queen
        }
        public void SetIsCastleAvailable(bool isCheckAvailable) // setting availability of castle status
        {
            this.isCastleAvailable = isCheckAvailable;
        }
        public bool IsCastleAvailable() // can castle be made or not
        {
            return isCastleAvailable;
        }
        public override object Clone() // cloning the position for use when "undo move" is applied
        {
            Rook rook = new Rook(directionUp);
            rook.blackBitmapImg = blackBitmapImg;
            rook.whiteBitmapImg = whiteBitmapImg;
            rook.currentBitmapImg = currentBitmapImg;
            rook.isWhite = isWhite;
            rook.isCastleAvailable = isCastleAvailable;
            return rook;
        }
    }
}
