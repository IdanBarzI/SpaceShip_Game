using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System;
using Windows.UI.Xaml.Controls;

namespace GameNew
{
    class Player : GameElem
    {
        public const int PLAYER_SPEED = 15;

        protected override void Init()
        {
            imgUri = @"ms-appx:///Assets/Player1.png";
            base.Init();
        }

        public void PlayerLocationSpawn(Canvas canvas)
        {
            this.X = canvas.ActualWidth / 2;
            this.Y = canvas.ActualHeight - this.Hieght;
        }

        public void Move(VirtualKey key , double boardCanvasActualWidth)
        {
            if (this.X > 0 && key == VirtualKey.Left)
            {
                this.X -= PLAYER_SPEED;
            }

            else if (this.X < boardCanvasActualWidth && key == VirtualKey.Right)
            {
                this.X += PLAYER_SPEED;
            }
        }
    }
}
