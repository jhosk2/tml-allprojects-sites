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
            public get { return Type; }
            private set { Type = value; }
        }

        private Texture2D   Texture;
        private Vector2     Position = new Vector2();

        private Timer       SkillTimer;
        private Boolean     SkillCooldown = false;

        public UInt32 MarkerPlaceID
        {
            public get { return MarkerPlaceID; }
            public set
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

        private void SkillTimerCallback(object state)
        {
            Monitor.Enter(SkillCooldown);
            SkillCooldown = true;
            Monitor.Exit(SkillCooldown);
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

            return true;
        }

        public void Initialize( MarkerType Type, Texture2D Texture, long SkillCoolTime )
        {
            this.Type       = Type;
            SkillTimer      = new Timer( SkillTimerCallback, null, SkillCoolTime, Timeout.Infinite );
            this.Texture    = Texture;
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
