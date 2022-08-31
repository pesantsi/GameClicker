using GameClicker.ViewModels;
using MahApps.Metro.Controls;

namespace GameClicker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = App.Current.Services.GetService(typeof(MainViewModel));
        }
    }
}
