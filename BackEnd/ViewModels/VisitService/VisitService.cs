using System;

namespace Hotelmanagement.BackEnd.ViewModels.VisitService
{
    public class VisitService
    {
        public VisitService(int visit_id, int service_offer_id, DateTime dateTime)
        {
            VisitId = visit_id;
            ServiceOfferId = service_offer_id;
            DateTime = dateTime;
        }
        
        public int VisitId { get; set; }
        public int ServiceOfferId { get; set; }
        public DateTime DateTime { get; set; }
    }
}