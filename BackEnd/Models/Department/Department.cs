using System;

namespace Hotelmanagement.BackEnd.ViewModels.Department
{
    public class Department
    {
        public Department(int id, string name, bool entfernt, DateTime entfernt_am)
        {
            ID = id;
            Name = name;
            Entfernt = entfernt;
            Entfernt_am = entfernt_am;
        }
        
        
        public int ID { get; set; }
        public string Name { get; set; }
        public bool Entfernt { get; set; }
        public DateTime Entfernt_am { get; set; }
    }
}