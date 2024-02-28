//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Arabology_ERP.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Salesman
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Salesman()
        {
            this.Sales = new HashSet<Sale>();
        }
    
        public int SalesmanId { get; set; }
        public string SalesmanNameA { get; set; }
        public string SalesmanNameE { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public Nullable<int> BranchId { get; set; }
        public Nullable<int> SalesmanTypeId { get; set; }
        public Nullable<int> SectorId { get; set; }
        public Nullable<int> StoreId { get; set; }
        public Nullable<bool> InActive { get; set; }
        public string BUID { get; set; }
    
        public virtual Branch Branch { get; set; }
        public virtual Category Category { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sale> Sales { get; set; }
        public virtual SalesmanType SalesmanType { get; set; }
        public virtual Sector Sector { get; set; }
        public virtual Store Store { get; set; }
        public virtual BuisnessUnit BuisnessUnit { get; set; }
    }
}
