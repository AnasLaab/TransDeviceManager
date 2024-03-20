using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranDeviceManager.Models
{
    public class CommandResponse
    {
        public bool Success { get; set; }
        public string Response { get; set; }
        public string ErrorMessage { get; set; }
        public string SendTimestamp { get; set; }
        public string ReceiveTimestamp { get; set; }
        public double Interval { get; set; }
    }
}
