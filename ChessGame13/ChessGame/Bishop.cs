using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ChessGame
{
    class Bishop : Piece // bishop class inherits from piece and giving it an image stored in Resources folder based on its colour
    {
        public Bishop(bool directionUp) : base(directionUp) // creating a bishop piece
        {
            whiteBitmapImg = new BitmapImage(new Uri(resourceFolder + "WhiteBishop.png", UriKind.RelativeOrAbsolute));
            blackBitmapImg = new BitmapImage(new Uri(resourceFolder + "BlackBishop.png", UriKind.RelativeOrAbsolute));
        }


        public override List<List<Point>> GetListOfAvailableDirections(int row, int col) // finding all available directions for movement for a bishop
        {
            List<List<Point>> allDirectionsList = new List<List<Point>>();
            List<Point> list = new List<Point>();


            //diagonal up right moves
            int i, j;
            for (i = row + 1, j = col + 1; i < 8 && j < 8; i++, j++)
            {
                list.Add(new Point(i, j)); //piecefull direction
            }
            allDirectionsList.Add(list); //add direction
            list = new List<Point>(); //clear data for next direction

            //diagonal up left moves
            for (i = row + 1, j = col - 1; i < 8 && j >= 0; i++, j--)
            {
                list.Add(new Point(i, j));
            }
            allDirectionsList.Add(list); 
            list = new List<Point>(); 

            //diagonal down left moves
            for (i = row - 1, j = col - 1; i >= 0 && j >= 0; i--, j--)
            {
                list.Add(new Point(i, j)); 
            }
            allDirectionsList.Add(list); 
            list = new List<Point>(); 

            //diagonal down right moves
            for (i = row - 1, j = col + 1; i >= 0 && j < 8; i--, j++)
            {
                list.Add(new Point(i, j)); 
            }
            allDirectionsList.Add(list);

            return allDirectionsList; // returns the list with all possible directions for a bishop
        }

        public override object Clone() // cloning the position for use when "undo move" is applied
        {
            Bishop bishop = new Bishop(directionUp);
            bishop.blackBitmapImg = blackBitmapImg;
            bishop.whiteBitmapImg = whiteBitmapImg;
            bishop.currentBitmapImg = currentBitmapImg;
            bishop.isWhite = isWhite;
            return bishop;
        }
    }
}
