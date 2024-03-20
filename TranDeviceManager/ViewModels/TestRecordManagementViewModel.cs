using System.Collections.ObjectModel;
using System.Windows.Input;
using TranDeviceManager.Services;
using System.Threading.Tasks;
using System.Windows;
using TranDeviceManager.Commands;
using System;

namespace TranDeviceManager.ViewModels
{
    public class TestRecordManagementViewModel : ObservableObject
    {
        private readonly TranApiHelper _tranApiHelper;
        private string _selectedRecord;
        private string _selectedRecordDetails;

        public TestRecordManagementViewModel(TranApiHelper tranApiHelper)
        {
            _tranApiHelper = tranApiHelper;
            Records = new ObservableCollection<string>();
            RetrieveRecordsCommand = new AsyncRelayCommand(RetrieveRecordsAsync);
        }

        public ObservableCollection<string> Records { get; }

        public string SelectedRecord
        {
            get => _selectedRecord;
            set
            {
                if (SetProperty(ref _selectedRecord, value))
                {
                    // Supposant que l'ID est toujours la dernière partie après "Record "
                    var recordId = value?.Split(' ')[1];
                    if (!string.IsNullOrEmpty(recordId))
                    {
                        SelectRecordAsync(recordId);
                    }
                }
            }
        }

        public string SelectedRecordDetails
        {
            get => _selectedRecordDetails;
            set => SetProperty(ref _selectedRecordDetails, value);
        }

        public ICommand RetrieveRecordsCommand { get; }

        private async Task RetrieveRecordsAsync()
        {
            try
            {
                var recordCountStr = await _tranApiHelper.GetTestRecordCountAsync();
                int recordCount = int.Parse(recordCountStr);
                Records.Clear();
                for (int i = 1; i <= recordCount; i++)
                {
                    Records.Add($"Record {i}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving records: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task SelectRecordAsync(string recordId)
        {
            try
            {
                var details = await _tranApiHelper.GetTestRecordDetailsAsync(recordId);
                SelectedRecordDetails = details;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving record details: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
