using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestTask1_.net_framework_4._7._2_.CustomErrors
{
    public class RecordException : ArgumentException
    {
        public RecordException(string msg = "record not found")
            : base(msg)
        {
        }
    }
}