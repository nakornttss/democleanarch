using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Console
{
    public class ResponseMessage
    {
        public int Status { get; set; }
        public string? Message { get; set; }
        public bool? Result { get; set; }
    }
}
