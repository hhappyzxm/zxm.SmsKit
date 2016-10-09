using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace zxm.SmsKit
{
    public class SmsKitException : Exception
    {
        public SmsKitException(string message) : base(message) { }
    }
}
