using SFML.Graphics;
using SFML.Window;
using TicTacToe.Game.Data;
using TicTacToe.Game.Events;
using TicTacToe.Utility;

namespace TicTacToe.Game.Actors.Buttons
{
    internal class ReturnButton : IconButton
    {
        private ScreenType ScreenToReturn { get; }
        private ScreenType ScreenToSetAsPrevious { get; }
        private bool SetPrevious { get; }

        public ReturnButton(Position position, Gamestate gamestate, Texture backgroundTexture, ScreenType screenToReturn, ScreenType screenToSetAsPrevious) : this(position, gamestate, backgroundTexture, screenToReturn)
        {
            ScreenToSetAsPrevious = screenToSetAsPrevious;
            SetPrevious = true;
        }

        public ReturnButton(Position position, Gamestate gamestate, Texture backgroundTexture, ScreenType screenToReturn) : base(position, gamestate, backgroundTexture, 0.6F)
        {
            ScreenToReturn = screenToReturn;
        }

        public override bool OnClick(MouseButtonEventArgs args)
        {
            if (base.OnClick(args))
            {
                if (SetPrevious)
                {
                    Gamestate.PreviousScreen = ScreenToSetAsPrevious;
                }

                MessageBus.Instance.PostEvent(MessageType.ChangeScreen, this, new ChangeScreenEventArgs { Screen = ScreenToReturn, ChangePreviousScreen = !SetPrevious });
                return true;
            }
            return false;
        }
    }
}