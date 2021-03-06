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
    using System.ComponentModel.DataAnnotations;

    public partial class tb_salary
    {
        public int ID { get; set; }
        [DisplayName("Nilai Gaji (RM)")]
        public Nullable<double> Amount { get; set; }
        [DisplayName("No Kad Pengenalan")]
        public int TutorID { get; set; }
        [DisplayName("Bulan")]
        public Nullable<int> month { get; set; }
        public string Status { get; set; }
        [DisplayName("Tarikh")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> Date { get; set; }
    
        public virtual tb_user tb_user { get; set; }
    }
}
