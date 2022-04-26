namespace Hotelmanagement.BackEnd.ViewModels.VisitRoom
{
    public class VisitRoom
    {
        public VisitRoom(int visit_id,int  room_id, bool extra_bed, int extra_bed_amount)
        {
            Visit_id = visit_id;
            Room_id = room_id;
            Extra_bed = extra_bed;
            Extra_bed_amount = extra_bed_amount;
        }
        
        public int Visit_id { get; set; }
        public int Room_id { get; set; }
        public bool Extra_bed { get; set; }
        public int Extra_bed_amount { get; set; }
        
    }
}