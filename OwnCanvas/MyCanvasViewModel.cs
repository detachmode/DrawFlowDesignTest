using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using OwnCanvas.Annotations;
using PropertyChanged;

namespace OwnCanvas
{
    [ImplementPropertyChanged]
    public class MyCanvasViewModel
    {
        public MyCanvasViewModel()
        {
            Connections = new ObservableCollection<PointerViewModel>();
            Connections.CollectionChanged += Connections_CollectionChanged;

            Nodes = new ObservableCollection<Node>();
           
            addNewAndConnect(0, 0, 100, 100);
            addNewAndConnect(100, 200, 300, 200);
            Connections.Add(new PointerViewModel(Nodes[1], Nodes[2]));
           


        }

        private void Connections_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action != NotifyCollectionChangedAction.Add) return;
            foreach (PointerViewModel item in e.NewItems)
            {
                item.PropertyChanged += connection_PropertyChanged;
            }
        }


        public ObservableCollection<Node> Nodes { get; set; }
        public ObservableCollection<PointerViewModel> Connections { get; set; }


        private Node addNew(double x, double y)
        {
            var n1 = new Node(new Point(x, y));
            Nodes.Add(n1);
            return n1;
        }


        private void addNewAndConnect(double x1, double y1, double x2, double y2)
        {
            var n1 = addNew(x1, y1);
            var n2 = addNew(x2, y2);
            var c1 = new PointerViewModel(n1, n2);
           
            Connections.Add(c1);
        }

        private void connection_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != "IsDragging") return; 
            var vm = (sender as PointerViewModel);

            // as long as an connection is dragged around don't change the Collections
            if (vm.IsDragging) return; 

            MaybeRemoveConnection(vm);
        }


        private void MaybeRemoveConnection(PointerViewModel vm)
        {
            if (vm.End == null)
                this.Connections.Remove(vm);
        }
    }


    [ImplementPropertyChanged]
    public class PointerViewModel : INotifyPropertyChanged
    {
        public PointerViewModel(Node start, Node end)
        {
            Start = start;
            End = end;
            Name = "Parameter";
        }


        public bool IsDragging { get; set; }
        public Node Start { get; set; }
        public Node End { get; set; }
        public string Name { get; set; }
        public Point Center { get; set; }
        public double AngleText { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;


        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
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