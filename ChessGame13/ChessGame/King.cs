using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ChessGame
{
    class King : Piece // king class inherits from piece
    {

        private bool isCastleAvailable = true;
        public King(bool directionUp) : base(directionUp) // creating a new king piece and giving it an image stored in Resources folder based on its colour
        {
            whiteBitmapImg = new BitmapImage(new Uri(resourceFolder + "WhiteKing.png", UriKind.RelativeOrAbsolute));
            blackBitmapImg = new BitmapImage(new Uri(resourceFolder + "BlackKing.png", UriKind.RelativeOrAbsolute));
        }


        public override List<List<Point>> GetListOfAvailableDirections(int row, int col)
        {
            List<List<Point>> allDirectionsList = new List<List<Point>>();
            List<Point> list = new List<Point>();

            list.Add(new Point(row + 1, col + 1)); // diagonal movement right and up
            allDirectionsList.Add(list); //add direction
            list = new List<Point>(); //clear data for next direction

            list.Add(new Point(row + 1, col)); // movement down
            allDirectionsList.Add(list); 
            list = new List<Point>(); 

            list.Add(new Point(row + 1, col - 1)); // diagonal movement left and down
            allDirectionsList.Add(list); 
            list = new List<Point>(); 

            list.Add(new Point(row, col + 1)); // movement right
            allDirectionsList.Add(list); 
            list = new List<Point>(); 

            list.Add(new Point(row, col - 1)); // movement left
            allDirectionsList.Add(list); 
            list = new List<Point>(); 

            list.Add(new Point(row - 1, col + 1)); // diagonal movement right and up
            allDirectionsList.Add(list); 
            list = new List<Point>(); 

            list.Add(new Point(row - 1, col)); // movement down
            allDirectionsList.Add(list);
            list = new List<Point>(); 

            list.Add(new Point(row - 1, col - 1)); // movement left and up
            allDirectionsList.Add(list);
            list = new List<Point>(); 



            RemoveNotAvailableCells(allDirectionsList); // removes all generated cells not on the board
            return allDirectionsList;  // returns the list with all possible directions for a king

        }
        public void SetIsCastleAvailable(bool isCheckAvailable) // setting availability of castle status
        {
            this.isCastleAvailable = isCheckAvailable;
        }
        public bool IsCastleAvailable() // can castle be made or not
        {
            return isCastleAvailable;
        }

        //returns list of cells which are used for check step
        public List<Point> GetCheckCells()
        {
            //if king made step, he can't use check step
            if (!isCastleAvailable) return null;

            List<Point> list = new List<Point>();
            list.Add(new Point(7, 5));
            list.Add(new Point(7, 6));
            list.Add(new Point(7, 3));
            list.Add(new Point(7, 2));
            return list;
        }
        public override object Clone() // cloning the position for use when "undo move" is applied
        {
            King king = new King(directionUp); 
            king.blackBitmapImg = blackBitmapImg;
            king.whiteBitmapImg = whiteBitmapImg;
            king.currentBitmapImg = currentBitmapImg;
            king.isWhite = isWhite;
            king.isCastleAvailable = isCastleAvailable;
            return king;
        }
    }
}
