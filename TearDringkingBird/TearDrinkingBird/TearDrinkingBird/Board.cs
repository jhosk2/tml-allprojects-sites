using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using System.Collections;
using Microsoft.Xna.Framework;

namespace TearDrinkingBird
{
	class Board
	{
		private Texture2D Texture;

		public void Initialize( Texture2D Texture )
		{
			this.Texture = Texture;
		}

		public void Update()
		{

		}

		public void Draw( SpriteBatch spriteBatch )
		{

		}
	}

	class MarkerPlace
	{
		private ArrayList	RearMarkerPlace;
		private ArrayList	FrontMarkerPlace;

		private Texture2D	Texture;
		private Vector2		Position;

		public void Initialize( Texture2D Texture, Vector2 Position )
		{
			this.Position	= Position;
			this.Texture	= Texture;
		}

		public void Update()
		{

		}

		public void Draw( SpriteBatch spriteBatch )
		{
			spriteBatch.Draw( Texture, Position, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f );
		}

	}
}
