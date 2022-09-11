using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Fract1
{
    /// <summary>
    /// абстрактный класс фракталов
    /// </summary>
    abstract class Fract
    {
        /// <summary>
        /// глубина 
        /// </summary>
        int deep;
        /// <summary>
        /// максимальная глубина
        /// </summary>
        static int maxValue;
        /// <summary>
        /// поле для хранение обьекта формы
        /// </summary>
        public MainWindow form;

        /// <summary>
        /// событие для глубины
        /// </summary>
        public int Deep 
        {
            get
            {
                return deep;
            }
            set
            {
                if (value > maxValue | value<=0)
                    throw new ArgumentException($"Максимальное значение глубины = {maxValue} \n" +
                        $"Минимальное значение = 1");
                deep = value;
            }
        }
        /// <summary>
        /// массив цветов градиента
        /// </summary>
       public Brush[] brushes;
        
        /// <summary>
        /// массив цветов из комбобокса
        /// </summary>
        static int[,] colors = { { 255, 0, 0 } ,{ 0, 0, 255 }, { 0, 255, 0 }, { 255, 255, 0 }, { 128, 0, 128 },
        { 0, 0, 0 } ,{ 255, 255, 255 }};

        /// <summary>
        /// конструктор фрактала
        /// </summary>
        /// <param name="max"></param>
        /// <param name="form"></param>
        public Fract(int max, MainWindow form)
        {
            this.form = form;
            maxValue = max;
            if (int.TryParse(form.textbox1.Text, out int deep))
            Deep = deep;
            else
                throw new ArgumentException($"глубина должно быть число");
            GetGradien();
            form.image.Children.Clear();
        }

        /// <summary>
        /// получение градиента
        /// </summary>
        private void GetGradien()
        {
            brushes = new Brush[Deep];
               int firstColor = form.comboBoxColor1.SelectedIndex;
              int   secondColor = form.comboBoxColor2.SelectedIndex;
            int[] addToFirstColor;
            if (Deep!=1)
            {
                 addToFirstColor = new int[]{
                (colors[secondColor,0]-colors[firstColor,0])/(Deep-1),
                 (colors[secondColor,1]-colors[firstColor,1])/(Deep-1),
                  (colors[secondColor,2]-colors[firstColor,2])/(Deep-1)
            };
            }
            else
            {
                 addToFirstColor = new int[]  { 0, 0, 0 };
            }
           
            int[] startColor = {
                colors[firstColor,0],
                 colors[firstColor,1],
                 colors[firstColor,2]
            };
            for (int i = brushes.Length-1; i >=0; i--)
            {
                int R = startColor[0];
                int G = startColor[1];
                int B = startColor[2];
                brushes[i] = new SolidColorBrush(Color.FromArgb(255,
                    (byte)(R), (byte)(G), (byte)(B)));
                startColor[0] += addToFirstColor[0];
                startColor[1] += addToFirstColor[1];
                startColor[2] += addToFirstColor[2];
            }   
        }

        /// <summary>
        /// абстрактный класс отрисовки фрактала
        /// </summary>
        public abstract void Paint();
    }
}
