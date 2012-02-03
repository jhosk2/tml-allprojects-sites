using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Threading;

namespace TearDrinkingBird
{
    class Marker
    {
        public enum MarkerType
        {
            eMT_Naga,
            eMT_Lecon,
            eMT_Doggabi,
            eMT_Ingan
        }

        public MarkerType Type
        {
            get { return Type; }
            private set { Type = value; }
        }

        private Texture2D   Texture;
        private Vector2     Position;
        private Boolean     SkillCooldown;

        public UInt16 MarkerPlaceID
        {
            get { return MarkerPlaceID; }
            set
            {
                if (value < 0)
                {
                    return;
                }

                MarkerPlaceID = value;
            }
        }

        private void GetPosition()
        {
            // MarkerPlaceID를 이용해 MarkerPlace의 x, y좌표를 읽어 vec에 저장
        }
        
        public Boolean SkillUse()
        {
            if (SkillCooldown)
            {
                return false;
            }

            switch (Type)
            {
                case MarkerType.eMT_Naga:
                    break;
                case MarkerType.eMT_Lecon:
                    break;
                case MarkerType.eMT_Doggabi:
                    break;
                case MarkerType.eMT_Ingan:
                    break;
            }

            SkillCooldown = true;

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Type"></param>
        /// <param name="Texture"> 250 x 500 pixel png format </param>
        /// <param name="SkillCoolTime"></param>
        public void Initialize( MarkerType Type, Texture2D Texture, long SkillCoolTime )
        {
            SkillCooldown   = false;
            this.Type       = Type;
            this.Texture    = Texture;
            Position        = new Vector2();
        }

        public void Update()
        {   
            GetPosition();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }
    }
}
