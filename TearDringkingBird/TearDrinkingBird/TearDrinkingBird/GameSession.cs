using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace TearDrinkingBird
{
	class GameSession
	{
		public enum TurnState
		{
			eTS_P1,
			eTS_P2,
			eTS_P3,
			eTS_P4
		}

		public enum GameState
		{
			eGS_TurnStart,
			eGS_WaitingYutThrow,
			eGS_YutThrown,
			eGS_YutFell,
			eGS_BeforeMoveMarker,
			eGS_MovingMarker,
			eGS_AfterMoveMarker,
			eGS_BeforeTurnEnd,
			eGS_TurnEnd
		}

        private TurnState NowTurn;
		private TurnState PlayerTurn;
		private GameState gameState;

        private Marker[] Markers;	

		# region Private Functions

		private void TurnStart()
		{
            gameState = GameState.eGS_WaitingYutThrow;
		}
				
		private void WaitingYutThrow()
		{
            gameState = GameState.eGS_YutThrown;
		}
				
		private void YutThrown()
        {
            gameState = GameState.eGS_YutFell;
		}

		private void YutFell()
        {
            gameState = GameState.eGS_BeforeMoveMarker;
		}
				
		private void BeforeMoveMarker()
        {
            gameState = GameState.eGS_MovingMarker;
		}
				
		private void MovingMarker()
        {
            gameState = GameState.eGS_AfterMoveMarker;
		}

		private void AfterMoveMarker()
        {
            gameState = GameState.eGS_BeforeTurnEnd;
		}

		private void BeforeTurnEnd()
        {
            gameState = GameState.eGS_TurnEnd;
		}
		
		private void TurnEnd()
        {
            gameState = GameState.eGS_TurnStart;

            switch (NowTurn)
            {
                case TurnState.eTS_P1:
                    NowTurn = TurnState.eTS_P2;
                    break;
                case TurnState.eTS_P2:
                    NowTurn = TurnState.eTS_P3;
                    break;
                case TurnState.eTS_P3:
                    NowTurn = TurnState.eTS_P4;
                    break;
                case TurnState.eTS_P4:
                    NowTurn = TurnState.eTS_P1;
                    break;
            }
		}

		# endregion

		# region XNA Calling Function

		public void Initialize( TurnState PlayerTurn )
		{
            this.PlayerTurn = PlayerTurn;
            NowTurn         = TurnState.eTS_P1;
			gameState       = GameState.eGS_TurnStart;

            Markers         = new Marker[16];
            for( int i = 0; i < 4; i+=4 )
            {
                Markers[i] = new Marker();
                Markers[i].Initialize(Marker.MarkerType.eMT_Doggabi, 3);
                Markers[i + 1] = new Marker();
                Markers[i + 1].Initialize(Marker.MarkerType.eMT_Ingan, 3);
                Markers[i + 2] = new Marker();
                Markers[i + 2].Initialize(Marker.MarkerType.eMT_Lecon, 3);
                Markers[i + 3] = new Marker();
                Markers[i + 3].Initialize(Marker.MarkerType.eMT_Naga, 3);
            }
		}

        public void Draw()
		{
			switch ( gameState )
			{
				case GameState.eGS_TurnStart:
					break;
				case GameState.eGS_WaitingYutThrow:
					break;
				case GameState.eGS_YutThrown:
					break;
				case GameState.eGS_YutFell:
					break;
				case GameState.eGS_BeforeMoveMarker:
					break;
				case GameState.eGS_MovingMarker:
					break;
				case GameState.eGS_AfterMoveMarker:
					break;
				case GameState.eGS_BeforeTurnEnd:
					break;
				case GameState.eGS_TurnEnd:
					break;
			}
		}

		public void Update()
		{
			if ( Mouse.GetState().LeftButton == ButtonState.Pressed )
			{
                if ( NowTurn != PlayerTurn )
                {
                    return;
                }

				switch ( gameState )
				{
					case GameState.eGS_TurnStart:
						TurnStart();
						break;
					case GameState.eGS_WaitingYutThrow:
						WaitingYutThrow();
						break;
					case GameState.eGS_YutThrown:
						YutThrown();
						break;
					case GameState.eGS_YutFell:
						YutFell();
						break;
					case GameState.eGS_BeforeMoveMarker:
						BeforeMoveMarker();
						break;
					case GameState.eGS_MovingMarker:
						MovingMarker();
						break;
					case GameState.eGS_AfterMoveMarker:
						AfterMoveMarker();
						break;
					case GameState.eGS_BeforeTurnEnd:
						BeforeTurnEnd();
						break;
					case GameState.eGS_TurnEnd:
						TurnEnd();
						break;
				}
			}
		}

		# endregion
	}
}
