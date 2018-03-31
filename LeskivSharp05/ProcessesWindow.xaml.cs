using System;
using System.Windows;

namespace LeskivSharp05
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ProcessesViewModel(delegate () { Dispatcher.Invoke(ProcessesDataGrid.Items.Refresh); });
        }
    }
}
