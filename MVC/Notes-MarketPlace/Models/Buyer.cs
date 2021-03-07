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
    
    public partial class Buyer
    {
        public Buyer()
        {
            this.IsActive = true;
            this.RequestDate = DateTime.Now;
            this.ApprovedDate = DateTime.Now;
        }
        public int Id { get; set; }
        public int NoteId { get; set; }
        public int OwnerId { get; set; }
        public int MemberId { get; set; }
        public bool Status { get; set; }
        public string AttachmentPath { get; set; }
        public bool IsPaid { get; set; }
        public int Price { get; set; }
        public string NoteTitle { get; set; }
        public string NoteCategory { get; set; }
        public System.DateTime RequestDate { get; set; }
        public Nullable<System.DateTime> ApprovedDate { get; set; }
        public bool IsActive { get; set; }
        public string CountryCode { get; set; }
        public string PhoneNumber { get; set; }
        public virtual Member Member { get; set; }
        public virtual Member Member1 { get; set; }
        public virtual Note Note { get; set; }
    }
}
