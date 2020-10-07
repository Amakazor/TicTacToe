using System;
using System.Collections.Generic;
using System.Text;
using TicTacToe.Utility;

namespace TicTacToe.Game.Actors
{
    interface ITextContainer
    {
        public Position CalculateTextPosition(Position position, Position relativeTextPosition)
        {
            return new Position(position.X + relativeTextPosition.X, position.Y + relativeTextPosition.Y, 0, relativeTextPosition.Height);
        }
    }
}
