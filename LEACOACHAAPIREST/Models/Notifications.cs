//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LEACOACHAAPIREST.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Notifications
    {
        public int id { get; set; }
        public string title { get; set; }
        public string text { get; set; }
        public string date { get; set; }
        public int tutor_id { get; set; }
    
        public virtual Tutors Tutors { get; set; }
    }
}
