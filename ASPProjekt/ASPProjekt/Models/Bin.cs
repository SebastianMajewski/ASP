using System.Collections.Generic;

namespace ASPProjekt.Models
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Core.Objects.DataClasses;

    public class Bin
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [CustomName]
        [StringLength(50, MinimumLength = 3)]
        [DisplayName("Nazwa")]
        public string Name { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 5)]
        [DisplayName("Opis")]
        public string Description { get; set; }

        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public virtual ICollection<Trash> Trash { get; set; }

    }
}