using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OwnCanvas
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        

        public MainWindow()
        {
            InitializeComponent();
        }


        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {



        }


        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //DragDrop.DoDragDrop((DependencyObject)e.Source, "Sample", DragDropEffects.Copy);
        }

        private void Window_DragOver(object sender, DragEventArgs e)
        {
            System.Windows.Point p1 = Mouse.GetPosition(this);
            lblInfo1.Content = string.Format("Mouse.GetPosition: {0}, {1}", p1.X, p1.Y);

            System.Windows.Point p2 = e.GetPosition(this);
            lblInfo2.Content = string.Format("DragEventArgs.GetPosition: {0}, {1}", p2.X, p2.Y);
        }

        private void Thumb_OnDragDelta(object sender, DragDeltaEventArgs e)
        {
            var node = ((Thumb) sender).DataContext as Node;
            var pos = node.Position;
            pos.X += e.HorizontalChange;
            pos.Y += e.VerticalChange;
            node.Position = pos;
            //myCanvas.InvalidateVisual();

        }
    }
}
