using System;
using System.Collections.Generic;

namespace EZSocketNc.Siemens
{

        public class Alarm
        {
                public string AlarmNo { get; set; }
                public string AlarmMessage { get; set; }
                public DateTime AlarmTime { get; set; }

                public string AlarmMsg { get { return $"{AlarmNo}\t{AlarmMessage}\t\t"; } }
        }

        public class StateObject
        {
                public System.Net.Sockets.Socket WorkSocket = null;
                public const int BufferSize = 4096;
                public byte[] Buffer = new byte[BufferSize];
        }
}
