using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LEACOACHAAPIREST.dto
{
    public class Notificationdto
    {

        public int id { get; set; }
        public string title { get; set; }
        public string text { get; set; }
        public string date { get; set; }
        public int tutor_id { get; set; }

        public Tutorsdto Tutors { get; set; }
    }
}