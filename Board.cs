using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace GameNew
{
    class Board
    {
        public int EnemyNum = 0;
        public int Damage = 0;
        private Canvas boardCanvas;
        private TextBlock scoretxt;
        private TextBlock dmgtxt;

        public Board(Canvas cnvs, TextBlock damageText, TextBlock scoreText)
        {
            this.scoretxt = scoreText;
            this.dmgtxt = damageText;
            boardCanvas = cnvs;
        }

        public void AddElement(GameElem element)
        {
            boardCanvas.Children.Add(element.Element);
        }

        public bool IsOutOfScreen(GameElem element)
        {
            return element.Y > boardCanvas.ActualHeight;
        }

        public void RemoveElement(GameElem element)
        {
            boardCanvas.Children.Remove(element.Element);
        }

        public void RemoveAll(List<GameElem> gameElems)
        {
            foreach (var gameElem in gameElems)
            {
                boardCanvas.Children.Remove(gameElem.Element);
            }
        }

        public void RemoveAll(List<Enemy> meterorits)
        {
            foreach (var gameelem in meterorits)
            {
                boardCanvas.Children.Remove(gameelem.Element);
            }
        }

        public void RemoveAll(List<Shot> shots)
        {
            foreach (var gameelem in shots)
            {
                boardCanvas.Children.Remove(gameelem.Element);
            }
        }

        public void UpdateScoreText(int score)
        {
            scoretxt.Text = score.ToString();
        }

        public void UpdateDamageText(int damage)
        {
            dmgtxt.Text = damage.ToString();
        }

        public void UpdatetBackGround(string uri)
        {
            ImageBrush bg = new ImageBrush();
            bg.ImageSource = new BitmapImage(new Uri(uri, UriKind.Absolute));
            boardCanvas.Background = bg;
        }
    }
}
