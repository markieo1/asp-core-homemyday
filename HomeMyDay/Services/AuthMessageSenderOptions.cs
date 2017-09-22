using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeMyDay.Services
{
    public class AuthMessageSenderOption
    {
        public string SmtpUser { get; set; }
        public string SmtpPass { get; set; }
        public string SmtpFrom { get; set; }
        public string SmtpFromName { get; set; }
    }
}
