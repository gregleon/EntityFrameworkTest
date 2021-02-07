using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EntityFrameworkTest.Console.Models
{
    public class Root
    {
        public Root()
        {
            DateChildren = new HashSet<DateChild>();
            AnyChildren = new HashSet<AnyChild>();
        }

        public int? ParentRootID { get; set; }
        
        public virtual Root ParentRoot { get; set; }
        
        [Key]
        public int RootID { get; set; }
        
        public int AnyEntityID { get; set; }
        
        public virtual AnyEntity AnyEntity { get; set; }

        public virtual ICollection<DateChild> DateChildren { get; set; }

        public virtual ICollection<AnyChild> AnyChildren { get; set; }
    }
}
