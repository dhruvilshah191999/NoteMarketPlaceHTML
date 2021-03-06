//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Notes_MarketPlace.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;


    public partial class Country
    {
        public Country()
        {
            this.IsActive = true;
            this.CreatedDate = DateTime.Now;
        }
        public int Id { get; set; }
        [DisplayName("Country Name *")]
        [Required(ErrorMessage = "This field must not be null")]
        public string CountryName { get; set; }
        [DisplayName("Country Code *")]
        [Required(ErrorMessage = "This field must not be null")]
        public int CountryCode { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public int ModifiedBy { get; set; }
        public bool IsActive { get; set; }

        public virtual Admin Admin { get; set; }
    }
}
