using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ChessGame
{
    class Board
    {
        MyStack<Cell[,]> myStack = new MyStack<Cell[,]>(100);
        List<Piece> whitePiecesList = new List<Piece>();
        List<Piece> blackPiecesList = new List<Piece>();
        List<Cell> selectedCellsForMove;
        bool whiteMove = true; //white turn first


        public Cell[,] arrBoard = new Cell[8, 8];
        public static Cell selectedCell;
        ChessController controller;

        
        Image CreateImage(BitmapImage bitmap) // Creating images for pieces on the board
        {
            Image img = new Image();
            img.Source = bitmap;
            return img;
        }
        public Board() // filling the board with pieces
        {

            controller = new ChessController();
            controller.board = this;
            whitePiecesList.Add(new Rook(true));
            whitePiecesList.Add(new Knight(true));
            whitePiecesList.Add(new Bishop(true));
            whitePiecesList.Add(new Queen(true));
            whitePiecesList.Add(new King(true));

            blackPiecesList.Add(new Rook(true));
            blackPiecesList.Add(new Knight(true));
            blackPiecesList.Add(new Bishop(true));
            blackPiecesList.Add(new Queen(true));
            blackPiecesList.Add(new King(true));


            for (int i = 0; i < arrBoard.GetLength(0); i++)
            {
                for (int j = 0; j < arrBoard.GetLength(1); j++)
                {
                    Cell button;
                    button = new Cell(i, j, null);

                    button.Click += ClickAction;

                    FillBackground(button, i, j);

                    arrBoard[i, j] = button;

                }
            }
            FillPieces();
        }

        public void PawnPromotion(Cell cell) // promotion of the pawn
        {
            //Check that the pawn was white and moved upwards and reached the last row
            if (cell.GetPiece() != null && cell.GetPiece().isWhite && cell.GetRow() == 0 && cell.GetPiece() is Pawn)
            {
               
                Confirmation confirmationWindow = new Confirmation();
                confirmationWindow.ShowDialog(); //wait until the window closes
                if (confirmationWindow.result == 1) // promoting to a queen
                {
                    Queen q = new Queen(true);
                    q.isWhite = true;
                    q.currentBitmapImg = q.whiteBitmapImg;
                    cell.SetPiece(q);
                    cell.Content = CreateImage(cell.GetPiece().currentBitmapImg);
                }
                if (confirmationWindow.result == 2) // promoting to a rook
                {
                    Rook q = new Rook(true);
                    q.isWhite = true;
                    q.currentBitmapImg = q.whiteBitmapImg;
                    cell.SetPiece(q);
                    cell.Content = CreateImage(cell.GetPiece().currentBitmapImg);
                }
                if (confirmationWindow.result == 3) // promoting to a bishop
                {
                    Bishop q = new Bishop(true);
                    q.isWhite = true;
                    q.currentBitmapImg = q.whiteBitmapImg;
                    cell.SetPiece(q);
                    cell.Content = CreateImage(cell.GetPiece().currentBitmapImg);
                }
                if (confirmationWindow.result == 4) // promoting to a knight
                {
                    Knight q = new Knight(true);
                    q.isWhite = true;
                    q.currentBitmapImg = q.whiteBitmapImg;
                    cell.SetPiece(q);
                    cell.Content = CreateImage(cell.GetPiece().currentBitmapImg);
                } 
            }
            ////Check that the pawn was black and moved upwards and reached the last row
            else if (cell.GetPiece() != null && !cell.GetPiece().isWhite && cell.GetRow() == 0 && cell.GetPiece() is Pawn)
            {
                Confirmation confirmationWindow = new Confirmation();
                confirmationWindow.ShowDialog();
                if (confirmationWindow.result == 1)
                {
                    Queen q = new Queen(false); // promoting to a queen
                    q.isWhite = false;
                    q.currentBitmapImg = q.blackBitmapImg;
                    cell.SetPiece(q);
                    cell.Content = CreateImage(cell.GetPiece().currentBitmapImg);
                }
                if (confirmationWindow.result == 2) // promoting to a rook
                {
                    Rook q = new Rook(false);
                    q.isWhite = false;
                    q.currentBitmapImg = q.blackBitmapImg;
                    cell.SetPiece(q);
                    cell.Content = CreateImage(cell.GetPiece().currentBitmapImg);
                }
                if (confirmationWindow.result == 3) // promoting to a bishop
                {
                    Bishop q = new Bishop(false);
                    q.isWhite = false;
                    q.currentBitmapImg = q.blackBitmapImg;
                    cell.SetPiece(q);
                    cell.Content = CreateImage(cell.GetPiece().currentBitmapImg);
                }
                if (confirmationWindow.result == 4) // promoting to a knight
                {
                    Knight q = new Knight(false);
                    q.isWhite = false;
                    q.currentBitmapImg = q.blackBitmapImg;
                    cell.SetPiece(q);
                    cell.Content = CreateImage(cell.GetPiece().currentBitmapImg);
                }
            }
        }
        bool boolWhiteCheck;
        bool boolBlackCheck;

        public Cell[,] GetBoardClone() // clone the board for swapping sides after each move
        {
            Cell[,] clone = new Cell[arrBoard.GetLength(0), arrBoard.GetLength(1)];
            for (int i = 0; i < arrBoard.GetLength(0); i++)
            {
                for (int j = 0; j < arrBoard.GetLength(1); j++)
                {
                    clone[i, j] = (Cell)arrBoard[i, j].Clone();
                }
            }
            return clone;
        }


        public void ClickAction(object sender, RoutedEventArgs e)
        {

            Cell cell = (Cell)sender;
            bool madeMove = false; //true if pawn already moved
            if (selectedCell != null)
            {
                //check if user can make move to selected cell
                if (!selectedCell.IsEmpty())
                {
                    //selectedCell - cell which has the piece that is going to be moved
                    //cell - cell where we would like to move a piece
                    //true if we are allowed to go to cell
                    if (IsAvailableDirection(cell.GetRow(), cell.GetCol()))
                    {
                        //deletion of the old cell highlight
                        controller.Deselect(selectedCell);
                        myStack.Push(GetBoardClone()); //save the look of the board on the stack on the stack
                        controller.MakeMove(selectedCell, cell);
                        
                        
                        //if blacks have check and now it's time to move for blacks
                        //move made from selected cell to cell

                        if (controller.IsCheck(!whiteMove)) // if a player does a move that puts their king in check
                        {
                            MessageBox.Show("This move will put you in check. Try another move");
                            BackStep(false);
                            controller.DeleteKingHighlight();
                            return;
                        }
                        if (!whiteMove && boolBlackCheck)
                        {
                            //check if past move solved the check
                            if (controller.IsCheck(!whiteMove))
                            {     
                                MessageBox.Show("You will still be in check. Try another move");
                                BackStep(false);
                                return;
                            }
                            else
                            {
                                controller.DeleteKingHighlight();
                                boolBlackCheck = false;
                            }
                        }
                        //if whites have check and now it's time to move for whites
                        else if (whiteMove && boolWhiteCheck)
                        {
                            //check that past move solved the check
                            if (controller.IsCheck(!whiteMove))
                            {     
                                MessageBox.Show("You will still be in check. Try another move");
                                BackStep(false);
                                return;
                            }
                            else
                            {
                                controller.DeleteKingHighlight();
                                boolWhiteCheck = false;
                            }
                        }

                        //announce that move was completed
                        madeMove = true;
                        PawnPromotion(cell);
                        Swap();
                        CheckDraw();
                        if (!boolBlackCheck && whiteMove && controller.IsCheck(whiteMove)) //checking for a check
                        {
                            boolBlackCheck = true;
                            MessageBox.Show("You are in check!");
                        }
                        if (!boolWhiteCheck && !whiteMove && controller.IsCheck(whiteMove)) //checking for a check
                        {
                            boolWhiteCheck = true;
                            MessageBox.Show("You are in check!");
                        }
                        //check if after a move a player got a check
                        if (!boolBlackCheck && whiteMove && controller.IsCheck(whiteMove) || !boolWhiteCheck && !whiteMove && controller.IsCheck(whiteMove)) //checking for a check
                        {
                            if (controller.IsCheckMate(!whiteMove)) // identifies a checkmate it there is a check and the king could not be protected
                            {
                                MessageBox.Show("it is a mate. Whites win. Congratulations!🥇");
                                Environment.Exit(0);
                            }
                            else
                            {
                                boolBlackCheck = true;
                                MessageBox.Show("it is a mate. Blacks win. Congratulations!🥇");
                                Environment.Exit(0);
                            }
                        }

                        /* if (!boolBlackCheck && whiteMove && !controller.IsCheck(whiteMove) || !boolWhiteCheck && !whiteMove && !controller.IsCheck(whiteMove)) //checking that there is no check
                        {
                            if (controller.IsCheckMate(!whiteMove)) // identifies a stalemate if there is no check, however neither of the pieces of the opponent can move
                            {
                                MessageBox.Show("it is a mate. Whites win. Congratulations!🥇");
                                Environment.Exit(0);
                            }
                            else
                            {
                                boolBlackCheck = true;
                                MessageBox.Show("it is a mate. Blacks win. Congratulations!🥇");
                                Environment.Exit(0);
                            }
                        }*/ 

                        if (controller.IsDraw()) // identifies a draw when only two kings are left - not enough material on the board to continue the game
                        {
                            MessageBox.Show("it is a draw.");
                        }

                        whiteMove = !whiteMove; //change of turn

                    }
                    else
                    {
                        //if direction of move not allowed, just delete the highlight of the cell
                        controller.Deselect(selectedCell);
                    }
                    selectedCellsForMove = null;
                    selectedCell = null;
                }
            }
            //click on the cell with a piece
            if (!madeMove && !cell.IsEmpty())
            {
                if (cell.GetPiece().isWhite == whiteMove)
                {
                    selectedCellsForMove = controller.Select(cell);
                    //if there are possible cells for movement
                    if (selectedCellsForMove.Count != 0)
                    {
                        selectedCell = cell; //fill the selected cell
                    }
                    
                }
            }


        }

        void CheckDraw()
        {
            if (myStack.Count >= 8)
            {
                Cell[,] oldBoard = myStack.Get(myStack.Count - 8);
                bool isDraw = true;
                for (int i = 0; i < oldBoard.GetLength(0); i++)
                {
                    for (int j = 0; j < oldBoard.GetLength(1); j++)
                    {
                        if (oldBoard[i, j].GetPiece() != null && arrBoard[i, j].GetPiece() != null
                            && oldBoard[i, j].GetPiece().GetType() != arrBoard[i, j].GetPiece().GetType())
                        {
                            isDraw = false;
                        }
                        else if (oldBoard[i, j].GetPiece() == null && arrBoard[i, j].GetPiece() != null)
                        {
                            isDraw = false;
                        }

                        else if (oldBoard[i, j].GetPiece() != null && arrBoard[i, j].GetPiece() == null)
                        {
                            isDraw = false;
                        }


                    }
                }
                if (isDraw) MessageBox.Show("It is a draw!");


            }
        }
        public void BackStep(bool isChangeTurn) // implementation of "Undo move" feature using a stack
        {
            Cell[,] pastBoard = myStack.Pop();

            for (int i = 0; i < arrBoard.GetLength(0); i++)
            {
                for (int j = 0; j < arrBoard.GetLength(1); j++)
                {
                    arrBoard[i, j].SetPiece(pastBoard[i, j].GetPiece());

                    if (arrBoard[i, j].GetPiece() != null)
                    {
                        arrBoard[i, j].Content = CreateImage(pastBoard[i, j].GetPiece().currentBitmapImg);
                    }
                    else
                    {
                        arrBoard[i, j].Content = null;
                    }
                }
            }
            if (isChangeTurn)
            {
                whiteMove = !whiteMove;
            }
        }

        public void Swap() // turn the board over, so that the player whose turn it is now, goes up
        {
            Piece[,] arr = new Piece[8,8];

            for (int row = 0, i = arrBoard.GetLength(0) - 1; i >= 0; i--, row++)
            {
                for (int col = 0, j = arrBoard.GetLength(1) - 1; j >= 0; j--, col++)
                {
                    if (arrBoard[i, j].GetPiece() != null)
                    {
                        arr[row, col] = (Piece)arrBoard[i, j].GetPiece().Clone();
                    }
                }
            }

            
            for (int i = 0; i < arrBoard.GetLength(0); i++)
            {
                for (int j = 0; j < arrBoard.GetLength(1); j++)
                {
                    arrBoard[i, j].SetPiece(arr[i, j]);
                    
                    if(arr[i, j] != null)
                    {
                        arrBoard[i, j].GetPiece().directionUp = !arrBoard[i, j].GetPiece().directionUp;
                        arrBoard[i, j].Content = CreateImage(arr[i, j].currentBitmapImg);
                    }
                    else
                    {
                        arrBoard[i, j].Content = null;
                    }
                }
            }
        }

        public virtual bool IsAvailableDirection(int row, int col)
        {

            foreach (Cell availableCell in selectedCellsForMove)
            {

                if (availableCell.GetRow() == row && availableCell.GetCol() == col)
                {
                    return true;
                }

            }
            //if neither of the available cells has the same row and col
            return false;

        }


        public void FillPawns(int row, bool direction) // how to add pawns to the board
        {
            for (int col = 0; col < arrBoard.GetLength(1); col++)
            {
                arrBoard[row, col].SetPiece(new Pawn(direction));
                if (direction) {
                    arrBoard[row, col].Content = CreateImage(arrBoard[row, col].GetPiece().whiteBitmapImg);
                    arrBoard[row, col].GetPiece().currentBitmapImg = arrBoard[row, col].GetPiece().whiteBitmapImg;
                    arrBoard[row, col].GetPiece().isWhite = true;
                }
                else
                {
                    arrBoard[row, col].Content = CreateImage(arrBoard[row, col].GetPiece().blackBitmapImg);
                    arrBoard[row, col].GetPiece().currentBitmapImg = arrBoard[row, col].GetPiece().blackBitmapImg;
                    arrBoard[row, col].GetPiece().isWhite = false;
                }
                
            }
        }

        public void FillOthers(int row,  List<Piece> piecesList, bool direction) // how to fill the board with the rest of the pieces
        {
            //fill first three pieces
            for (int col = 0; col < 3; col++)
            {
                arrBoard[row, col].SetPiece(piecesList[col]);
                if (direction)
                {
                    arrBoard[row, col].Content = CreateImage(arrBoard[row, col].GetPiece().whiteBitmapImg);
                    arrBoard[row, col].GetPiece().currentBitmapImg = arrBoard[row, col].GetPiece().whiteBitmapImg;
                    arrBoard[row, col].GetPiece().isWhite = true;
                }
                else
                {
                    arrBoard[row, col].Content = CreateImage(arrBoard[row, col].GetPiece().blackBitmapImg);
                    arrBoard[row, col].GetPiece().currentBitmapImg = arrBoard[row, col].GetPiece().blackBitmapImg;
                    arrBoard[row, col].GetPiece().isWhite = false;
                }
                piecesList[col].directionUp = direction;

            }
            arrBoard[row, 3].SetPiece(piecesList[3]);
            if (direction)
            {
                arrBoard[row, 3].Content = CreateImage(arrBoard[row, 3].GetPiece().whiteBitmapImg);
                arrBoard[row, 3].GetPiece().currentBitmapImg = arrBoard[row, 3].GetPiece().whiteBitmapImg;
                arrBoard[row, 3].GetPiece().isWhite = true;
            }
            else
            {
                arrBoard[row, 3].Content = CreateImage(arrBoard[row, 3].GetPiece().blackBitmapImg);
                arrBoard[row, 3].GetPiece().currentBitmapImg = arrBoard[row, 3].GetPiece().blackBitmapImg;
                arrBoard[row, 3].GetPiece().isWhite = false;
            }
            arrBoard[row, 3].GetPiece().directionUp = direction;

            arrBoard[row, 4].SetPiece(piecesList[4]);
            if (direction)
            {
                arrBoard[row, 4].Content = CreateImage(arrBoard[row, 4].GetPiece().whiteBitmapImg);
                arrBoard[row, 4].GetPiece().currentBitmapImg = arrBoard[row, 4].GetPiece().whiteBitmapImg;
                arrBoard[row, 4].GetPiece().isWhite = true;
            }
            else
            {
                arrBoard[row, 4].Content = CreateImage(arrBoard[row, 4].GetPiece().blackBitmapImg);
                arrBoard[row, 4].GetPiece().currentBitmapImg = arrBoard[row, 4].GetPiece().blackBitmapImg;
                arrBoard[row, 4].GetPiece().isWhite = false;
            }
            arrBoard[row, 4].GetPiece().directionUp = direction;




            //fill last three pieces
            for (int col = 5, i = 2; col < 8; col++, i--)
            {
                arrBoard[row, col].SetPiece(piecesList[i]);
                if (direction)
                {
                    arrBoard[row, col].Content = CreateImage(arrBoard[row, col].GetPiece().whiteBitmapImg);
                    arrBoard[row, col].GetPiece().currentBitmapImg = arrBoard[row, col].GetPiece().whiteBitmapImg;
                    arrBoard[row, col].GetPiece().isWhite = true;
                }
                else
                {
                    arrBoard[row, col].Content = CreateImage(arrBoard[row, col].GetPiece().blackBitmapImg);
                    arrBoard[row, col].GetPiece().currentBitmapImg = arrBoard[row, col].GetPiece().blackBitmapImg;
                    arrBoard[row, col].GetPiece().isWhite = false;
                }
                arrBoard[row, col].GetPiece().directionUp = direction;
            }

        }

        public void FillPieces() // fill the board with all the required pieces
        {

            FillPawns(1, false);
            FillOthers(0, blackPiecesList, false);

            FillPawns(arrBoard.GetLength(0) - 2,  true);
            FillOthers(arrBoard.GetLength(0) - 1, whitePiecesList, true);

        }

        public void FillBackground(Cell btn, int i, int j) // fill the background design of the board and buttons
        {
            if (i % 2 == 0)
            {
                if (j % 2 == 0)
                {
                    btn.Background = Brushes.Beige;
                }
                else
                {
                    btn.Background = Brushes.ForestGreen;
                }
            }
            else
            {
                if (j % 2 == 0)
                {
                    btn.Background = Brushes.ForestGreen;
                }
                else
                {
                    btn.Background = Brushes.Beige;
                }
            }
        }
    }
}
