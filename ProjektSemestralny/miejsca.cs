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
    using System.Collections.Generic;
    
    public partial class miejsca
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public miejsca()
        {
            this.zarezerwowane_miejsca = new HashSet<zarezerwowane_miejsca>();
        }
    
        public int id_miejsca { get; set; }
        public byte rzad { get; set; }
        public byte numer_miejsca { get; set; }
        public int id_sali { get; set; }
    
        public virtual sale sale { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<zarezerwowane_miejsca> zarezerwowane_miejsca { get; set; }

        public override string ToString()
        {
            return $"Rz�d: {rzad}, Nr miejsca: {numer_miejsca}";
        }
    }
}
