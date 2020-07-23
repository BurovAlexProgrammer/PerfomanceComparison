using PerfomanceComparison.Dialogs;
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
using static GlobalExtension;

namespace PerfomanceComparison 
{
    public partial class MainWindow : Window
    {
        public MainWindow mainWindow;

        public ProcessState StringModel = new ProcessState() { Title = "String" };
        public ProcessState StringBuilderModel = new ProcessState() { Title = "StringBuilder" };
        public MainWindow()
        {
            mainWindow = this;
            InitializeComponent();
            StringContent.DataContext = StringModel;
            StringBuilderContent.DataContext = StringBuilderModel;
            Panel_StringPerformance.Collapse();
            Panel_StringMemoryUsed.Collapse();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (sender == ButtonQuit)
                SureQuit();

            if (sender == ButtonStartTestStringVsStringBuilder)
                //Test();
                Panel_StringPerformance.Show();
                Panel_StringMemoryUsed.Show();
                StringVsStringBuilder();
        }

        void Test()
        {
            var x = StringModel.Title;
        }
    }
}
