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
    /// класс для рисование фрактал Кохи
    /// </summary>
    class KochCurve : Fract
    {
        /// <summary>
        /// конструктор принимающий форму и передающий в базовый максимальную глубину и форму
        /// </summary>
        /// <param name="form"></param>
        public KochCurve(MainWindow form) : base(8, form)
        {

        }

        /// <summary>
        /// переопределенный метод риосование
        /// </summary>
        public override void Paint()
        {
            Point one = new(20, 100);
            Point two = new(980, 100);
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
            if (n == 0 )
                return;
            if (n==Deep)
            {
                Line line = new Line();
                line.X1 = one.X;
                line.X2 = two.X;
                line.Y1 = one.Y;
                line.Y2 = two.Y;
                line.Stroke = brushes[n-1];
                line.StrokeThickness = 2;
                form.image.Children.Add(line);
                Draw(one, two, Deep - 1);
            }
            if (n<Deep)
            {
                double a = Math.Atan2((two.Y - one.Y), (two.X - one.X));
                double leng = 1.0 / 3 * Math.Sqrt(Math.Pow(two.X - one.X, 2) + Math.Pow(two.Y - one.Y, 2));
                Line line = new Line();
                line.X1 = one.X+(two.X-one.X)/3;
                line.X2 = two.X- (two.X - one.X) / 3;
                line.Y1 = one.Y + (two.Y - one.Y) / 3;
                line.Y2 = two.Y - (two.Y - one.Y) / 3;
                line.Stroke = Brushes.White;
                line.StrokeThickness = 3;
                form.image.Children.Add(line);
                Draw(new Point(one.X, one.Y), new Point(line.X1, line.Y1), n - 1);
                Draw(new Point(line.X2, line.Y2), new Point(two.X, two.Y), n - 1);
                line = new Line();
                line.X1 = one.X + (two.X - one.X) / 3;
                line.X2 = line.X1 + leng * Math.Cos(a + Math.PI / 3);
                line.Y1 = one.Y + (two.Y - one.Y) / 3;
                line.Y2 = line.Y1 + leng * Math.Sin(a + Math.PI / 3);
                line.Stroke = brushes[n - 1];
                line.StrokeThickness = 2;
                form.image.Children.Add(line);
                Draw(new Point(line.X1,line.Y1), new Point(line.X2, line.Y2), n-1);
                Line line2 = new Line();
                line2.X1 = line.X2;
                line2.Y1 = line.Y2;
                line2.X2 = two.X - (two.X - one.X) / 3;
                line2.Y2 = two.Y - (two.Y - one.Y) / 3;
                line2.Stroke = brushes[n - 1];
                line.StrokeThickness = 2;
                form.image.Children.Add(line2);
                Draw(new Point(line2.X1, line2.Y1), new Point(line2.X2, line2.Y2), n - 1);    
            }

        }
    }
}
