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
    
    public partial class Publications
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Publications()
        {
            this.Comments = new HashSet<Comments>();
            this.Favorites = new HashSet<Favorites>();
            this.Images = new HashSet<Images>();
        }
    
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
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comments> Comments { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Favorites> Favorites { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Images> Images { get; set; }
        public virtual Tutor_courses Tutor_courses { get; set; }
        public virtual Types Types { get; set; }
    }
}