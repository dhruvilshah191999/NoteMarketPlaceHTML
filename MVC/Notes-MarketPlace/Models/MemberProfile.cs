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
    using System.Web;

    public partial class MemberProfile
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        [DisplayName("Date Of Birth")]
        public Nullable<System.DateTime> BirthDate { get; set; }
        public string Gender { get; set; }
        public string CountryCode { get; set; }

        [StringLength(10,MinimumLength =10,ErrorMessage ="Phone number must be 10 digit")]
        public string PhoneNumber { get; set; }
        public HttpPostedFileBase ImageUrl { get; set; }

        [DisplayName("Profile Picture")]
        public string ProfilePicture { get; set; }

        [DisplayName("Address Line 1*")]
        [Required(ErrorMessage ="This field must not be null")]
        public string AddressLine1 { get; set; }

        [DisplayName("Address Line 2*")]
        public string AddressLine2 { get; set; }

        [DisplayName("City*")]
        [Required(ErrorMessage = "This field must not be null")]
        public string City { get; set; }

        [DisplayName("State*")]
        [Required(ErrorMessage = "This field must not be null")]
        public string State { get; set; }

        [DisplayName("ZipCode*")]
        [Required(ErrorMessage = "This field must not be null")]
        public int Zipcode { get; set; }
        public string Country { get; set; }

        [DisplayName("University")]
        public string University { get; set; }

        [DisplayName("College")]
        public string College { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
    
        public virtual Member Member { get; set; }
    }
}
