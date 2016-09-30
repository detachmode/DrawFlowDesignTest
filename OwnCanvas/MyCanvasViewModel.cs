using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using PropertyChanged;

namespace OwnCanvas
{
    [ImplementPropertyChanged]
    public class MyCanvasViewModel
    {
        public ObservableCollection<Node> Nodes { get; set; }
        public ObservableCollection<Connection> Connections { get; set; }


        private Node AddNew(double x, double y)
        {
            var n1 = new Node(new Point(x,y));
            Nodes.Add(n1);
            return n1;
        }


        private void AddNewAndConnect(double x1, double y1, double x2, double y2)
        {
            var n1 = AddNew(x1, y1);
            var n2 = AddNew(x2, y2);
            var c1 = new Connection(n1,n2);
            Connections.Add(c1);
        }


        public MyCanvasViewModel()
        {
            Nodes = new ObservableCollection<Node>();
            Connections = new ObservableCollection<Connection>();
            AddNewAndConnect(0, 0, 100, 100);
            AddNewAndConnect(100, 200, 300, 400);


        }
    }

    [ImplementPropertyChanged]
    public class Connection
    {
        public Connection(Node start, Node end)
        {
            Start = start;
            End = end;
            Name = "Parameter";
        }
        public Node Start { get; set; }
        public Node End { get; set; }

        public string Name { get; set; }
    }


    [ImplementPropertyChanged]
    public class Node
    {
        public Node(Point position)
        {
            Position = position;
        }
        public Point Position { get; set; }
    }

}
