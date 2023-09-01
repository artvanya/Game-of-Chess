using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ChessGame
{
    class Knight : Piece // knight class inherits from piece
    {
        
        public Knight(bool directionUp) : base(directionUp) // creating a new knight piece and giving it an image stored in Resources folder based on its colour
        {
            whiteBitmapImg = new BitmapImage(new Uri(resourceFolder + "WhiteKnight.png", UriKind.RelativeOrAbsolute));
            blackBitmapImg = new BitmapImage(new Uri(resourceFolder + "BlackKnight.png", UriKind.RelativeOrAbsolute));
        }


        public override List<List<Point>> GetListOfAvailableDirections(int row, int col)
        {
            List<List<Point>> allDirectionsList = new List<List<Point>>();
            List<Point> list = new List<Point>();
            

            list.Add(new Point(row + 2, col + 1));
            allDirectionsList.Add(list); //add direction
            list = new List<Point>(); //clear data for next direction

            list.Add(new Point(row + 1, col + 2));
            allDirectionsList.Add(list); 
            list = new List<Point>(); 

            list.Add(new Point(row - 2, col - 1));
            allDirectionsList.Add(list); 
            list = new List<Point>(); 

            list.Add(new Point(row - 1, col - 2));
            allDirectionsList.Add(list);
            list = new List<Point>();

            list.Add(new Point(row - 1, col + 2));
            allDirectionsList.Add(list); 
            list = new List<Point>(); 

            list.Add(new Point(row - 2, col + 1));
            allDirectionsList.Add(list); 
            list = new List<Point>(); 

            list.Add(new Point(row + 1, col - 2));
            allDirectionsList.Add(list); 
            list = new List<Point>(); 

            list.Add(new Point(row + 2, col - 1));
            allDirectionsList.Add(list);

            RemoveNotAvailableCells(allDirectionsList); // removes all generated cells not on the board
            return allDirectionsList; // returns the list with all possible directions for a knight
        }
        

        public override object Clone() // cloning the position for use when "undo move" is applied
        {
            Knight knight = new Knight(directionUp);
            knight.blackBitmapImg = blackBitmapImg;
            knight.whiteBitmapImg = whiteBitmapImg;
            knight.currentBitmapImg = currentBitmapImg;
            knight.isWhite = isWhite;
            
            return knight;
        }
    }
}
