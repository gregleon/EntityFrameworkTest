using System.ComponentModel.DataAnnotations;

namespace EntityFrameworkTest.Console.Models
{
    public class AnyEntity
    {
        [Key]
        public int AnyEntityID { get; set; }
        
        public int AnyEntityParentID { get; set; }
        
        public virtual AnyEntityParent AnyEntityParent { get; set; }
    }
}
