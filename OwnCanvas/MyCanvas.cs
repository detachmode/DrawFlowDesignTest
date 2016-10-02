using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace OwnCanvas
{

    public class MyCanvas : Canvas
    {
        public MyCanvas()
        {           
            DataContextChanged += (sender, args) => InvalidateVisual();
            Ellipse el = new Ellipse
            {
                Width = 100,
                Height = 50,
                Fill = Brushes.Green
            };
            Canvas.SetTop(el, 30);
            Canvas.SetLeft(el, 40);
            el.MouseDown += ElOnMouseDown;
            this.Children.Add(el);

        }

        private void ElOnMouseDown(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            throw new NotImplementedException();
        }

        private void RectOnMouseDown(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            throw new NotImplementedException();
        }

        private const int arrowLength = 30;
        private const int arrowHalfHeight = 5;
        private const int connectionExtensionLength = 100;
        private readonly Pen _pen = new Pen();
        private readonly Pen _pen2 = new Pen();

        private MyCanvasViewModel GetViewModel() => DataContext as MyCanvasViewModel;



        private PathGeometry GetLine(DrawingContext dc, Point start, Point end, out Point centerPoint)
        {
            end.X -= arrowLength;
            var figure = new PathFigure
            {
                StartPoint = start,
                IsClosed = false
            };
            var startextend = new Point(start.X+ connectionExtensionLength, start.Y);
            var endextend = new Point(end.X- connectionExtensionLength, end.Y);
            figure.Segments.Add(new BezierSegment(startextend,endextend, end, true));

            
            Point tg;
            var path = new PathGeometry();
            path.Figures.Add(figure);

            path.GetPointAtFractionLength(0.5, out centerPoint, out tg);
            

            return path;
        }
        

        private void DrawConnections(DrawingContext dc)
        {
            var viewmodel = GetViewModel();

            _pen.Thickness = 3;
            _pen.Brush = new SolidColorBrush(Colors.DodgerBlue);
            var cons = viewmodel.Connections;
            cons.ToList().ForEach(con => DrawConnection(dc,con));          

           


            //cons.ToList().ForEach( con => AddParameter(dc, con));

        }

        private void DrawConnection(DrawingContext dc, Connection con)
        {
            Point centerPoint;

            dc.DrawGeometry(null, _pen, GetLine(dc, con.Start.Position, con.End.Position, out centerPoint));
            //dc.DrawEllipse(null, _pen, centerPoint, 3, 12);
            dc.DrawGeometry(_pen.Brush, _pen, GetArrowHeads(con.End.Position));
            centerPoint.Y -= 0;
            con.Center = centerPoint;
            double xDiff = (con.End.Position.X- connectionExtensionLength) - (con.Start.Position.X );
            double yDiff = con.End.Position.Y - con.Start.Position.Y;
            con.AngleText = Math.Atan2(yDiff, xDiff) * 180.0 / Math.PI;
            


            //dc.DrawEllipse(new SolidColorBrush(Colors.Black), _pen, centerPoint, 10,10);

            //var formText = new FormattedText(con.Name,
            //    CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Consolas"), 12,
            //    new SolidColorBrush(Colors.AliceBlue), null, TextFormattingMode.Display);
            //dc.DrawText(formText, centerPoint);
        }

        private PathGeometry GetArrowHeads(Point position)
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
            var path = new PathGeometry();
            path.Figures.Add(figure);
                        
            return path;
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
