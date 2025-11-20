using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EZSocketNc.Mqtts.Dtos
{
    public class CmdExecute
    {
        public string Cmd { get; set; }

        public string Parameter { get; set; }

    }
}
