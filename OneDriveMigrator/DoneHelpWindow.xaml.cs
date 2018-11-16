using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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

namespace OneDriveMigrator
{
    /// <summary>
    /// Interaction logic for DoneHelpWindow.xaml
    /// </summary>
    public partial class DoneHelpWindow : Window
    {
        public DoneHelpWindow()
        {
            InitializeComponent();
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("http://OneDrive.com");
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }        

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }
    }
}
