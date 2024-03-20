using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using System.Windows.Controls;
using TranDeviceManager.Services;
using TranDeviceManager.ViewModels;

namespace TranDeviceManager.Views
{
    /// <summary>
    /// Interaction logic for AutoDetectAndOpenView.xaml
    /// </summary>
    public partial class AutoDetectAndOpenView : UserControl
    {
        public AutoDetectAndOpenView()
        {
            InitializeComponent();
            var viewModel = App.ServiceProvider.GetService<AutoDetectAndOpenViewModel>();
            DataContext = viewModel;
        }
    }
}
