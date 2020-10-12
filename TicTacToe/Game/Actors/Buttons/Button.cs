using SFML.Window;
using TicTacToe.Game.Data;
using TicTacToe.Utility;

namespace TicTacToe.Game.Actors.Buttons
{
    internal abstract class Button : Actor, IClickable
    {
        private ButtonState myButtonState;

        public ButtonState ButtonState 
        { 
            get { return myButtonState; } 
            set { myButtonState = value; OnStateChange(value); } 
        }

        public Button(Position position, Gamestate gamestate) : this(position, gamestate, ButtonState.Active) { }
        public Button(Position position, Gamestate gamestate, ButtonState state) : base(position, gamestate) 
        {
            ButtonState = state;
        }

        public virtual bool OnClick(MouseButtonEventArgs args) 
        {
            return ButtonState != ButtonState.Inactive;
        }

        protected abstract void OnStateChange(ButtonState buttonState);
    }
}