using System.ComponentModel.DataAnnotations;

namespace EntityFrameworkTest.Console.Models
{
    public class AnyChild
    {
        [Key]
        public int AnyChildID { get; set; }
    }
}
