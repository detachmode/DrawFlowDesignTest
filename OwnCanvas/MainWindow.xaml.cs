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

        private SolidColorBrush hoverfeedbackBrush = new SolidColorBrush(Colors.GreenYellow);

        private void softwareCell_DragOver(object sender, DragEventArgs e)
        {
            var rect = (e.Source as Rectangle);
            if (rect == null) return;
            hoverfeedbackBrush.Opacity = 0.3;
            rect.Fill = hoverfeedbackBrush;
            
            SetPointerToMousePosition(e);
        }


        private void SetPointerToMousePosition(DragEventArgs e)
        {
            Point p2 = e.GetPosition(this);      
            var obj = e.Data.GetData(e.Data.GetFormats()[0]);
            if (!(obj is Pointer)) return;
            
            var pointer = (obj as Pointer);
            var datacontext = (PointerViewModel)pointer.DataContext;

            pointer.End = p2;
        }
    

        private void SoftwareCell_Drop(object sender, DragEventArgs e)
        {
            var obj = e.Data.GetData(e.Data.GetFormats()[0]);
            if (obj is Pointer)
            {
                var pointer = (obj as Pointer);
                var datacontext = (PointerViewModel)pointer.DataContext;
                var droppedContext = (Node) ((Rectangle) sender).DataContext;
                pointer.SetBinding((DependencyProperty) Pointer.EndProperty, "End.Position");
                datacontext.End = droppedContext;

            }
            UndoDragOverFeedback(e);

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

        private void MainWindow_OnDragOver(object sender, DragEventArgs e)
        {
            SetPointerToMousePosition(e);
        }


        private void SoftwareCell_DragLeave(object sender, DragEventArgs e)
        {
            UndoDragOverFeedback(e);
        }


        private static void UndoDragOverFeedback(DragEventArgs e)
        {
            var rect = (e.Source as Rectangle);
            if (rect == null) return;
            rect.Fill = Brushes.Transparent;
        }
    }
}
