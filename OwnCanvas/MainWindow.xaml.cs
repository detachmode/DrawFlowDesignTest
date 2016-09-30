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


        private void UIElement_OnMouseMove(object sender, MouseEventArgs e)
        {
            
        }


        private void OnDragDelta(object sender, DragDeltaEventArgs e)
        {
           
        }

        private void Thumb_OnDragDelta(object sender, DragDeltaEventArgs e)
        {
            var node = ((Thumb) sender).DataContext as Node;
            var pos = node.Position;
            pos.X += e.HorizontalChange;
            pos.Y += e.VerticalChange;
            node.Position = pos;
            myCanvas.InvalidateVisual();

        }
    }
}
