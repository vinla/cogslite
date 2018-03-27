using System;
using System.Collections.Generic;
using System.Text;

namespace CogsLite.Core
{
    public class User : BaseObject
    {
        public string EmailAddress { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
    }
}
