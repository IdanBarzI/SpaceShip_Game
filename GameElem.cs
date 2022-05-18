using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace GameNew
{
    public class GameElem
    {
        protected string imgUri = "";

        public Image Element { get; set; }

        public double X
        {
            get
            {
                return Canvas.GetLeft(Element);
            }
            set
            {
                Canvas.SetLeft(Element, value);
            }
        }

        public double Y
        {
            get
            {
                return Canvas.GetTop(Element);
            }

            set
            {
                Canvas.SetTop(Element, value);
            }
        }

        public double Hieght
        {
            get
            {
                return Element.ActualHeight;
            }
            set
            {
                Element.Height = value;
            }
        }

        public double Width
        {
            get
            {
                return Element.ActualWidth;
            }
            set
            {
                Element.Width = value;
            }
        }

        public GameElem()
        {
            Element = new Image();
            Element.Stretch = Windows.UI.Xaml.Media.Stretch.Fill;
            Hieght = 100;
            Width = 100;
            Init();
        }

        protected virtual void Init()
        {
            Element.Source = new BitmapImage(new Uri(imgUri));
        }

        public virtual void MoveElem()
        {

        }

        public void UpdatetImage(string uri)
        {
            Element.Source = new BitmapImage(new Uri(uri));
        }

        
    }
}
