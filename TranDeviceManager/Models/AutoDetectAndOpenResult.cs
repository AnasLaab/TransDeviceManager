using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranDeviceManager.Models
{
    public class AutoDetectAndOpenResult
    {
        public bool Success { get; set; }
        public string PortName { get; set; }
        public string PingResponse { get; set; }
    }
}
