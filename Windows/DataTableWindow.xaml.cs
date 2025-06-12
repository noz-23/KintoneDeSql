using KintoneDeSql.Views;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace KintoneDeSql.Windows
{
    /// <summary>
    /// DataTableWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class DataTableWindow : Window
    {
        public DataTableWindow()
        {
            InitializeComponent();
        }
        internal DataTableWindow(DataTable dataTable_) : this()
        {
            _dataTable = dataTable_;
            _dataGrid.ItemsSource = null;
            _dataGrid.ItemsSource = _dataTable.DefaultView;
        }

        private readonly DataTable _dataTable;
    }
}
