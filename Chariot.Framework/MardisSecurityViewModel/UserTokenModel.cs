﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chariot.Framework.MardisSecurityViewModel
{
 public   class UserTokenModel
    {
       
        public string token { get; set; }
        public DateTime DateToken { get; set; }
        public string message { get; set; }
        public UserViewModel _user  { get; set; }
}
}
