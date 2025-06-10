using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LodgerBackend.App.User.Models.Entities
{
    [Table("addresses")]
    public class Address
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Column("address_name")]
        public string AddressName { get; set; } = string.Empty;

        [Column("code_postal")]
        public string PostalCode { get; set; } = string.Empty;

        public ICollection<User> Users { get; set; } = new List<User>();
    }

}
