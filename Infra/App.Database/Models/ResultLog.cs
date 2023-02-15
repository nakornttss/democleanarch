using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace App.Database.Models
{
    public class ResultLog
    {
        [Key]
        public Guid Id { get; set; }
        public int Input1 { get; set; }
        public int Input2 { get; set; }
        public int Output { get; set; }
        public bool Result { get; set; }
        public DateTime ExecutionTime { get; set; }
    }
}
