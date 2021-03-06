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

    public partial class Note
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Note()
        {
            this.Buyers = new HashSet<Buyer>();
            this.Inappropriates = new HashSet<Inappropriate>();
            this.Reviews = new HashSet<Review>();
            this.CreatedDate = DateTime.Now;
            this.IsActive = true;
            this.Status = "Draft";
        }

        public int NoteId { get; set; }
        public int OwnerId { get; set; }
        [DisplayName("Title*")]
        [Required(ErrorMessage = "This field must not be null")]
        public string NoteTitle { get; set; }
        [DisplayName("Category*")]
        [Required(ErrorMessage = "This field must not be null")]
        public string Category { get; set; }
        [DisplayName("Display Picture")]
        public string DisplayPicture { get; set; }
        public HttpPostedFileBase DisplayPhoto { get; set; }
        [DisplayName("Upload Notes*")]
        [Required(ErrorMessage = "This field must not be null")]
        public string UploadNotes { get; set; }
        public HttpPostedFileBase DisplayNote { get; set; }
        [DisplayName("Type*")]
        [Required(ErrorMessage = "This field must not be null")]
        public string Type { get; set; }
        [DisplayName("Number of Pages*")]
        [Required(ErrorMessage = "This field must not be null")]
        public Nullable<int> TotalPages { get; set; }
        [DisplayName("Description*")]
        [Required(ErrorMessage = "This field must not be null")]
        public string Description { get; set; }
        [DisplayName("Country")]
        public string Country { get; set; }
        [DisplayName("Institution Name")]
        public string InstitutionName { get; set; }
        [DisplayName("Course Name")]
        public string CourseName { get; set; }
        [DisplayName("Course Code")]
        public Nullable<int> CourseCode { get; set; }
        [DisplayName("Professor/Lecturer")]
        public string Professor { get; set; }
        [DisplayName("Sell For*")]
        [Required(ErrorMessage = "This field must not be null")]
        public string SellType { get; set; }
        [DisplayName("Sell Price*")]
        [Required(ErrorMessage = "This field must not be null")]
        public int SellPrice { get; set; }
        public int Avgrate { get; set; }
        public int report { get; set; }
        [DisplayName("Note Preview*")]
        [Required(ErrorMessage = "This field must not be null")]
        public string NotePreview { get; set; }
        public HttpPostedFileBase DisplayPhotoPreview { get; set; }
        public string Status { get; set; }
        public string AdminRemarks { get; set; }
        public string rejectedName { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<System.DateTime> ApprovedDate { get; set; }
        public Nullable<int> ApprovedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public bool IsActive { get; set; }
        public int FileSize { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Buyer> Buyers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Inappropriate> Inappropriates { get; set; }
        public virtual Member Member { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
