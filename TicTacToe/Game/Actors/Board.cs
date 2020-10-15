using SFML.Graphics;
using System;
using System.Collections.Generic;
using TicTacToe.Game.Data;
using TicTacToe.Game.GUI.RenderObjects;
using TicTacToe.Utility;
using static TicTacToe.Utility.Utility;

namespace TicTacToe.Game.Actors
{
    internal class Board : Actor
    {
        public int Size { get; private set; }
        private List<List<Field>> Fields { get; set; }
        private TextureManager TextureManager;

        private PlayersManager PlayersManager;

        public Board(int newSize, Position position, Gamestate gamestate, PlayersManager playersManager, TextureManager textureManager) : base(position, gamestate)
        {
            if (IntBetweenInclusive(newSize, 2, int.MaxValue))
            {
                Size = newSize;
            }
            else
            {
                throw new ArgumentException("Size of the board must be bigger than 1", "newSize");
            }

            PlayersManager = playersManager;
            TextureManager = textureManager;
            Fields = new List<List<Field>>();

            for (int column = 0; column < Size; column++)
            {
                Fields.Add(new List<Field>());

                for (int row = 0; row < Size; row++)
                {
                    

                    Fields[column].Add(new Field(new Position(column * position.Width / Size + position.X, row * position.Height / Size + position.Y, position.Width / Size, position.Height / Size), Gamestate, PlayersManager, getTextureForField(row, column)));
                }
            }

            RecalculateComponentsPositions();
        }

        public override List<IRenderObject> GetRenderObjects()
        {
            RecalculateComponentsPositions();
            List<IRenderObject> RenderObjects = new List<IRenderObject>();

            for (int column = 0; column < Size; column++)
            {
                for (int row = 0; row < Size; row++)
                {
                    Fields[column][row].RecalculateComponentsPositions();
                    RenderObjects.AddRange(Fields[column][row].GetRenderObjects());
                }
            }

            return RenderObjects;
        }

        public Boardstate CheckBoard()
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
                        continue;
                    }
                }

                if (!isBad)
                {
                    return Boardstate.Won;
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
                        continue;
                    }
                }

                if (!isBad)
                {
                    return Boardstate.Won;
                }
            }

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
                return Boardstate.Won;
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
                return Boardstate.Won;
            }
            

            return hasEmptyField ? Boardstate.NotResolved : Boardstate.Draw;
        }

        private Texture getTextureForField(int row, int column)
        {
            if (row == 0)
            {
                if (column == 0)
                {
                    return TextureManager.TexturesDictionary[TextureType.Field]["tl"];
                }
                else if (column == Size -1)
                {
                    return TextureManager.TexturesDictionary[TextureType.Field]["tr"];
                }
                else
                {
                    return TextureManager.TexturesDictionary[TextureType.Field]["t"];
                }
            }
            else if (row == Size - 1)
            {
                if (column == 0)
                {
                    return TextureManager.TexturesDictionary[TextureType.Field]["bl"];
                }
                else if (column == Size - 1)
                {
                    return TextureManager.TexturesDictionary[TextureType.Field]["br"];
                }
                else
                {
                    return TextureManager.TexturesDictionary[TextureType.Field]["b"];
                }
            }
            else if (column == 0)
            {
                return TextureManager.TexturesDictionary[TextureType.Field]["l"];
            }
            else if (column == Size - 1)
            {
                return TextureManager.TexturesDictionary[TextureType.Field]["r"];
            }
            else
            {
                return TextureManager.TexturesDictionary[TextureType.Field]["c"];
            }
        }

        public override void RecalculateComponentsPositions()
        {
        }
    }
}