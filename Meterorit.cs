using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameNew
{
    class Meterorit : Enemy
    {
        private const int STEP_MET = 5;
        
        public Meterorit(int actualHeight,int actualWidth)
        {
            Random rand = new Random();
            int ymet = rand.Next(actualHeight/3);
            int xmet = rand.Next(actualWidth);
            this.X =   xmet;
            this.Y = ymet;
            this.Hieght = 100;
            this.Width = 100;
            this.Score = 3;
            this.Damage = 3;
        }

        protected override void Init()
        {
            imgUri = @"ms-appx:///Assets/meteoroid.png";
            base.Init();
        }

        public override  void MoveElem()
        {
            this.Y += STEP_MET * 5;
            this.X -= STEP_MET * 6;
        }
    }
}
