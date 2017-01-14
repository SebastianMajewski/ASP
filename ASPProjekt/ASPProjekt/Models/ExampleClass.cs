using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASPProjekt.Models
{
    using System.Web.Mvc;

    public class ExampleClass
    {
        // This attributes allows your HTML Content to be sent up
        [AllowHtml]
        public string HtmlContent { get; set; }

        public ExampleClass()
        {

        }
    }
}