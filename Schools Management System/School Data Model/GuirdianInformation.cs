//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Schools_Management_System.School_Data_Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class GuirdianInformation
    {
        public int GuirdianID { get; set; }
        public string GuirdianName { get; set; }
        public string GuirdianGender { get; set; }
        public string GuirdianOccupation { get; set; }
        public string GuirdianRelation { get; set; }
        public string GuirdianEmail { get; set; }
        public string GuirdianMobile { get; set; }
        public int StudentID { get; set; }
    
        public virtual StudentInformation StudentInformation { get; set; }
    }
}
