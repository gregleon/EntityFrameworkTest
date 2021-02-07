using System;
using System.ComponentModel.DataAnnotations;

namespace EntityFrameworkTest.Console.Models
{
    public class DateChild
    {
        [Key]
        public int DateChildID { get; set; }
        
        public DateTime CreateDate { get; set; }
    }
}
