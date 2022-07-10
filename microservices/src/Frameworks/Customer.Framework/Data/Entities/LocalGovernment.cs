using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Customer.Framework.Data.Entities
{

    [Table("LocalGovernment", Schema = "dbo")]
    public class LocalGovernment
    {
        [Key]
        public long Id { get; set; }
        public long State_id { get; set; }
        public string Name { get; set; }
    }
}
        
 