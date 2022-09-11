using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;


namespace Fract1
{
    /// <summary>
    /// Класс формы 
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// конструктор, в котором выбирается изначальные цвета градиента
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            comboBoxColor1.SelectedIndex = 1;
            comboBoxColor2.SelectedIndex = 3;
        }

        /// <summary>
        /// запуск рендеринга рисунка
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonRender_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Fract fract = null;
                if (comboBoxOfType.SelectedIndex == 0)
                    fract = new Tree(this);
                if (comboBoxOfType.SelectedIndex == 1)
                    fract = new KochCurve(this);
                if (comboBoxOfType.SelectedIndex == 2)
                    fract = new Rug(this);
                if (comboBoxOfType.SelectedIndex == 3)
                    fract = new Triangle(this);
                if (comboBoxOfType.SelectedIndex == 4)
                    fract = new Segment(this);
                fract.Paint();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// события при нажатие на кнопку сохранения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog a = new SaveFileDialog();
            a.DefaultExt = ".PNG";
            a.Filter = "Image (.PNG)|*.PNG";
            if (a.ShowDialog() == true)
            {
                ToImageSource(image, a.FileName);
            }
        }

        /// <summary>
        /// само сохранение рисунка
        /// </summary>
        /// <param name="canvas"></param>
        /// <param name="filename"></param>
        public static void ToImageSource(Canvas canvas, string filename)
        {
            RenderTargetBitmap bmp = new RenderTargetBitmap((int)canvas.ActualWidth, (int)canvas.ActualHeight, 96d, 96d, PixelFormats.Pbgra32);
            canvas.Measure(new Size((int)canvas.ActualWidth, (int)canvas.ActualHeight));
            canvas.Arrange(new Rect(new Size((int)canvas.ActualWidth, (int)canvas.ActualHeight)));
            bmp.Render(canvas);
            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bmp));
            using (FileStream file = File.Create(filename))
            {
                encoder.Save(file);
            }
        }

        /// <summary>
        /// событие при изменение выбранного фрактала
        /// Нужно для отрисовки нужных полей для параметров у фрактала
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxOfType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            label2.Visibility = Visibility.Hidden;
            textbox2.Visibility = Visibility.Hidden;
            label3.Visibility = Visibility.Hidden;
            textbox3.Visibility = Visibility.Hidden;
            label4.Visibility = Visibility.Hidden;
            textbox4.Visibility = Visibility.Hidden;
            if (comboBoxOfType.SelectedIndex==0)
            {
                Tree.ChangeMenu(this);
            }
            if (comboBoxOfType.SelectedIndex == 4)
                Segment.ChangeMenu(this);
        }
    }
}
