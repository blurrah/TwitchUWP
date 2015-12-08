using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchUWP.Models
{
    public class LiveStream
    {

    }

    public class AccessToken {
        public class Chansub
        {
            public int view_until { get; set; }
            public List<object> restricted_bitrates { get; set; }
        }

        public class Private
        {
            public bool allowed_to_view { get; set; }
        }

        public class Token
        {
            public object user_id { get; set; }
            public string channel { get; set; }
            public int expires { get; set; }
            public Chansub chansub { get; set; }
            public Private @private { get; set; }
            public bool privileged { get; set; }
            public bool source_restricted { get; set; }
        }

        public class RootObject
        {
            public Token token { get; set; }
            public string sig { get; set; }
            public bool mobile_restricted { get; set; }
        }
    }
}
