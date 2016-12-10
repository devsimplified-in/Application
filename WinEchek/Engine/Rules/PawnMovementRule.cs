﻿using WinEchek.Model;
using WinEchek.Model.Piece;

namespace WinEchek.Engine.Rules
{
    public class PawnMovementRule : IRule
    {
        public bool IsMoveValid(Move move)
        {
            Square square = move.Square;
            Piece piece = move.Piece;
            bool isWhite = piece.Color == Color.White;
            bool isStartPosition = piece.Square.Y == 1 && !isWhite || piece.Square.Y == 6 && isWhite;

            if (square.Piece == null)
            {
                return //Déplacement d'une case en avant
                    (piece.Square.Y - square.Y == (isWhite ? 1 : -1) ||
                    //Premier déplacement de deux cases
                    (isStartPosition && piece.Square.Y - square.Y == (isWhite ? 2 : -2))) 
                    &&
                piece.Square.X == square.X;
                
            }
            else
            {
                return //Seulement les deux cases diagonal
                (piece.Square.X == square.X - 1 || piece.Square.X == square.X + 1) &&
                //D'une case en avant
                piece.Square.Y - square.Y == (isWhite ? 1 : -1);
            }
            
        }
    }
}