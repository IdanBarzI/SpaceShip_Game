using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace GameNew
{
    class GameLogic
    {
        private const int MAX_SCORE_TO_WIN = 50;

        private const int MAX_DAMAGE_TO_LOSE = 5;

        public int Score = 0;

        public int Damage = 0;

        public bool IsCollition(GameElem elem1, GameElem elem2)
        {
            return (Math.Abs(elem1.X - elem2.X) < elem2.Width / 2 && Math.Abs(elem1.Y - elem2.Y) < elem2.Hieght / 2);
        }

        public void AddScoreUntilWin(int score)
        {
            Score += score;
            if (IsWin())
            {
                Score = MAX_SCORE_TO_WIN;
            }
        }

        public bool IsWin()
        {
            return Score >= MAX_SCORE_TO_WIN;
        }

        public void AddDamageUntilLose(int damage)
        {
            Damage += damage;
            if (IsLose())
            {
                Damage = MAX_DAMAGE_TO_LOSE;
            }
        }

        public bool IsLose()
        {
            return Damage >= MAX_DAMAGE_TO_LOSE;
        }


    }
}
