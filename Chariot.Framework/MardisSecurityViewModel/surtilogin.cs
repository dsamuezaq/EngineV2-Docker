using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chariot.Framework.MardisSecurityViewModel
{
   public class surtilogin
    {

        public string access_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
        public string error_description { get; set; }
        public string refresh_token { get; set; }
        public string Responce { get; set; }
    }
}
