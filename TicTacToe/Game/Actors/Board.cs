using System;
using System.Collections.Generic;
using TicTacToe.Game.GUI.RenderObjects;
using static TicTacToe.Utility.Utility;
using TicTacToe.Utility;
using TicTacToe.Game.Data;

namespace TicTacToe.Game.Actors
{
    class Board : Actor, IRenderable
    {
        public int Size { get; private set; }
        private List<List<Field>> Fields { get; set; }

        public Board(int newSize, Position position, Gamestate gamestate) : base(position, gamestate)
        {
            if (IntBetweenInclusive(newSize, 2, int.MaxValue))
            {
                Size = newSize;
            }
            else
            {
                throw new ArgumentException("Size of the board must be bigger than 1", "newSize");
            }

            Fields = new List<List<Field>>();

            for (int column = 0; column < Size; column++)
            {
                Fields.Add(new List<Field>());

                for (int row = 0; row < Size; row++)
                {
                    Fields[column].Add(new Field(new Position(column*position.Width/Size, row*position.Height/Size, position.Width/Size, position.Height/Size), Gamestate));
                }
            }
        }

        public List<IRenderObject> GetRenderObjects()
        {
            List<IRenderObject> RenderObjects = new List<IRenderObject>();
            RenderObjects.Add(new RenderRectangle(0, 0, 300, 300, this));

            for (int column = 0; column < Size; column++)
            {
                for (int row = 0; row < Size; row++)
                {
                    RenderObjects.AddRange(Fields[column][row].GetRenderObjects());
                }
            }

            return RenderObjects;
        }

        public int CheckBoard()
        {
            //It is possible to make this cleaner and take less time incrementally building the state of each row, column and diagonal
            //TODO: maybe do this

            bool hasEmptyField = false;
            bool isBad = false;

            for (int column = 0; column < Size; column++)
            {
                isBad = false;
                for (int row = 0; row < Size; row++)
                {
                    if (Fields[column][row].PlayerID == 0 || Fields[column][0].PlayerID == 0)
                    {
                        hasEmptyField = true;
                        isBad = true;
                        break;
                    }

                    if (row == 0) continue;

                    if (!Fields[column][row].Equals(Fields[column][0]))
                    {
                        isBad = true;
                        break;
                    }
                }

                if (!isBad)
                {
                    return 1;
                }
            }

            for (int row = 0; row < Size; row++)
            {
                isBad = false;
                for (int column = 0; column < Size; column++)
                {
                    if (Fields[column][row].PlayerID == 0 || Fields[0][row].PlayerID == 0)
                    {
                        hasEmptyField = true;
                        isBad = true;
                        break;
                    }

                    if (column == 0) continue;

                    if (!Fields[column][row].Equals(Fields[0][row]))
                    {
                        isBad = true;
                        break;
                    }
                }

                if (!isBad)
                {
                    return 1;
                }
            }

            if (Size % 2 == 1)
            {
                isBad = false;
                for (int field = 0; field < Size; field++)
                {
                    if (Fields[field][field].PlayerID == 0 || Fields[0][0].PlayerID == 0)
                    {
                        hasEmptyField = true;
                        isBad = true;
                        break;
                    }

                    if (field == 0) continue;

                    if (!Fields[field][field].Equals(Fields[0][0]))
                    {
                        isBad = true;
                        break;
                    }
                }
                if (!isBad)
                {
                    return 1;
                }

                isBad = false;
                for (int field = 0; field < Size; field++)
                {
                    if (Fields[field][Size - field - 1].PlayerID == 0 || Fields[0][Size - 1].PlayerID == 0)
                    {
                        hasEmptyField = true;
                        isBad = true;
                        break;
                    }

                    if (field == 0) continue;

                    if (!Fields[field][Size - field - 1].Equals(Fields[0][Size - 1]))
                    {
                        isBad = true;
                        break;
                    }
                }
                if (!isBad)
                {
                    return 1;
                }
            }

            return hasEmptyField ? 0 : -1;
        }
    }
}
