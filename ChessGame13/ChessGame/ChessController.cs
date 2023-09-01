using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ChessGame
{
    class ChessController
    {
        public Board board;
        int enemyPieceCounter = 0;
        Cell king;
        List<Cell> SourceCells = new List<Cell>();
        List<Cell> TargetCells = new List<Cell>();
        List<Piece> PieceCells = new List<Piece>();
        //targetCell - cell where teh user would like to move
        public virtual void MakeMove(Cell sourceCell, Cell targetCell) // player makes a move
        {
            SourceCells.Add(sourceCell);
            TargetCells.Add(targetCell);
            PieceCells.Add(sourceCell.GetPiece());
            bool wasCastleMade = false;
            if (sourceCell.GetPiece() != null && sourceCell.GetPiece() is King)
            {
                King king = (King)sourceCell.GetPiece();
                bool color = !sourceCell.GetPiece().isWhite;
                //castle to the right
                if (targetCell.GetRow() == 7 && targetCell.GetCol() == 6 )
                {
                    
                    if (!IsCheck(color) &&  !IsCheckCastle(color, board.arrBoard[7, 5]) && !IsCheckCastle(color, board.arrBoard[7, 6]))
                    {
                        //swap king and rook
                        MakeCastle(sourceCell, board.arrBoard[7, 6], board.arrBoard[7, 7], board.arrBoard[7, 5]);
                    }
                    wasCastleMade = true;

                }
                //castle to the left
                else if (targetCell.GetRow() == 7 && targetCell.GetCol() == 2  )
                {
                    
                    if (!IsCheck(color) && !IsCheckCastle(color, board.arrBoard[7, 2]))
                    {
                        //swap king and rook
                        MakeCastle(sourceCell, board.arrBoard[7, 2], board.arrBoard[7, 0], board.arrBoard[7, 3]);
                    }
                    wasCastleMade = true;
                }
                king.SetIsCastleAvailable(false);
            }
            else if (sourceCell.GetPiece() != null && sourceCell.GetPiece() is Rook)
            {
                Rook rook = (Rook)sourceCell.GetPiece();
                rook.SetIsCastleAvailable(false);
            }

            if (!wasCastleMade )
            {
                targetCell.SetPiece(sourceCell.GetPiece());
                targetCell.Content = sourceCell.Content;
                sourceCell.Content = null;
                sourceCell.SetPiece(null);
            }
        }
        public void MakeCastle(Cell kingCell, Cell kingTargetCell, Cell rookCell, Cell rookTargetCell) // implementing a castle
        {
            kingTargetCell.SetPiece(kingCell.GetPiece());
            kingTargetCell.Content = kingCell.Content;
            rookTargetCell.SetPiece(rookCell.GetPiece());
            rookTargetCell.Content = rookCell.Content;

            kingCell.Content = null;
            kingCell.SetPiece(null);
            rookCell.Content = null;
            rookCell.SetPiece(null);

        }

        public bool IsCheck(bool pieceColor) // checking if a check has been done
        {
            
            for (int i = 0; i < board.arrBoard.GetLength(0); i++)
            {
                for (int j = 0; j < board.arrBoard.GetLength(1); j++)
                {
                    Cell cell = board.arrBoard[i, j];
                    //if cell isn't empty
                    if (cell.GetPiece() != null)
                    {
                        //check if color of cell is the same as pieceColor
                        if (cell.GetPiece().isWhite == pieceColor)
                        {
                            //get all available directions of the cell's piece
                            List<Cell> filteredList = FilterAvailableMoves(cell);
                            Cell kingCell = GetKingCell(filteredList);
                            if(kingCell != null && kingCell.GetPiece().isWhite != pieceColor)
                            //if(kingCell != null)
                            {
                                PaintCell(kingCell);
                                king = kingCell;
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        public bool IsCheckCastle(bool pieceColor, Cell kingCell) // checking if during castle check would not stop it from occuring
        {

            for (int i = 0; i < board.arrBoard.GetLength(0); i++)
            {
                for (int j = 0; j < board.arrBoard.GetLength(1); j++)
                {
                    Cell cell = board.arrBoard[i, j];
                    //if cell isn't empty
                    if (cell.GetPiece() != null)
                    {
                        //check if color of cell is the same as pieceColor
                        if (cell.GetPiece().isWhite == pieceColor)
                        {
                            //get all available directions of the cell's piece
                            List<Cell> filteredList = FilterAvailableMoves(cell);
                            for (int z = 0; z < filteredList.Count; z++)
                            {
                                if (filteredList[z].GetRow() == kingCell.GetRow() && filteredList[z].GetCol() == kingCell.GetCol()) return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        public bool IsDraw()
        {
            bool IsDraw = true;
            for (int i = 0; i < board.arrBoard.GetLength(0); i++)
            {
                for (int j = 0; j < board.arrBoard.GetLength(1); j++)
                {
                    Cell cell = board.arrBoard[i, j];
                    //if cell isn't empty
                    if (cell.GetPiece() != null)
                    {
                        if (!(cell.GetPiece() is King))
                        {
                            IsDraw = false;
                        }
                    }
                }
            }
            return IsDraw;
        }

        public bool IsCheckMate(bool pieceColor) // checking if a check has been done
        {
            Cell kingCell=null;
            List<Cell> enemyCells = new List<Cell>();
            List<Cell> friendCells = new List<Cell>();
            
            for (int i = 0; i < board.arrBoard.GetLength(0); i++)
            {
                for (int j = 0; j < board.arrBoard.GetLength(1); j++)
                {
                    if(board.arrBoard[i, j].GetPiece()!=null && board.arrBoard[i,j].GetPiece().isWhite == pieceColor && board.arrBoard[i, j].GetPiece() is King)
                    {
                        kingCell = board.arrBoard[i, j];
                    }
                    if(board.arrBoard[i, j].GetPiece()!=null && board.arrBoard[i,j].GetPiece().isWhite == pieceColor)
                    {
                        enemyCells.Add(board.arrBoard[i, j]);
                    }
                    if (board.arrBoard[i, j].GetPiece() != null && board.arrBoard[i, j].GetPiece().isWhite != pieceColor && !(board.arrBoard[i, j].GetPiece() is King))
                    {
                        friendCells.Add(board.arrBoard[i, j]);
                    }

                }
            }
            List<Cell> dirs = FilterAvailableMoves(kingCell);
            
            for(int i = 0; i < dirs.Count; i++)
            {
                if (dirs[i].GetPiece() != null ||  CellContains(dirs[i], enemyCells))
                {
                    dirs.RemoveAt(i);
                    i--;
                }
            }
            if (dirs.Count != 0) return false;


            return true;
        }
        public bool CellContains(Cell cell, List<Cell> cells)
        {
            foreach(Cell c in cells)
            {
                List<Cell> availCells = FilterAvailableMoves(c);
                foreach(Cell availCell in availCells)
                {
                    if (availCell.GetRow() == cell.GetRow() && availCell.GetCol() == cell.GetCol()) return true;
                }
               
            }
            return false;
        }

        public void DeleteKingHighlight() // delete the highlight of the king when there is no check any more
        {
            if (king != null)
            {
                king.BorderThickness = new System.Windows.Thickness(0);
                king = null;
            }
        }
        public void PaintCell(Cell cell) // highlight of the cell
        {
            cell.BorderBrush = Brushes.Red;
            cell.BorderThickness = new System.Windows.Thickness(5);
        }
        Cell GetKingCell(List<Cell> list) // finds the position of a king
        {
            foreach(Cell cell in list)
            {
                if (cell.GetPiece() != null && cell.GetPiece() is King) return cell;
            }
            return null;
        }
        public (bool, bool) CanKingCastle(Cell kingCell) // checks whether the king can castle at the moment
        {
            bool checkToRight = true;
            bool checkToLeft = true;
            King king = (King)kingCell.GetPiece();
            if (king.IsCastleAvailable())
            {
                //checks whether cells on the rigth are empty
                int row = kingCell.GetRow();
                int col = kingCell.GetCol();
                for (int i = col+1; i < 7; i++)
                {
                    if (!board.arrBoard[row, i].IsEmpty()) checkToRight = false;
                }

                //checks whether cells on the left are empty
                row = kingCell.GetRow();
                col = kingCell.GetCol();
                for (int i = col-1; i > 0; i--)
                {
                    if (!board.arrBoard[row, i].IsEmpty()) checkToLeft = false;
                }

            }
            else
            {
                checkToRight = false;
                checkToLeft = false;
            }
            return (checkToLeft, checkToRight);
        }
        public List<Cell> FilterAvailableMoves(Cell selectedCell) // method takes a generated list with all possible movements of a piece and checks which of those are currently available on the board
        {
            List<Cell> finalAvailableCellsForMove = new List<Cell>();
            List<List<Point>> allDirectionsList = selectedCell.GetPiece().GetListOfAvailableDirections(selectedCell.GetRow(), selectedCell.GetCol());
            //starts the loop fro all piece cells in order from piece position
            foreach (List<Point> listOfDirections in allDirectionsList)
            {

                enemyPieceCounter = 0;
                foreach (Point point in listOfDirections)
                {

                    if (enemyPieceCounter > 0)
                    {
                        break;
                    }
                    Cell cell = board.arrBoard[point.GetRow(), point.GetCol()];

                    if (IsCellAvailableForMoving(selectedCell, cell, point))
                    {
                       
                        finalAvailableCellsForMove.Add(cell);
                    }
                    //if current desired cell was unavailable than stop the loop to avoid piece jumping
                    else
                    {
                        break;
                    }

                }
            }
            //check availability of king castling
            if (selectedCell.GetPiece() is King)
            {
                //check ability only if the king is located at the bottom
                if (selectedCell.GetRow() == 7)
                {
                    (bool checkToLeft, bool checkToRigth) = CanKingCastle(selectedCell);
                    if (checkToLeft)
                    {
                        finalAvailableCellsForMove.Add(board.arrBoard[7, 3]);
                        finalAvailableCellsForMove.Add(board.arrBoard[7, 2]);
                    }
                    if (checkToRigth)
                    {
                        finalAvailableCellsForMove.Add(board.arrBoard[7, 5]);
                        finalAvailableCellsForMove.Add(board.arrBoard[7, 6]);
                    }
                }

            }
            //if the piece is pawn we check all fight directions
            if (selectedCell.GetPiece() is Pawn)
            {
                List<Point> listOfDirections = allDirectionsList[0];
                foreach (Point point in listOfDirections)
                {
                    if (point.IsFightDirection())
                    {

                        Cell cell = board.arrBoard[point.GetRow(), point.GetCol()];
                        if (IsCellAvailableForMoving(selectedCell, cell, point))
                        {
                            finalAvailableCellsForMove.Add(cell);
                        }
                    }
                }
            }
            return finalAvailableCellsForMove;
        }
        public virtual List<Cell> Select(Cell selectedCell) // select a cell to be accessed
        {

            List<Cell> finalAvailableCellsForMove = FilterAvailableMoves(selectedCell);
            foreach(Cell cell in finalAvailableCellsForMove)
            {
                PaintCell(cell);  
            }
            return finalAvailableCellsForMove;
        }


        public virtual void Deselect(Cell selectedCell) // deselect a cell when it is no longer accessed
        {
            List<List<Point>> allDirectionsList = selectedCell.GetPiece().GetListOfAvailableDirections(selectedCell.GetRow(), selectedCell.GetCol());
            if(selectedCell.GetPiece() is King)
            {
                King king = (King)selectedCell.GetPiece();
                List<Point> points = king.GetCheckCells();
                if(points!=null) allDirectionsList.Add(points);
            }
            foreach (List<Point> listOfDirections in allDirectionsList)
            {
                foreach (Point point in listOfDirections)
                {
                    Cell cell = board.arrBoard[point.GetRow(), point.GetCol()];
                    cell.BorderThickness = new System.Windows.Thickness(0);
                }
            }
        }


        public bool IsCellAvailableForMoving(Cell selectedCell, Cell desiredCell, Point point) // checks whether the desired cell is allowed for movement
        {

            //if the pawn is moving and disired cell is empty and the move is piecefull
            if (selectedCell.GetPiece() is Pawn && desiredCell.IsEmpty() && !point.IsFightDirection())
            {
                return true;
            }
            
            else if (selectedCell.GetPiece() is Pawn && !desiredCell.IsEmpty() && point.IsFightDirection() && desiredCell.GetPiece().directionUp != selectedCell.GetPiece().directionUp)
            {
                return true;
            }
            
            else if (!(selectedCell.GetPiece() is Pawn) && desiredCell.IsEmpty())
            {
                return true;

            }
            else if (!(selectedCell.GetPiece() is Pawn) && !desiredCell.IsEmpty() && desiredCell.GetPiece().directionUp != selectedCell.GetPiece().directionUp)
            {
                enemyPieceCounter++;
                return true;
            }
            return false;
        }


    }
}
