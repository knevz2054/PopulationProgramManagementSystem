//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataProcessingSystem.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblReason
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblReason()
        {
            this.tblIndividuals = new HashSet<tblIndividual>();
        }
    
        public int ID { get; set; }
        public string reasonName { get; set; }
        public int reasonNumber { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblIndividual> tblIndividuals { get; set; }
    }
}