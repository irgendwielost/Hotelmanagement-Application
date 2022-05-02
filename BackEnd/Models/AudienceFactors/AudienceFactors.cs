using System;

namespace Hotelmanagement.BackEnd.Models.AudienceFactors
{
    public class AudienceFactors
    {
        public AudienceFactors(int customer_id, string gender,string travel_reason, string familystate, 
            string company, string job, string lifestyle, string salary, bool is_businesstrip, bool deleted)
        {
            CustomerID = customer_id;
            Gender = gender;
            TravelReason = travel_reason;
            Familystate = familystate;
            Company = company;
            Job = job;
            Lifestyle = lifestyle;
            Salary = salary;
            IsBusinesstrip = is_businesstrip;
            Deleted = deleted;
        }
    
        public int CustomerID { get; set; }
        public string Gender { get; set; }
        public string TravelReason { get; set; }
        public string Familystate { get; set; }
        public string Company { get; set; }
        public string Job { get; set; }
        public string Lifestyle { get; set; }
        public string Salary { get; set; }
        public bool IsBusinesstrip { get; set; }
        public bool Deleted { get; set; }
    }
}