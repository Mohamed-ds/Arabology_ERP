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
    
    public partial class CalcUnitOfMeasure
    {
        public int ItemId { get; set; }
        public string MainUOM { get; set; }
        public string ConvertTo { get; set; }
        public Nullable<int> Factory { get; set; }
    
        public virtual Item Item { get; set; }
        public virtual UnitOfMeasure UnitOfMeasure { get; set; }
        public virtual UnitOfMeasure UnitOfMeasure1 { get; set; }
    }
}
