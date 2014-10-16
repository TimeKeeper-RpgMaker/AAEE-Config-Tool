using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AAEE.Windows
{
    /// <summary>
    /// Logique d'interaction pour VersionHistoryWindow.xaml
    /// </summary>
    public partial class VersionHistoryWindow : Window
    {
        public VersionHistoryWindow()
        {
            InitializeComponent();
        }

        private void button_Ok_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
