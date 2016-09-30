using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace OwnCanvas
{

    public class MyCanvas : Canvas
    {
        public MyCanvas()
        {           
            DataContextChanged += (sender, args) => InvalidateVisual();
        }



        private readonly Pen _pen = new Pen();
        private readonly Pen _pen2 = new Pen();

        private MyCanvasViewModel GetViewModel() => DataContext as MyCanvasViewModel;


        private PathFigure GetLine(Point start, Point end)
        {
            var figure = new PathFigure
            {
                StartPoint = start,
                IsClosed = false
            };

            figure.Segments.Add(new LineSegment(end, true));
            return figure;
        }
        

        private void DrawConnections(DrawingContext dc)
        {
            var viewmodel = GetViewModel();
            var path = new PathGeometry();
            var cons = viewmodel.Connections;

       

            cons.ToList().ForEach(x => path.Figures.Add(GetLine(x.Start.Position, x.End.Position)));

            _pen.Thickness = 3;
            _pen.Brush = new SolidColorBrush(Colors.Wheat);
            dc.DrawGeometry(null, _pen, path);

            cons.ToList().ForEach( con => AddParameter(dc, con));

        }


        private void AddParameter(DrawingContext dc, Connection con)
        {
           // dc.Draw
        }


        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);
            DrawConnections(dc);
            
            //DrawNodes(dc);

            
        }


        private void DrawNodes(DrawingContext dc)
        {
            var viewmodel = GetViewModel();
            var nodes = viewmodel.Nodes;

            nodes.ToList().ForEach(x =>
                dc.DrawEllipse(Brushes.OrangeRed, _pen2, x.Position, 20, 20));
            

        }
    }
}
