﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class leacoachEntities2 : DbContext
    {
        public leacoachEntities2()
            : base("name=leacoachEntities2")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Careers> Careers { get; set; }
        public virtual DbSet<Comments> Comments { get; set; }
        public virtual DbSet<Courses> Courses { get; set; }
        public virtual DbSet<Favorites> Favorites { get; set; }
        public virtual DbSet<Images> Images { get; set; }
        public virtual DbSet<Notifications> Notifications { get; set; }
        public virtual DbSet<Publications> Publications { get; set; }
        public virtual DbSet<Tutor_courses> Tutor_courses { get; set; }
        public virtual DbSet<Tutors> Tutors { get; set; }
        public virtual DbSet<Types> Types { get; set; }
        public virtual DbSet<Users> Users { get; set; }
    }
}
