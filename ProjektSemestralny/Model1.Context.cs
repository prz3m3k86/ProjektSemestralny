//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProjektSemestralny
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class KinoEntities : DbContext
    {
        public KinoEntities()
            : base("name=KinoEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<filmy> filmy { get; set; }
        public virtual DbSet<miejsca> miejsca { get; set; }
        public virtual DbSet<rezerwacje> rezerwacje { get; set; }
        public virtual DbSet<sale> sale { get; set; }
        public virtual DbSet<seanse> seanse { get; set; }
        public virtual DbSet<zarezerwowane_miejsca> zarezerwowane_miejsca { get; set; }
    }
}
