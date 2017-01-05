using System;

namespace CQRSDemo.Shared.Employee
{
    public class AssignEmployee : EventInfo
    {
        public Employee Employee { get; set; }

        public AssignEmployee()
        {
            
        }
        public AssignEmployee(Guid parentId, Employee employee) 
            : base(parentId, "Domain request")
        {
            Employee = employee;
        }
    }
}