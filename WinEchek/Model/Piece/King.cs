﻿using System;

namespace WinEchek.Model.Piece
{
    [Serializable]
    class King : Piece
    {
        public King(Color color, Square square) : base(color, square)
        {
        }

        public override Type Type()
        {
            return Model.Piece.Type.King;
        }
    }
}
