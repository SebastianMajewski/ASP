using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASPProjekt.Models
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web.Mvc;

    public class Trash
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [CustomName]
        [StringLength(50, MinimumLength = 4)]
        [DisplayName("Tytuł")]
        public string Title { get; set; }

        [Required]
        [AllowHtml]
        [DisplayName("Treść")]
        public string Content { get; set; }

        [DataType(DataType.DateTime), DisplayFormat(DataFormatString = "{0:H:mm dd.MM.yyy}", ApplyFormatInEditMode = false)]
        public DateTime AddTime { get; set; }

        [ForeignKey("Bin")]       
        public int BinId { get; set; }

        [DisplayName("Kosz")]
        public virtual Bin Bin { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}