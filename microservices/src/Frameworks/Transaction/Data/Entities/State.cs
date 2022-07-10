using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Customer.Framework.Data.Entities
{
  
    public class State
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
