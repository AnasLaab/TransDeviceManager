using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;

namespace TranDeviceManager.Views
{
    public partial class TestRecordManagementView : UserControl
    {
        public TestRecordManagementView()
        {
            InitializeComponent();
            DataContext = App.ServiceProvider.GetService<ViewModels.TestRecordManagementViewModel>();
        }
    }
}
