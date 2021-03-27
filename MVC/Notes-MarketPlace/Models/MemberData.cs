using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;

namespace Notes_MarketPlace.Models
{
    using System;
    using System.Collections.Generic;
    public class MemberData
    {
        public Member member { get; set; }
        public MemberProfile mp { get; set; }
        public IPagedList<Note> notes { get; set; }   
        public ManageSystem manage { get; set; } 
    }
}