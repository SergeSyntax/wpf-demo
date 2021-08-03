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

namespace Demo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string[] items { get; set; } =
        {
            "Hello from item 1",
            "Hello from item 2",
            "Hello from item 3",
            "Hello from item 4",
            "Hello from item 5",
        };

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }
    }
}
