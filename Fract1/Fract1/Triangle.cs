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
    /// фрактал триугольников 
    /// </summary>
    class Triangle : Fract
    {

        /// <summary>
        /// конструктор триугольников
        /// </summary>
        /// <param name="form">WPF форма</param>
        public Triangle(MainWindow form) : base(9, form)
        {

        }

        /// <summary>
        /// override method for paint fract
        /// </summary>
        public override void Paint()
        {
            Point one = new(50, 50);
            Point two = new(500, 50+900*Math.Sqrt(3)/2);
            Point three = new Point(950,50);
            PointCollection points = new PointCollection() { one, two, three };
            Draw(points, Deep);
        }

        /// <summary>
        /// method which paint fract 
        /// </summary>
        /// <param name="points"></param>
        /// <param name="n"></param>
        private void Draw(PointCollection points, int n)
        {
            if (n == 0)
                return;
            if (n == Deep)
            {
                Polygon tr = new Polygon();
                tr.Points = points;
                tr.Stroke = brushes[n - 1];
                form.image.Children.Add(tr);
                Draw(points,n-1);          
            }
            if (n < Deep)
            {
                Point one = new((points[0].X+points[1].X)/2, (points[0].Y + points[1].Y) / 2);
                Point two = new((points[2].X + points[1].X) / 2, (points[2].Y + points[1].Y) / 2);
                Point three = new Point((points[0].X + points[2].X) / 2, (points[0].Y + points[2].Y) / 2);
                Polygon tr = new Polygon();
                PointCollection points2 = new PointCollection() { one, two, three };
                tr.Points = points2;
                tr.Fill = brushes[n - 1];
                form.image.Children.Add(tr);
                points2 = new PointCollection() {points[0],one,three };
                Draw(points2, n - 1);
                points2 = new PointCollection() { one,points[1] ,two };
                Draw(points2, n - 1);
                points2 = new PointCollection() { three,two,points[2] };
                Draw(points2, n - 1);
            }
        }
    }
}
