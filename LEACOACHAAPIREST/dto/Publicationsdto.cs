using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LEACOACHAAPIREST.dto
{
    public class Publicationsdto
    {

        public int id { get; set; }
        public string name { get; set; }
        public int views { get; set; }
        public int tutor_course_id { get; set; }
        public int type_id { get; set; }
        public string date { get; set; }
        public string description { get; set; }
        public string url { get; set; }
        public bool premium { get; set; }
        public Nullable<int> likes { get; set; }
    }
}