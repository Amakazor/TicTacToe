using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToe.Utility.Exceptions
{
    class FileMissingOrCorruptedException : Exception
    {
        public FileMissingOrCorruptedException()
        {
        }

        public FileMissingOrCorruptedException(string message) : base(message)
        {
        }
    }
}
