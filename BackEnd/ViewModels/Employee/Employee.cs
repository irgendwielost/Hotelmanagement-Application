using System;

namespace Hotelmanagement.BackEnd.ViewModels.Customer
{
    public class Employee
    {
        public Employee(int id, string name, int department_id, bool entfernt, DateTime entfernt_am)
        {
            ID = id;
            Name = name;
            Department_ID = department_id;
            Entfernt = entfernt;
            Entfernt_Am = entfernt_am;
        }
        
        public int ID { get; set; }
        public string Name { get; set; }
        public int Department_ID { get; set; }
        public bool Entfernt { get; set; }
        public DateTime Entfernt_Am { get; set; }
    }
}