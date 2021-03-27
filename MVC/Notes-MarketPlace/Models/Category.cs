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

    public partial class Category
    {
        public Category()
        {
            this.IsActive = true;
            this.CreatedDate = DateTime.Now;
        }

        public int Id { get; set; }
        [DisplayName("Category Name *")]
        [Required(ErrorMessage = "This field must not be null")]
        public string Name { get; set; }
        [DisplayName("Description *")]
        [Required(ErrorMessage = "This field must not be null")]
        public string Description { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public bool IsActive { get; set; }

        public virtual Admin Admin { get; set; }
    }
}
