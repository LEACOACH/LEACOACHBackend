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
    
    public partial class Favorites
    {
        public int id { get; set; }
        public int publication_id { get; set; }
        public int user_id { get; set; }
    
        public virtual Publications Publications { get; set; }
        public virtual Users Users { get; set; }
    }
}
