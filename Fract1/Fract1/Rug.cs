using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Windows;
using System.Windows.Media;

namespace Fract1
{
    /// <summary>
    /// Фрактал ковра
    /// </summary>
    class Rug : Fract
    {
        /// <summary>
        /// конструктор принимающий форму и передающий в базовый максимальную глубину и форму
        /// </summary>
        /// <param name="form"></param>
        public Rug(MainWindow form) : base(6, form)
        {

        }

        /// <summary>
        /// переопределенный метод риосование
        /// </summary>
        public override void Paint()
        {
            Point one = new(50, 50);
            Point two = new(950, 950);
            Draw(one, two, Deep);
        }

        /// <summary>
        /// рекурсивный метод
        /// </summary>
        /// <param name="one"></param>
        /// <param name="two"></param>
        /// <param name="n"></param>
        private void Draw(Point one, Point two, int n)
        {
            if (n == 0)
                return;
            if (n==Deep)
            {
                Line line = new Line();
                line.X1 = one.X;
                line.X2 = two.X;
                line.Y1 = (one.Y+two.Y)/2;
                line.Y2 = (one.Y + two.Y) / 2;
                line.Stroke = brushes[n - 1];
                line.StrokeThickness = line.X2-line.X1;
                form.image.Children.Add(line);
                Draw(new Point(one.X,one.Y), new Point(two.X, two.Y), n-1);
            }    
            if (n<Deep)
            {
                Line line = new Line();
                line.X1 = one.X+(two.X-one.X)/3;
                line.X2 = one.X + (two.X - one.X)* 2.0/ 3;
                line.Y1 = (one.Y + two.Y) / 2;
                line.Y2 = (one.Y + two.Y) / 2;
                line.Stroke = brushes[n - 1];
                line.StrokeThickness = line.X2 - line.X1;
                form.image.Children.Add(line);
                Draw(new Point(one.X, one.Y), new Point(one.X + (two.X - one.X) / 3, one.Y + (two.Y - one.Y) / 3), n - 1);
                Draw(new Point(one.X + (two.X - one.X) / 3, one.Y), new Point(one.X + (two.X - one.X) * 2.0 / 3, one.Y + (two.Y - one.Y) / 3), n - 1);
                Draw(new Point(one.X + (two.X - one.X) * 2.0 / 3, one.Y), new Point(one.X + (two.X - one.X), one.Y + (two.Y - one.Y) / 3), n - 1);
                Draw(new Point(one.X, one.Y + (two.Y - one.Y) / 3), new Point(one.X + (two.X - one.X) / 3, one.Y + (two.Y - one.Y) * 2.0 / 3), n - 1);
                Draw(new Point(one.X, one.Y + (two.Y - one.Y) * 2.0 / 3), new Point(one.X + (two.X - one.X) / 3, one.Y + (two.Y - one.Y)), n - 1);
                Draw(new Point(one.X + (two.X - one.X) / 3, one.Y + (two.Y - one.Y) * 2.0 / 3), new Point(one.X + (two.X - one.X) * 2.0 / 3, one.Y + (two.Y - one.Y)), n - 1);
                Draw(new Point(one.X + (two.X - one.X) * 2.0 / 3, one.Y + (two.Y - one.Y) * 2.0 / 3), new Point(one.X + (two.X - one.X), one.Y + (two.Y - one.Y)), n - 1);
                Draw(new Point(one.X + (two.X - one.X) * 2.0 / 3, one.Y + (two.Y - one.Y) / 3), new Point(one.X + (two.X - one.X), one.Y + (two.Y - one.Y) * 2.0 / 3), n - 1);
            }
        }
    }
}
