//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ManagementSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    public partial class tb_salaryRate
    {
        public int ID { get; set; }
        [DisplayName("Tarikh Dijana")]
        public Nullable<System.DateTime> DateCreated { get; set; }
        [DisplayName("Kadar Gaji Tutor (RM)")]
        public Nullable<decimal> SalaryRate { get; set; }
    }
}