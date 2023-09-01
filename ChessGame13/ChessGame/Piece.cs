using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ChessGame
{
    class Piece : ICloneable
    {
        public bool directionUp;
        public BitmapImage whiteBitmapImg;
        public BitmapImage blackBitmapImg;
        public BitmapImage currentBitmapImg;
        public bool isWhite;
        protected string resourceFolder = "../../Resources/";

        public Piece(bool directionUp)
        {
            this.directionUp = directionUp;
        }
        
        public void RemoveNotAvailableCells(List<List<Point>> allDirectionsList)  //try to check that a cell is exist on the board in each direction
        {

            foreach (List<Point> list in allDirectionsList)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].GetRow() < 0 || list[i].GetRow() > 7 || list[i].GetCol() < 0 || list[i].GetCol() > 7)
                    {
                        list.RemoveAt(i);
                        i--;
                    }
                }
            }
        }

        public virtual void MakeMove(Cell targetCell)
        {
            throw new Exception();
        }
        public virtual bool Select()
        {
            throw new Exception();
        }

        public virtual void Deselect()
        {
            throw new Exception();
        }

        //returns only regular move's directions
        public virtual List<List<Point>> GetListOfAvailableDirections(int row, int col)
        {
            throw new Exception();
        }




        public virtual bool IsAvailableDirection(int row, int col)
        {
            throw new Exception();
        }

        public virtual object Clone()
        {
            throw new NotImplementedException();
        }
    }
}
