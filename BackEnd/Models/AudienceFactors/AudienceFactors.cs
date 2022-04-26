using System;

namespace Hotelmanagement.BackEnd.Models.AudienceFactors
{
    public class AudienceFactors
    {
        public AudienceFactors(int customer_id, string gender, DateTime birthday, string travel_reason, string familystate, 
            string company, string job, string lifestyle, string salary, bool is_businesstrip, bool deleted, DateTime deleted_at)
        {
            CustomerID = customer_id;
            Gender = gender;
            Birthday = birthday;
            TravelReason = travel_reason;
            Familystate = familystate;
            Company = company;
            Job = job;
            Lifestyle = lifestyle;
            Salary = salary;
            IsBusinesstrip = is_businesstrip;
            Deleted = deleted;
            DeletedAt = deleted_at;
        }
    
        public int CustomerID { get; set; }
        public string Gender { get; set; }
        public DateTime Birthday { get; set; }
        public string TravelReason { get; set; }
        public string Familystate { get; set; }
        public string Company { get; set; }
        public string Job { get; set; }
        public string Lifestyle { get; set; }
        public string Salary { get; set; }
        public bool IsBusinesstrip { get; set; }
        public bool Deleted { get; set; }
        public DateTime DeletedAt { get; set; }
    }
}