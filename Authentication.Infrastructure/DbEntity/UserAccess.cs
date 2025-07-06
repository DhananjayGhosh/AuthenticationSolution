using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Authentication.Infrastructure.DbEntity
{
    public class UserAccess
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string UserName { get; set; } = null!;
        public string? InstitutionID { get; set; } 
        public string? Modality { get; set; }

    }
}
