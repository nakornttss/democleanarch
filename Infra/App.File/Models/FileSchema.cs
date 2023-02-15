using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.File.Models
{
    public class FileSchema
    {
        public int Input1 { get; set; }
        public int Input2 { get; set; }
        public int Output { get; set; }
        public bool Result { get; set; }
        public DateTime ExecutionTime { get; set; }
    }
}
