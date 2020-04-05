using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CustomersAPIClient
{
    class CustomDataGrid2 : DataGrid
    {
        public CustomDataGrid2()
        {
            this.SelectionChanged += CustomDataGrid_SelectionChanged2;
        }

        void CustomDataGrid_SelectionChanged2(object sender, SelectionChangedEventArgs e)
        {
            this.SelectedItemsList2 = this.SelectedItems;
        }
        #region SelectedItemsList2

        public IList SelectedItemsList2
        {
            get { return (IList)GetValue(SelectedItemsListProperty2); }
            set { SetValue(SelectedItemsListProperty2, value); }
        }

        public static readonly DependencyProperty SelectedItemsListProperty2 =
                DependencyProperty.Register("SelectedItemsList2", typeof(IList), typeof(CustomDataGrid2), new PropertyMetadata(null));
    }
    #endregion
}

