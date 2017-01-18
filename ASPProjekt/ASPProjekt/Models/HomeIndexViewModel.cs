using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASPProjekt.Models
{
    public class HomeIndexViewModel
    {
        public ICollection<ApplicationUser> Users { get; set; }

        public ICollection<Bin> Bins { get; set; }

        public ICollection<Trash> Trash { get; set; }
    }
}