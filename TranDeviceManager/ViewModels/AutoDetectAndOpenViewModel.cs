using System.Windows.Input;
using TranDeviceManager.Services;
using System;
using System.Threading.Tasks;
using System.Windows;
using TranDeviceManager.Commands;

namespace TranDeviceManager.ViewModels
{
    public class AutoDetectAndOpenViewModel : ObservableObject
    {
        private readonly TranApiHelper _tranApiHelper;

        public AutoDetectAndOpenViewModel(TranApiHelper tranApiHelper)
        {
            _tranApiHelper = tranApiHelper;
            ConnectCommand = new AsyncRelayCommand(ConnectAsync);
            DisconnectCommand = new AsyncRelayCommand(DisconnectAsync);
        }

        public ICommand ConnectCommand { get; }
        public ICommand DisconnectCommand { get; }

        private string _detectedComPort;
        public string DetectedComPort
        {
            get => _detectedComPort;
            set => SetProperty(ref _detectedComPort, value);
        }

        private string _pingResponse;
        public string PingResponse
        {
            get => _pingResponse;
            set => SetProperty(ref _pingResponse, value);
        }

        private async Task ConnectAsync()
        {
            try
            {
                var result = await _tranApiHelper.AutoDetectAndOpenTranComPortAsync();
                if (result.Success)
                {
                    DetectedComPort = result.PortName;
                    PingResponse = result.PingResponse;
                    MessageBox.Show($"Connected to {DetectedComPort}.", "Auto-Detect and Open", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("No TRAN device detected.", "Auto-Detect and Open", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred during auto-detect: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task DisconnectAsync()
        {
            try
            {
                await _tranApiHelper.CloseConnectionAsync();
                MessageBox.Show("Connection closed.", "Disconnect", MessageBoxButton.OK, MessageBoxImage.Information);
                DetectedComPort = string.Empty;
                PingResponse = string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred during disconnect: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
