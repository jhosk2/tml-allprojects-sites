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

		private TurnState turnState;
		private GameState gameState;
		

		# region Private Functions

		private void TurnStart()
		{
		}
				
		private void WaitingYutThrow()
		{
		}
				
		private void YutThrown()
		{
		}

		private void YutFell()
		{
		}
				
		private void BeforeMoveMarker()
		{
		}
				
		private void MovingMarker()
		{
		}

		private void AfterMoveMarker()
		{
		}

		private void BeforeTurnEnd()
		{
		}
		
		private void TurnEnd()
		{
		}

		# endregion

		# region XNA Calling Function

		public void Initialize()
		{
			turnState = TurnState.eTS_P1;
			gameState = GameState.eGS_TurnStart;
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
