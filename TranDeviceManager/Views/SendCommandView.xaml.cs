using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;
using TranDeviceManager.ViewModels;

namespace TranDeviceManager.Views
{
    public partial class SendCommandView : UserControl
    {
        public SendCommandView()
        {
            InitializeComponent();
            DataContext = App.ServiceProvider.GetService<SendCommandViewModel>();
        }
    }
}