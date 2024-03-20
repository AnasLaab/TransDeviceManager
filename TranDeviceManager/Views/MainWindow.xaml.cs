using System.Windows;
using TranDeviceManager.Services;

namespace TranDeviceManager
{
    public partial class MainWindow : Window
    {
        private readonly TranApiHelper _tranApiHelper;

        // Le constructeur accepte maintenant TranApiHelper comme une dépendance
        public MainWindow(TranApiHelper tranApiHelper)
        {
            InitializeComponent();
            _tranApiHelper = tranApiHelper;
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
