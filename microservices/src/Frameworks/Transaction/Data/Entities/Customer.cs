using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Customer.Framework.Data.Entities
{
    [Table("Customer", Schema = "dbo")]
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Pasword { get; set; }
        public string State { get; set; }
        public string Lga { get; set; }
    }
}
