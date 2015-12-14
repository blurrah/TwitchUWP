using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchUWP.Models
{
    public class LiveStream
    {
        public string sourceStream { get; set; }
    }

    public class AccessToken {
        public class RootObject
        {
            public string token { get; set; }
            public string sig { get; set; }
            public bool mobile_restricted { get; set; }
        }
    }
}
