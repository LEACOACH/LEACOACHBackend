using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LEACOACHAAPIREST.Models;

namespace LEACOACHAAPIREST.dto
{
    public class Commentsdto
    {


        public int id { get; set; }
        public int publication_id { get; set; }
        public string comment { get; set; }
        public string date { get; set; }
        //List<Publication> Publications { get; set; }


    }
}