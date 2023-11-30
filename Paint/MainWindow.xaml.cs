using System;
using System.Collections.Generic;
using System.Linq;
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

namespace Paint
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private Point? startPoint = null;

        private void InkCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            startPoint = e.GetPosition((IInputElement)sender);
        }

        private void InkCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            startPoint = null;
            ellipse = null;
        }
        private Line line;
        private Ellipse ellipse;
        private ToolType toolType = ToolType.Ellipse;
        
        private void InkCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            var currentPoint = e.GetPosition((IInputElement)sender);
            if (toolType == ToolType.Pen)
            {
                if (startPoint is { } point)
                {
                    line = new Line();
                    line.Stroke = Brushes.GreenYellow;
                    line.StrokeThickness = 4;
                    line.X1 = point.X;
                    line.Y1 = point.Y;
                    line.X2 = currentPoint.X;
                    line.Y2 = currentPoint.Y;
                    MyCanvas.Children.Add(line);
                    startPoint = currentPoint;
                }
            }
            else
            {
                if (startPoint is {  } point)
                {
                    if (ellipse == null)
                    {
                        ellipse = new Ellipse();
                        ellipse.Stroke = Brushes.Black;
                        ellipse.StrokeThickness = 2;
                        ellipse.Fill = Brushes.Red;
                        MyCanvas.Children.Add(ellipse);
                    }
                    var r = new Rect(point, currentPoint);
                    ellipse.Margin = new Thickness(r.X, r.Y, 0, 0);
                    ellipse.Width = r.Width;
                    ellipse.Height = r.Height;
                
                }
            }
            
        }

    }
    enum ToolType
    {
        Pen, Ellipse
    }
}
