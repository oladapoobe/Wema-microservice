using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Customer.Framework.Data.Entities
{


    [Table("OtpLog", Schema = "dbo")]
    public class OtpLog
    {
        [Key]
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public string Otp { get; set; }
        public System.DateTime DateCreated { get; set; }
        public System.DateTime DateExpired { get; set; }
    }
}
