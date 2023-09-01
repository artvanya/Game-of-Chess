using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ChessGame
{
    class Queen : Piece // queen class inherits from piece
    {
        public Queen(bool directionUp) : base(directionUp) // creating a new king piece and giving it an image stored in Resources folder based on its colour
        {
            whiteBitmapImg = new BitmapImage(new Uri(resourceFolder + "WhiteQueen.png", UriKind.RelativeOrAbsolute));
            blackBitmapImg = new BitmapImage(new Uri(resourceFolder + "BlackQueen.png", UriKind.RelativeOrAbsolute));
        }


        public override List<List<Point>> GetListOfAvailableDirections(int row, int col)
        {
            //creates every direction in different list
            List<List<Point>> allDirectionsList = new List<List<Point>>();
            List<Point> list = new List<Point>();

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
                list.Add(new Point(i, col));
            }
            allDirectionsList.Add(list);
            list = new List<Point>();

            //movement horizontally to the right
            for (int i = col + 1; i < 8; i++)
            {
                list.Add(new Point(row, i));
            }
            allDirectionsList.Add(list);
            list = new List<Point>();

            //movement horizontally to the left
            for (int i = col - 1; i >= 0; i--)
            {
                list.Add(new Point(row, i));
            }
            allDirectionsList.Add(list);
            list = new List<Point>();

            //movement diagonally to the right and down
            for (int i = row + 1, j = col + 1; i < 8 && j < 8; i++, j++)
            {
                list.Add(new Point(i, j));
            }
            allDirectionsList.Add(list);
            list = new List<Point>();

            //movement diagonally to the right and down
            for (int i = row + 1, j = col - 1; i < 8 && j >= 0; i++, j--)
            {
                list.Add(new Point(i, j));
            }
            allDirectionsList.Add(list);
            list = new List<Point>();

            //movement diagonally to the left and up
            for (int i = row - 1, j = col - 1; i >= 0 && j >= 0; i--, j--)
            {
                list.Add(new Point(i, j));
            }
            allDirectionsList.Add(list);
            list = new List<Point>();

            //movement diagonally to the right and up
            for (int i = row - 1, j = col + 1; i >= 0 && j < 8; i--, j++)
            {
                list.Add(new Point(i, j));
            }
            allDirectionsList.Add(list);

            RemoveNotAvailableCells(allDirectionsList); // removes all generated cells not on the board
            return allDirectionsList; // returns the list with all possible directions for a queen
        }

        public override object Clone() // cloning the position for use when "undo move" is applied
        {
            Queen queen = new Queen(directionUp);
            queen.blackBitmapImg = blackBitmapImg;
            queen.whiteBitmapImg = whiteBitmapImg;
            queen.currentBitmapImg = currentBitmapImg;
            queen.isWhite = isWhite;
            return queen;
        }
    }
}
