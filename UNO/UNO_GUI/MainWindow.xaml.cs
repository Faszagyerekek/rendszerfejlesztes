using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UNO_GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        #region >>> WelcomeTab <<<
        private void ChatB_Click(object sender, RoutedEventArgs e)
        {
            TabC.SelectedItem = ChatTab;
        }

        #endregion


        #region >>> ChatTab <<<
        private void MSGBOX_TextChanged(object sender, TextChangedEventArgs e)
        {
            MSGBOX.ScrollToEnd();
        }

        private void Input_field_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {

            }
        }
        #endregion
    }
}
