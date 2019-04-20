using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace FluentTreeView.CustomControl
{
    public class FluentTreeView:TreeView
    {

        public FluentTreeView()
        {
            
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new FluentTreeViewItem();
        }

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is FluentTreeViewItem;
        }
    }
}
