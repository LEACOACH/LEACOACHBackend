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
    
    public partial class Tutor_courses
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tutor_courses()
        {
            this.Publications = new HashSet<Publications>();
        }
    
        public int id { get; set; }
        public int id_tutor { get; set; }
        public int id_course { get; set; }
    
        public virtual Courses Courses { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Publications> Publications { get; set; }
        public virtual Tutors Tutors { get; set; }
    }
}
