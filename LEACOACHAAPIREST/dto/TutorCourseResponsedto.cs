using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LEACOACHAAPIREST.dto
{
    public class TutorCourseResponsedto

    {
        public int id { get; set; }
        public Tutorsdto Tutors { get; set; }
        public int id_course { get; set; }

    }
}