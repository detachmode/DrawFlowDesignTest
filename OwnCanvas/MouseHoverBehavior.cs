using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls;
using System.Windows.Interactivity;
using System.Windows.Media;
using System.Windows.Shapes;

namespace OwnCanvas
{
    public class SelectAble : Behavior<Ellipse>
    {
        private SolidColorBrush defaultBrush;
        private readonly SolidColorBrush hoverBrush = new SolidColorBrush(Colors.Yellow);

        protected override void OnAttached()
        {
            //AssociatedObject.MouseEnter +=
            //    (sender, args) =>
            //    {
            //        AssociatedObject.Background = hoverBrush;
            //        defaultBrush = (SolidColorBrush)AssociatedObject.Stroke;
            //        AssociatedObject.Stroke = hoverBrush;
            //        //AssociatedObject.StrokeThickness *= 1.5;
            //        //AssociatedObject.StrokeDashArray = new DoubleCollection() {3};
            //    };

            //AssociatedObject.MouseLeave += (sender, args) =>
            //{
            //    ////AssociatedObject.StrokeThickness /= 1.5;
            //    //AssociatedObject.StrokeDashArray = null;
            //    AssociatedObject.Stroke = defaultBrush;
            //};


            AssociatedObject.MouseDown += (sender, args) =>
            {
                AssociatedObject.Fill = hoverBrush;
            };
        }
    }
}
