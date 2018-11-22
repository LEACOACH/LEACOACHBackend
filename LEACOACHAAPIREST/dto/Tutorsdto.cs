using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LEACOACHAAPIREST.Models;

namespace LEACOACHAAPIREST.dto
{
    public class Tutorsdto
    {

        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string url_Image { get; set; }
        public Nullable<int> likes { get; set; }
        public string phone_number { get; set; }
        public string description { get; set; }

        //List<Publication> Publications { get; set; }


    }
}