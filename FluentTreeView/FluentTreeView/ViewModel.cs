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
        private ObservableCollection<TreeViewVMBase> _treeViewBase;

        public ObservableCollection<TreeViewVMBase> TreeViewBase
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
            TreeViewBase = new ObservableCollection<TreeViewVMBase>(new[]
            {
                new TreeNodeVM("Root1",nodes),
                new TreeNodeVM("Root2",nodes)
            });
        }

    }

    abstract class TreeViewVMBase : ViewModelBase
    {

        public abstract ObservableCollection<TreeViewVMBase> Items { get; set; }
        

        private string _name;

        public string Name
        {
            get { return _name; }
            private set { Set(ref _name, value); }
        }

        protected static IEnumerable<TreeViewVMBase> Empty => Enumerable.Empty<TreeViewVMBase>();

        public TreeViewVMBase(string name)
        {
            Name = name;
            // calling child from base ctor. Bad practice!!!
            // https://stackoverflow.com/a/119531
            //Items = new ObservableCollection<TreeViewVMBase>(LoadItems() ?? Empty);
        }
     

    }

    class TreeNodeVM : TreeViewVMBase
    {
        private ObservableCollection<TreeViewVMBase> _items;
        public override ObservableCollection<TreeViewVMBase> Items
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

        public TreeNodeVM(string name, IEnumerable<TreeViewVMBase> items) : base(name)
        {
            _items = new ObservableCollection<TreeViewVMBase>(items??Empty);
        }


    }
    class TreeLeafVM : TreeViewVMBase
    {
        private static readonly ObservableCollection<TreeViewVMBase> _items = new ObservableCollection<TreeViewVMBase>();
        public override ObservableCollection<TreeViewVMBase> Items
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
