using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Collections.Generic;

namespace Fractals
{
    /// <summary>
    /// Класс "Кривая Коха".
    /// </summary>
    class KochCurve : Fractal
    {
        private List<Line> elements = new List<Line>();
        private List<bool> flags = new List<bool>();
        private double size;
        private int currentIndex;

        /// <summary>
        /// Метод для создания элемента первой итерации отрисовки фрактала.
        /// </summary>
        /// <param name="startPoint">Точка, с которой начинается отрисовка фрактала.</param>
        /// <param name="size">Размер фрактала.</param>
        /// /// <param name="line">Отрезок, с которым будут проводиться операции.</param>
        /// <returns>Отрезок - элемент первой итерации отрисовки фрактала.</returns>
        private Line CreateFirstElement(Point startPoint, double size, Line line)
        {
            Point nextPoint = new Point(startPoint.X + size, startPoint.Y);
            Line newLine = new Line();
            newLine.X1 = startPoint.X;
            newLine.Y1 = startPoint.Y;
            newLine.X2 = nextPoint.X;
            newLine.Y2 = nextPoint.Y;
            line.Stroke = new SolidColorBrush(Color.FromRgb(100, 100, 100));
            elements.Add(line);
            flags.Add(true);
            currentIndex++;
            return newLine;
        }

        /// <summary>
        /// Метод создания фрактала.
        /// </summary>
        /// <param name="startPoint">Точка, с которой начинается отрисовка фрактала.</param>
        /// <param name="iteration">Текущая итерация.</param>
        /// <param name="angle">Текущий угол</param>
        /// <param name="line">Отрезок, с которым будут проводиться операции.</param>
        private void CreateFractal(Point startPoint, int iteration, double angle, Line line)
        {
            if (iteration > 0)
            {
                if (iteration == depth)
                {
                    Line newLine = CreateFirstElement(startPoint, size, line);
                    CreateFractal(new Point(line.X2, line.Y2), iteration - 1, 0, newLine);
                }
                else
                {
                    flags[currentIndex - 1] = false;
                    Line line1 = new Line() { X1 = line.X1, Y1 = line.Y1, X2 = line.X1 + (line.X2 - line.X1) / 3, Y2 = line.Y1 + (line.Y2 - line.Y1) / 3 };
                    line1.Stroke = new SolidColorBrush(Color.FromRgb(100, 100, 100));
                    elements.Add(line1);
                    flags.Add(true);
                    currentIndex++;
                    CreateFractal(new Point(line1.X2, line1.Y2), iteration - 1, angle, line1);
                    Line line2 = new Line() { X1 = line1.X2, Y1 = line1.Y2, X2 = line1.X2 + Math.Cos(angle + Math.PI / 3) * Math.Sqrt(Math.Pow(line.X2 - line.X1, 2) + Math.Pow(line.Y2 - line.Y1, 2)) / 3,
                        Y2 = line1.Y2 - Math.Sin(angle + Math.PI / 3) * Math.Sqrt(Math.Pow(line.X2 - line.X1, 2) + Math.Pow(line.Y2 - line.Y1, 2)) / 3 };
                    line2.Stroke = new SolidColorBrush(Color.FromRgb(100, 100, 100));
                    elements.Add(line2);
                    flags.Add(true);
                    currentIndex++;
                    CreateFractal(new Point(line2.X2, line2.Y2), iteration - 1, angle + Math.PI / 3, line2);
                    Line line3 = new Line() { X1 = line2.X2, Y1 = line2.Y2, X2 = line2.X2 + Math.Cos(angle - Math.PI / 3) * Math.Sqrt(Math.Pow(line.X2 - line.X1, 2) + Math.Pow(line.Y2 - line.Y1, 2)) / 3,
                        Y2 = line2.Y2 - Math.Sin(angle - Math.PI / 3) * Math.Sqrt(Math.Pow(line.X2 - line.X1, 2) + Math.Pow(line.Y2 - line.Y1, 2)) / 3 };
                    line3.Stroke = new SolidColorBrush(Color.FromRgb(100, 100, 100));
                    elements.Add(line3);
                    flags.Add(true);
                    currentIndex++;
                    CreateFractal(new Point(line3.X2, line3.Y2), iteration - 1, angle - Math.PI / 3, line3);
                    Line line4 = new Line() { X1 = line3.X2, Y1 = line3.Y2, X2 = line.X2, Y2 = line.Y2 };
                    line4.Stroke = new SolidColorBrush(Color.FromRgb(100, 100, 100));
                    elements.Add(line4);
                    flags.Add(true);
                    currentIndex++;
                    CreateFractal(new Point(line4.X2, line4.Y2), iteration - 1, angle, line4);
                }
            }
        }

        /// <summary>
        /// Метод отрисовки фрактала.
        /// </summary>
        /// <param name="startPoint">Точка, с которой начинается отрисовка фрактала.</param>
        /// <param name="depth">Глубина рекурсии.</param>
        /// <param name="param1">Первый параметр для построения фрактала.</param>
        /// <param name="param2">Второй параметр для построения фрактала.</param>
        /// <param name="param3">Третий параметр для построения фрактала.</param>
        /// <param name="size">Размер фрактала.</param>
        public override void Paint(Point startPoint, int depth, double param1, double param2, double param3, double size)
        {
            try
            {
                MainWindow.Canvas.Children.Clear();
                elements.Clear();
                flags.Clear();
                currentIndex = 0;
                this.size = size;
                this.depth = depth;
                if (this.depth <= maxDepth)
                {
                    CreateFractal(startPoint, depth, 0, new Line());
                    for (int i = 0; i < elements.Count; i++)
                    {
                        if (flags[i])
                        {
                            MainWindow.Canvas.Children.Add(elements[i]);
                        }
                    }
                }
            }
            catch { }
        }
    }
}
