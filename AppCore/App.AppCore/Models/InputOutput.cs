using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.AppCore.Models
{
    public class InputOutput
    {
        public int Input1 { get; set; }
        public int Input2 { get; set; }
        public int Output { get; set; }

        public bool ValidateInput
        {
            get
            {
                if (Input1 > 10 || Input1 < -10) return false;
                if (Input2 > 10 || Input2 < -10) return false;
                return true;
            }
        }

        public string? ValidateExplaination
        {
            get
            {
                if (Input1 > 10 || Input1 < -10) return "Input 1 must between -10 to 10";
                if (Input2 > 10 || Input2 < -10) return "Input 2 must between -10 to 10";
                return null;
            }
        }
    }
}
