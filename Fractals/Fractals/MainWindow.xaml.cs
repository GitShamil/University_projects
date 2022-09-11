using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Fractals
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Canvas Canvas;
        private FractalTree fractalTree;
        private KochCurve kochCurve;
        private SierpinskiСarpet sierpinskiСarpet;
        private SierpinskiTriangle sierpinskiTriangle;
        private CantorSet cantorSet;
        private int zoomValue = 1;

        /// <summary>
        /// Конструктор класса.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            Canvas = canvas;
            fractalTree = new FractalTree();
            kochCurve = new KochCurve();
            sierpinskiСarpet = new SierpinskiСarpet();
            sierpinskiTriangle = new SierpinskiTriangle();
            cantorSet = new CantorSet();
            window.MinHeight = SystemParameters.PrimaryScreenHeight / 2;
            window.MinWidth = SystemParameters.PrimaryScreenWidth / 2;
        }

        /// <summary>
        /// Метод отрисовки выбранного фрактала.
        /// </summary>
        private void PaintSelectedFractal()
        {
            double width = window.ActualWidth;
            double height = window.ActualHeight;
            try
            {
                if (comboBox.SelectedIndex == 0)
                {
                    FractalTreeInterface();
                    fractalTree.Paint(new Point(width / 3 * zoomValue, (height - height / 6) * zoomValue),
                        (int)slider1.Value, slider2.Value * Math.PI / 180, slider3.Value * Math.PI / 180, slider4.Value / 100, Math.Min(height, width) / 6 * zoomValue);
                }
                else if (comboBox.SelectedIndex == 1)
                {
                    KochCurveInterface();
                    kochCurve.Paint(new Point(width / 12 * zoomValue, (height - height / 6) * zoomValue),
                        (int)slider1.Value + 1, 0, 0, 0, (2 * width / 3 - 2 * width / 12) * zoomValue);
                }
                else if (comboBox.SelectedIndex == 2)
                {
                    SierpinskiCarpetInterface();
                    sierpinskiСarpet.Paint(new Point(width / 12 * zoomValue, height / 24 * zoomValue),
                        (int)slider1.Value, 20, 0, 0, (height - 2 * height / 12) * zoomValue);
                }
                else if (comboBox.SelectedIndex == 3)
                {
                    SierpinskiTriangleInterface();
                    sierpinskiTriangle.Paint(new Point(width / 12 * zoomValue, (height - height / 6) * zoomValue),
                        (int)slider1.Value, 20, 0, 0, (2 * width / 3 - 2 * width / 12) * zoomValue);
                }
                else if (comboBox.SelectedIndex == 4)
                {
                    CantorSetInterface();
                    cantorSet.Paint(new Point(width / 12 * zoomValue, height / 12 * zoomValue), (int)slider1.Value, 
                        (int)slider5.Value, 0, 0, (2 * width / 3 - 2 * width / 12) * zoomValue);
                }
            }
            catch { }
        }

        /// <summary>
        /// Метод для изменения интерфейса под отображение фрактала "Фрактальное дерево".
        /// </summary>
        private void FractalTreeInterface()
        {
            slider1.Maximum = 12;
            slider1.Minimum = 1;
            gridSlider2.Visibility = Visibility.Visible;
            gridSlider3.Visibility = Visibility.Visible;
            gridSlider4.Visibility = Visibility.Visible;
            gridSlider5.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Метод для изменения интерфейса под отображение фрактала "Кривая Коха".
        /// </summary>
        private void KochCurveInterface()
        {
            slider1.Maximum = 6;
            slider1.Minimum = 1;
            gridSlider2.Visibility = Visibility.Collapsed;
            gridSlider3.Visibility = Visibility.Collapsed;
            gridSlider4.Visibility = Visibility.Collapsed;
            gridSlider5.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Метод для изменения интерфейса под отображение фрактала "Ковер Серпинского".
        /// </summary>
        private void SierpinskiCarpetInterface()
        {
            slider1.Maximum = 6;
            slider1.Minimum = 1;
            gridSlider2.Visibility = Visibility.Collapsed;
            gridSlider3.Visibility = Visibility.Collapsed;
            gridSlider4.Visibility = Visibility.Collapsed;
            gridSlider5.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Метод для изменения интерфейса под отображение фрактала "Треугольник Серпинского".
        /// </summary>
        private void SierpinskiTriangleInterface()
        {
            slider1.Maximum = 8;
            slider1.Minimum = 1;
            gridSlider2.Visibility = Visibility.Collapsed;
            gridSlider3.Visibility = Visibility.Collapsed;
            gridSlider4.Visibility = Visibility.Collapsed;
            gridSlider5.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Метод для изменения интерфейса под отображение фрактала "Множество Кантора".
        /// </summary>
        private void CantorSetInterface()
        {
            slider1.Maximum = 8;
            slider1.Minimum = 1;
            gridSlider2.Visibility = Visibility.Collapsed;
            gridSlider3.Visibility = Visibility.Collapsed;
            gridSlider4.Visibility = Visibility.Collapsed;
            gridSlider5.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Метод обработки события изменения значения slider.
        /// </summary>
        /// <param name="sender">Издатель.</param>
        /// <param name="e">Информация о событии.</param>
        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PaintSelectedFractal();
        }

        /// <summary>
        /// Метод обработки события изменения значения slider.
        /// </summary>
        /// <param name="sender">Издатель.</param>
        /// <param name="e">Информация о событии.</param>
        private void window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            PaintSelectedFractal();
        }

        /// <summary>
        /// Метод обработки события изменения значения slider.
        /// </summary>
        /// <param name="sender">Издатель.</param>
        /// <param name="e">Информация о событии.</param>
        private void slider1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            PaintSelectedFractal();
        }

        /// <summary>
        /// Метод обработки события изменения значения slider.
        /// </summary>
        /// <param name="sender">Издатель.</param>
        /// <param name="e">Информация о событии.</param>
        private void slider2_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            PaintSelectedFractal();
        }

        /// <summary>
        /// Метод обработки события изменения значения slider.
        /// </summary>
        /// <param name="sender">Издатель.</param>
        /// <param name="e">Информация о событии.</param>
        private void slider3_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            PaintSelectedFractal();
        }

        /// <summary>
        /// Метод обработки события изменения значения slider.
        /// </summary>
        /// <param name="sender">Издатель.</param>
        /// <param name="e">Информация о событии.</param>
        private void slider4_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            PaintSelectedFractal();
        }

        /// <summary>
        /// Метод обработки события изменения значения slider.
        /// </summary>
        /// <param name="sender">Издатель.</param>
        /// <param name="e">Информация о событии.</param>
        private void slider5_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            PaintSelectedFractal();
        }

        /// <summary>
        /// Метод обработки события выбора варианта увеличения фрактала.
        /// </summary>
        /// <param name="sender">Издатель.</param>
        /// <param name="e">Изформация о событии.</param>
        private void zoomComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (zoomComboBox.SelectedIndex == 0)
                {
                    zoomValue = 1;
                    Canvas.Height = mainScrollViewer.ActualHeight * zoomValue;
                    Canvas.Width = mainScrollViewer.ActualWidth * zoomValue;
                    mainScrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Disabled;
                    mainScrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled;
                }
                else if (zoomComboBox.SelectedIndex == 1)
                {
                    zoomValue = 2;
                    Canvas.Height = mainScrollViewer.ActualHeight * zoomValue;
                    Canvas.Width = mainScrollViewer.ActualWidth * zoomValue;
                    mainScrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
                    mainScrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
                }
                else if (zoomComboBox.SelectedIndex == 2)
                {
                    zoomValue = 3;
                    Canvas.Height = mainScrollViewer.ActualHeight * zoomValue;
                    Canvas.Width = mainScrollViewer.ActualWidth * zoomValue;
                    mainScrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
                    mainScrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
                }
                else if (zoomComboBox.SelectedIndex == 3)
                {
                    zoomValue = 5;
                    Canvas.Height = mainScrollViewer.ActualHeight * zoomValue;
                    Canvas.Width = mainScrollViewer.ActualWidth * zoomValue;
                    mainScrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
                    mainScrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
                }
                PaintSelectedFractal();
            }
            catch { }
        }

        /// <summary>
        /// Метод обработки события нажатия на кнопку "Сохранить".
        /// </summary>
        /// <param name="sender">Издатель.</param>
        /// <param name="e">Информация о событии.</param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.DefaultExt = ".PNG";
                saveFileDialog.Filter = ".png|*.PNG";
                if (saveFileDialog.ShowDialog() == true)
                {
                    SaveImage(saveFileDialog.FileName);
                }
            }
            catch
            {
                MessageBox.Show("Ошибка сохранения");
            }
        }

        /// <summary>
        /// Метод сохранения содержимого canvas.
        /// </summary>
        /// <param name="fileName">Имя файла для сохранения.</param>
        public static void SaveImage(string fileName)
        {
            try
            {
                double dpi = 300;
                Size size = Canvas.RenderSize;
                RenderTargetBitmap rtb = new RenderTargetBitmap((int)(size.Width * 300 / 96), (int)(size.Height * 300 / 96), dpi, dpi, PixelFormats.Pbgra32);
                rtb.Render(Canvas);
                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(rtb));
                using (FileStream file = File.Create(fileName))
                {
                    encoder.Save(file);
                }
            }
            catch
            {
                MessageBox.Show("Ошибка сохранения");
            }
        }
    }
}
