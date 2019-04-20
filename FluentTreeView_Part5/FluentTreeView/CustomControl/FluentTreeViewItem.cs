using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace FluentTreeView.CustomControl
{
    class FluentTreeViewItem:TreeViewItem
    {


        public Brush MouseOverBrush
        {
            get { return (Brush)GetValue(MouseOverBrushProperty); }
            set { SetValue(MouseOverBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MouseOverBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MouseOverBrushProperty =
            DependencyProperty.Register("MouseOverBrush", typeof(Brush), typeof(FluentTreeViewItem), new PropertyMetadata(new SolidColorBrush(Colors.Red)));



        public Brush SelectedBrush
        {
            get { return (Brush)GetValue(SelectedBrushProperty); }
            set { SetValue(SelectedBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedBrushProperty =
            DependencyProperty.Register("SelectedBrush", typeof(Brush), typeof(FluentTreeViewItem), new PropertyMetadata(new SolidColorBrush(Colors.Green)));




        public bool RecursiveHighlightMode
        {
            get { return (bool)GetValue(RecursiveHighlightModeProperty); }
            set { SetValue(RecursiveHighlightModeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RecursiveHighlightMode.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RecursiveHighlightModeProperty =
            DependencyProperty.Register("RecursiveHighlightMode", typeof(bool), typeof(FluentTreeViewItem), new FrameworkPropertyMetadata(false,FrameworkPropertyMetadataOptions.AffectsMeasure));

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
