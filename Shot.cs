using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameNew
{
    public class Shot : GameElem
    {
        private const int SHOT_SPEED = 10;

        public Shot(double x , double y)
        {
            this.X = x;
            this.Y = y;
        }

        protected override void Init()
        {
            imgUri = @"ms-appx:///Assets/BlueLaserShot1.png";
            base.Init();
        }

        public override void MoveElem()
        {
            this.Y -= SHOT_SPEED;
        }
        
    }
}
