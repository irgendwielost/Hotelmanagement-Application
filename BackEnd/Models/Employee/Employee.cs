using System;

namespace Hotelmanagement.BackEnd.ViewModels.Customer
{
    public class Employee
    {
        public Employee(int id, string name, int department_id, bool entfernt)
        {
            ID = id;
            Name = name;
            Department_ID = department_id;
            Entfernt = entfernt;
        }
        
        public int ID { get; set; }
        public string Name { get; set; }
        public int Department_ID { get; set; }
        public bool Entfernt { get; set; }
    }
}