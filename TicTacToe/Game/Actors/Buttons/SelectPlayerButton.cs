using SFML.Graphics;
using SFML.Window;
using System.Collections.Generic;
using TicTacToe.Game.Data;
using TicTacToe.Game.Events;
using TicTacToe.Game.GUI.RenderObjects;
using TicTacToe.Utility;

namespace TicTacToe.Game.Actors.Buttons
{
    internal class SelectPlayerButton : Button
    {
        public PlayersManager PlayersManager;
        private int PlayerId { get; }
        private ScreenType Screen { get; }
        private RenderRoundedRectangle PlayerRectangle { get; set; }

        public SelectPlayerButton(Position position, Gamestate gamestate, PlayersManager playersManager, int playerId, ScreenType screenType) : base(position, gamestate)
        {
            PlayersManager = playersManager;
            PlayerId = playerId;
            Screen = screenType;

            PlayerRectangle = new RenderRoundedRectangle(new Position(), this, Color.White, new Color(120, 160, 255), CalculateScreenSpaceHeight(5), CalculateScreenSpaceHeight(20));

            RecalculateComponentsPositions();
        }

        public override List<IRenderObject> GetRenderObjects()
        {
            return new List<IRenderObject> { PlayerRectangle };
        }

        public override void RecalculateComponentsPositions()
        {
            PlayerRectangle.SetPosition(CalculateScreenSpacePosition(Position));
            PlayerRectangle.OutlineThickness = CalculateScreenSpaceHeight(5);
            PlayerRectangle.Radius = (CalculateScreenSpaceHeight(20));
        }

        protected override void OnStateChange(ButtonState buttonState)
        {
            if (PlayerRectangle != null)
            {
                switch (buttonState)
                {
                    case ButtonState.Active:
                        PlayerRectangle.SetColor(Color.White);
                        break;

                    case ButtonState.Inactive:
                        PlayerRectangle.SetColor(new Color(220, 220, 220));
                        break;

                    case ButtonState.Focused:
                        PlayerRectangle.SetColor(new Color(200, 240, 200));
                        break;
                }
            }
        }

        public override bool OnClick(MouseButtonEventArgs args)
        {
            if (base.OnClick(args))
            {
                PlayersManager.AddPlayerToGame(PlayerId);
                MessageBus.Instance.PostEvent(MessageType.ChangeScreen, this, new ChangeScreenEventArgs { Screen = Screen });
                return true;
            }
            return false;
        }
    }
}