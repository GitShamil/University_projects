using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;

namespace Fractals
{
    /// <summary>
    /// Абстрактный класс "Фрактал".
    /// </summary>
    abstract class Fractal
    {
        protected const int maxDepth = 13;
        protected int depth;

        /// <summary>
        /// Метод построения фрактала.
        /// </summary>
        /// <param name="startPoint">Точка, с которой начинается отрисовка фрактала.</param>
        /// <param name="depth">Глубина рекурсии.</param>
        /// <param name="param1">Первый параметр для построения фрактала.</param>
        /// <param name="param2">Второй параметр для построения фрактала.</param>
        /// <param name="param3">Третий параметр для построения фрактала.</param>
        /// <param name="size">Размер фрактала.</param>
        public abstract void Paint(Point startPoint, int depth, double param1, double param2, double param3, double size);
    }
}
