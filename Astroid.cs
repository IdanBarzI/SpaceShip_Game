using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameNew
{
    class Astroid : Enemy
    {
        private int step;

        public Astroid(double actualWidth)
        {
            this.Score = 1;
            this.Damage = 1;
            Random rand = new Random();
            int num = rand.Next(0, (int)actualWidth);
            this.X = num;
            this.Y = 0;       //actual Height
        }

        protected override void Init()
        {
            Random rand = new Random();
            int astroSpriteCounter = rand.Next(1, 4);
            switch (astroSpriteCounter)
            {
                case 1:
                    imgUri = @"ms-appx:///Assets/Astro1.png";
                    break;
                case 2:
                    imgUri = @"ms-appx:///Assets/Astro2.png"; ;
                    break;
                default:
                    imgUri = @"ms-appx:///Assets/Astro2.png";  //happens twice more 
                    break;
            }
            step = new Random().Next(9, 20);
            base.Init();
        }

        public override void MoveElem()
        {
            this.Y += step;
        }

    }
}

