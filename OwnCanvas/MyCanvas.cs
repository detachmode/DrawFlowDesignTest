using System;
using System.Collections.Generic;
using System.Globalization;
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

        private const int arrowLength = 30;
        private const int arrowHalfHeight = 5;
        private readonly Pen _pen = new Pen();
        private readonly Pen _pen2 = new Pen();

        private MyCanvasViewModel GetViewModel() => DataContext as MyCanvasViewModel;


        private PathFigure GetLine(Point start, Point end)
        {
            end.X -= arrowLength;
            var figure = new PathFigure
            {
                StartPoint = start,
                IsClosed = false
            };
            var startextend = new Point(start.X+100,start.Y);
            var endextend = new Point(end.X-100,end.Y);
            figure.Segments.Add(new BezierSegment(startextend,endextend, end, true));

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

            path = new PathGeometry();
            cons.ToList().ForEach(x => path.Figures.Add(GetArrowHeads(x.End.Position)));
            dc.DrawGeometry(_pen.Brush, _pen, path);


            cons.ToList().ForEach( con => AddParameter(dc, con));

        }

        private PathFigure GetArrowHeads(Point position)
        {
            position.X -= arrowLength;
            var figure = new PathFigure
            {
                StartPoint = position,
                IsClosed = true
            };
            var pts = new List<Point>
            {
                new Point(position.X, position.Y - arrowHalfHeight),
                new Point(position.X + arrowLength, position.Y),
                new Point(position.X, position.Y + arrowHalfHeight)
            };
            figure.Segments.Add(new PolyLineSegment(pts, true));
            return figure;
        }

        private void AddParameter(DrawingContext dc, Connection con)
        {
            var delta = 100;
           //dc.DrawText(new FormattedText("hello", CultureInfo.CurrentCulture, FlowDirection.LeftToRight ), new Point(con.Start.Position.X + delta, con.Start.Position.Y+delta));
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
