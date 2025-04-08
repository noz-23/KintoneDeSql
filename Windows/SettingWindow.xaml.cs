using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
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
    /// SettingWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class SettingWindow : Window
    {
        public SettingWindow()
        {
            InitializeComponent();
        }

        private void _okClick(object sender_, RoutedEventArgs e_)
        {
            Settings.Default.KintoneDomain = _domain.Text;
            Settings.Default.KintoneAccessId = _accessId.Text;
            Settings.Default.KintoneAccessPassword = _accessPassword.Text;
            Settings.Default.KintoneLoginId = _kintoneId.Text;
            Settings.Default.KintoneLoginPassword = _kintonePassword.Text;
            Settings.Default.KitoneApiToken = _kintoneApiToken.Text;
            Settings.Default.ProxyAddress = _proxyAddress.Text;
            Settings.Default.ProxyUser = _proxyUser.Text;
            Settings.Default.ProxyPassword = _proxyPassword.Text;
            //
            Settings.Default.Save();

            Close();
        }

        private void _cancelClick(object sender_, RoutedEventArgs e_)
        {
            Close();
        }
    }
}
