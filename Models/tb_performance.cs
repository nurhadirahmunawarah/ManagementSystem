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

    public partial class tb_performance
    {
        public int ID { get; set; }
        public string Remark { get; set; }
        public int StudentID { get; set; }
        [DisplayName("Tarikh Dijana")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> DateCreated { get; set; }
        [DisplayName("Penilaian Tutor")]
        public Nullable<int> ratingStudent { get; set; }
    
        public virtual tb_student tb_student { get; set; }
    }
}
