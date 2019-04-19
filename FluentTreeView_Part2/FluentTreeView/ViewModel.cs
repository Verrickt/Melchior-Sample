using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FluentTreeView
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void Set<T>(ref T property, T value, [CallerMemberName]string propertyName = "")
        {
            if (!EqualityComparer<T>.Default.Equals(property, value))
            {
                property = value;
                OnPropertyChanged(propertyName);
            }
        }

        private void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    class MainViewModel : ViewModelBase
    {
        private ObservableCollection<TreeVMBase> _treeViewBase;

        public ObservableCollection<TreeVMBase> TreeViewBase
        {
            get { return _treeViewBase; }
            set { Set(ref _treeViewBase, value); }
        }

        public MainViewModel()
        {
            var leafs = new[]
            {
                new TreeLeafVM("Leaf1"),
                new TreeLeafVM("Leaf2"),
                new TreeLeafVM("Leaf3")
            };
            var nodes = new[]
            {
                new TreeNodeVM("Node1",null),
                new TreeNodeVM("Node2",leafs),
                new TreeNodeVM("Node3",leafs.Skip(1).Take(1))
            };
            TreeViewBase = new ObservableCollection<TreeVMBase>(new[]
            {
                new TreeNodeVM("Root1",nodes),
                new TreeNodeVM("Root2",nodes)
            });
        }

    }

    abstract class TreeVMBase : ViewModelBase
    {

        public abstract ObservableCollection<TreeVMBase> Items { get; set; }

        private string _name;

        public string Name
        {
            get { return _name; }
            private set { Set(ref _name, value); }
        }

        protected static IEnumerable<TreeVMBase> Empty => Enumerable.Empty<TreeVMBase>();

        public TreeVMBase(string name)
        {
            Name = name;
        }
     

    }

    class TreeNodeVM : TreeVMBase
    {
        private ObservableCollection<TreeVMBase> _items;
        public override ObservableCollection<TreeVMBase> Items
        {
            get
            {
                return _items;
            }

            set
            {
                Set(ref _items, value);
            }
        }

        public TreeNodeVM(string name, IEnumerable<TreeVMBase> items) : base(name)
        {
            _items = new ObservableCollection<TreeVMBase>(items??Empty);
        }


    }
    class TreeLeafVM : TreeVMBase
    {
        private static readonly ObservableCollection<TreeVMBase> _items = new ObservableCollection<TreeVMBase>();
        public override ObservableCollection<TreeVMBase> Items
        {
            get
            {
                return _items;
            }

            set
            {
            }
        }
        public TreeLeafVM(string name) : base(name)
        {
        }


    }
}
