using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TranDeviceManager.Models;

namespace TranDeviceManager.Services
{
    public class TranApiHelper
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUri;

        public TranApiHelper(string baseUri)
        {
            _httpClient = new HttpClient();
            _baseUri = baseUri;
        }

        public async Task<CommandResponse> SendCommandAsync(CommandRequest commandRequest)
        {
            var json = JsonConvert.SerializeObject(commandRequest);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{_baseUri}/commands/send", content);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<CommandResponse>(responseContent);
            }

            throw new ApplicationException($"Error sending command: {response.ReasonPhrase}");
        }

        public async Task<AutoDetectAndOpenResult> AutoDetectAndOpenTranComPortAsync()
        {
            var response = await _httpClient.GetAsync($"{_baseUri}/commands/autoDetectAndOpen");

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<AutoDetectAndOpenResult>(responseContent);
            }

            throw new ApplicationException("Error auto-detecting and opening TRAN device: " + response.ReasonPhrase);
        }

        public async Task<bool> GetConnectionStatusAsync()
        {
            var response = await _httpClient.GetAsync($"{_baseUri}/commands/status");

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<dynamic>(responseContent).PortOpen;
            }

            throw new ApplicationException("Error getting connection status: " + response.ReasonPhrase);
        }

        public async Task CloseConnectionAsync()
        {
            var response = await _httpClient.PostAsync($"{_baseUri}/commands/close", null);

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException("Error closing connection: " + response.ReasonPhrase);
            }
        }

        public async Task<string> GetTestRecordCountAsync()
        {
            var commandRequest = new CommandRequest { Command = "$GRC 1" };
            var commandResponse = await SendCommandAsync(commandRequest);

            if (commandResponse.Success)
            {
                // Extraction du nombre de records à partir de la réponse
                // La réponse est sous la forme "#GRC: 1 = 2", où "2" est le nombre que nous voulons extraire
                var parts = commandResponse.Response.Split('=');
                if (parts.Length > 1)
                {
                    var countStr = parts[1].Trim(); // Supprime les espaces avant et après le nombre
                    return countStr;
                }
                else
                {
                    throw new ApplicationException("Invalid response format.");
                }
            }
            else
            {
                throw new ApplicationException("Error getting test record count: " + commandResponse.ErrorMessage);
            }
        }



        public async Task<string> GetTestRecordDetailsAsync(string recordId)
        {
            var commandRequest = new CommandRequest { Command = $"$GRC 17 {recordId}" };
            var commandResponse = await SendCommandAsync(commandRequest);

            if (commandResponse.Success)
            {
                return commandResponse.Response; // Assume que la réponse contient les détails du record
            }
            else
            {
                throw new ApplicationException($"Error getting test record details for ID {recordId}: " + commandResponse.ErrorMessage);
            }
        }


    }

}
