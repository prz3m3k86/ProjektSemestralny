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
    
    public partial class zarezerwowane_miejsca
    {
        public int id_zar_miej { get; set; }
        public int id_miejsca { get; set; }
        public int id_rezerwacji { get; set; }
        public int id_seansu { get; set; }
    
        public virtual miejsca miejsca { get; set; }
        public virtual rezerwacje rezerwacje { get; set; }
        public virtual seanse seanse { get; set; }
    }
}
