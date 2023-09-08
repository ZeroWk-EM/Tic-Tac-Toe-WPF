using System.Windows;
using Tic_tac_Toe_WPF.ViewModel;

namespace Tic_tac_Toe_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var vm = new MainViewModel();
            DataContext = vm;
        }
    }
}
