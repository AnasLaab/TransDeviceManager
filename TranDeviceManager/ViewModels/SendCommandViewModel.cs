using System.Windows.Input;
using TranDeviceManager.Models;
using TranDeviceManager.Services;
using System;
using System.Threading.Tasks;
using System.Windows;
using TranDeviceManager.Commands;

namespace TranDeviceManager.ViewModels
{
    public class SendCommandViewModel : ObservableObject
    {
        private readonly TranApiHelper _tranApiHelper;
        private string _command;
        private string _response;
        private string _sendTimestamp;
        private string _receiveTimestamp;
        private string _interval;

        public SendCommandViewModel(TranApiHelper tranApiHelper)
        {
            _tranApiHelper = tranApiHelper;
            SendCommand = new AsyncRelayCommand(ExecuteSendCommandAsync);
            ClearCommand = new RelayCommand(ExecuteClearCommand);
        }

        public string Command
        {
            get => _command;
            set => SetProperty(ref _command, value);
        }

        public string Response
        {
            get => _response;
            set => SetProperty(ref _response, value);
        }

        public string SendTimestamp
        {
            get => _sendTimestamp;
            set => SetProperty(ref _sendTimestamp, value);
        }

        public string ReceiveTimestamp
        {
            get => _receiveTimestamp;
            set => SetProperty(ref _receiveTimestamp, value);
        }

        public string Interval
        {
            get => _interval;
            set => SetProperty(ref _interval, value);
        }

        public ICommand SendCommand { get; }
        public ICommand ClearCommand { get; }

        private async Task ExecuteSendCommandAsync()
        {
            if (!string.IsNullOrWhiteSpace(Command))
            {
                try
                {
                    var commandRequest = new CommandRequest { Command = Command };
                    var response = await _tranApiHelper.SendCommandAsync(commandRequest);

                    Response = response.Response;
                    SendTimestamp = response.SendTimestamp;
                    ReceiveTimestamp = response.ReceiveTimestamp;
                    Interval = response.Interval.ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error sending command: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Please enter a command.", "Input Required", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void ExecuteClearCommand()
        {
            Command = string.Empty;
            Response = string.Empty;
            SendTimestamp = string.Empty;
            ReceiveTimestamp = string.Empty;
            Interval = string.Empty;
        }
    }
}
