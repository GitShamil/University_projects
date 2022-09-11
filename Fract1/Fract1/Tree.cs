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
    /// фрактал дерева
    /// </summary>
    class Tree : Fract
    {
        /// <summary>
        /// левый угол 
        /// </summary>
        double cornorLeft = Math.PI / 4;

        /// <summary>
        /// св-во для левого угла от 0% до 33% от Pi
        /// </summary>
        double CornorLeft
        {
            get
            {
                return cornorLeft;
            }
            set
            {
                if (value < 0 | value > 33)
                    throw new ArgumentException($"Левый угол от 0 до 33 процентов от pi");
                cornorLeft = value * 0.01 * Math.PI;
            }
        }

        /// <summary>
        /// левый угол 
        /// </summary>
        double cornorRight = Math.PI / 4;

        /// <summary>
        /// св-во для левого угла от 0% до 33% от Pi
        /// </summary>
        double CornorRight
        {
            get
            {
                return cornorRight;
            }
            set
            {
                if (value < 0 | value > 33)
                    throw new ArgumentException($"Правый угол от 0 до 33 процентов от pi");
                cornorRight = value * 0.01 * Math.PI;
            }
        }

        /// <summary>
        /// Отношение текущего к следующему
        /// </summary>
        double nowDevidedbyNext = 1.5;

        /// <summary>
        /// св-во отношения текущего к след
        /// </summary>
        double NowDevidedbyNext
        {
            get
            {
                return nowDevidedbyNext;
            }
            set
            {
                if (value < 1.4 | value > 5)
                    throw new ArgumentException($"Отношение текущего к следующему от 1.4 до 5");
                nowDevidedbyNext=value;
            }
        }

        /// <summary>
        /// конструктор
        /// </summary>
        /// <param name="form">WPF форма</param>
        public Tree(MainWindow form) : base(12,form)
        {
            if (!double.TryParse(form.textbox2.Text, out double temp1))
                throw new ArgumentException($"Левый угол должен быть числом");
            CornorLeft = temp1;
            if (!double.TryParse(form.textbox3.Text, out double temp2))
                throw new ArgumentException($"Правый угол должен быть числом");
            CornorRight = temp2;
            if (!double.TryParse(form.textbox4.Text, out double temp3))
                throw new ArgumentException($"Отношение должен быть числом");
            NowDevidedbyNext = temp3;
        }

        /// <summary>
        /// переопределенный метод
        /// </summary>
        public override void Paint()
        {
            Point one = new(500,100);
            Point two = new(500,350);
            Draw(one, two, Deep);
        }

        /// <summary>
        /// метод для рисования
        /// </summary>
        /// <param name="one"></param>
        /// <param name="two"></param>
        /// <param name="n"></param>
        private void Draw(Point one, Point two, int n)
        {
            double leng = 1.0 / NowDevidedbyNext * Math.Sqrt(Math.Pow(two.X - one.X, 2) + Math.Pow(two.Y - one.Y, 2));
            if (n != 0 & leng > 1)
            {
                Line line = new Line();
                line.X1 = one.X;
                line.X2 = two.X;
                line.Y1 = one.Y;
                line.Y2 = two.Y;
                line.Stroke = brushes[n-1] ;
                line.StrokeThickness = leng / 5;
                form.image.Children.Add(line);
                double a = Math.Atan2((two.Y - one.Y), (two.X - one.X));
                Draw(new Point(two.X, two.Y),
                    new Point(two.X + leng * Math.Cos(a + CornorLeft), two.Y + leng * Math.Sin(a + CornorLeft)), n - 1);
                Draw(new Point(two.X, two.Y),
                     new Point(two.X + leng * Math.Cos(a - CornorRight), two.Y + leng * Math.Sin(a - CornorRight)), n - 1);
            }
        }

        /// <summary>
        /// статический метод помогающий слелать нужные поля видимыми
        /// </summary>
        /// <param name="form"></param>
        public static void ChangeMenu(MainWindow form)
        {
            form.label2.Visibility = Visibility.Visible;
            form.textbox2.Visibility = Visibility.Visible;
            form.label3.Visibility = Visibility.Visible;
            form.textbox3.Visibility = Visibility.Visible;
            form.label4.Visibility = Visibility.Visible;
            form.textbox4.Visibility = Visibility.Visible;
            form.label2.Content = $"Левый угол \n от 0 до 33";
            form.label3.Content = "Правый угол \n от 0 до 33";
            form.label4.Content = "Отношение \n от 1,4 до 5";
            form.textbox2.Text = "15";
            form.textbox3.Text = "25";
            form.textbox4.Text = "1,5";
        }
    }
}
