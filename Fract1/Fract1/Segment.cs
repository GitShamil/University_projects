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
    /// фрактал сегментов
    /// </summary>
    class Segment : Fract
    {

        /// <summary>
        /// поле хранящее отношение длины сегмента и пустоты между ними
        /// </summary>
        double segmentDividedByEmpty = 1;

        /// <summary>
        /// св-во для segmentDividedByEmpty 
        /// </summary>
        double SegmentDividedByEmpty
        {
            get
            {
                return segmentDividedByEmpty;
            }
            set
            {
                if (value < 0.2 | value > 10)
                    throw new ArgumentException($"Отношение должен быть числом от 0,2 до 10");
                segmentDividedByEmpty = value;
            }
        }

        /// <summary>
        /// поле хранящее отношение длины расстояния между итерациями
        /// </summary>
        double lengBetweenCurrentandBefore = 1;

        /// <summary>
        /// св-во для lengBetweenCurrentandBefore
        /// </summary>
        double LengBetweenCurrentandBefore
        {
            get
            {
                return lengBetweenCurrentandBefore;
            }
            set
            {
                if (value < 0.5 | value > 5)
                    throw new ArgumentException($"Отношение должен быть числом от 0,5 до 5");
                lengBetweenCurrentandBefore = value;
            }
        }

        /// <summary>
        /// конструктор принимающий форму и передающий в базовый максимальную глубину и форму
        /// </summary>
        /// <param name="form"></param>
        public Segment(MainWindow form) : base(12, form)
        {
            if (!double.TryParse(form.textbox2.Text, out double temp1))
                throw new ArgumentException($"Отношение должен быть числом");
            SegmentDividedByEmpty = temp1;
            if (!double.TryParse(form.textbox3.Text, out double temp2))
                throw new ArgumentException($"Рассояние между итерациями должно быть числом");
            LengBetweenCurrentandBefore = temp2;
        }

        /// <summary>
        /// переопределенный метод риосование
        /// </summary>
        public override void Paint()
        {
            Point one = new(50, 100);
            Point two = new(950, 100);
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
           
                double leng = two.X - one.X;
                Line line = new Line();
                line.X1 = one.X;
                line.X2 = two.X;
                line.Y1 = one.Y;
                line.Y2 = two.Y;
                line.Stroke = brushes[n - 1];
                line.StrokeThickness = leng / 10;
                form.image.Children.Add(line);
                Draw(new Point(line.X1, line.Y1 + LengBetweenCurrentandBefore *leng / 10), new Point(line.X1 + leng / (2+1/SegmentDividedByEmpty), line.Y1 + LengBetweenCurrentandBefore * leng / 10), n - 1);
            Draw(new Point(line.X2 - leng / (2 + 1 / SegmentDividedByEmpty), line.Y1 + LengBetweenCurrentandBefore * leng / 10), new Point(line.X2, line.Y1 + LengBetweenCurrentandBefore * leng / 10), n - 1);

        }
        public static void ChangeMenu(MainWindow form)
        {
            form.label2.Visibility = Visibility.Visible;
            form.textbox2.Visibility = Visibility.Visible;
            form.label2.Content = $"Отрезок/пустоту \n от 0,2 до 10";
            form.textbox2.Text = "1";
            form.label3.Visibility = Visibility.Visible;
            form.textbox3.Visibility = Visibility.Visible;
            form.label3.Content = $"Расст. между \n итерациями \n от 0,5 до 5";
            form.textbox3.Text = "1";
        }
    }
}
