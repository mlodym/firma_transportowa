//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FirmaTransportowa.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    public partial class PRZYSTANEK
    {
        public PRZYSTANEK()
        {
            this.PRZYSTANKI_NA_TRASIE = new HashSet<PRZYSTANKI_NA_TRASIE>();
        }
    
        [Key]
        public int PRK_ID { get; set; }
        public string PRK_NAZWA { get; set; }
    
        public virtual ICollection<PRZYSTANKI_NA_TRASIE> PRZYSTANKI_NA_TRASIE { get; set; }
    }
}
