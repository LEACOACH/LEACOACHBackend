using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LEACOACHAAPIREST.dto
{
    public class Usersdto
    {
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public bool premium { get; set; }
        public string url_Image { get; set; }

    }
}