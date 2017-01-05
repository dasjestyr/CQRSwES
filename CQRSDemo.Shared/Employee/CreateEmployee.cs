using System;

namespace CQRSDemo.Shared.Employee
{
    public class CreateEmployee : EventInfo
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public CreateEmployee()
        {
            
        }

        public CreateEmployee(Guid employeeId, string firstName, string lastName)
            : base(employeeId, "Domain request")
        {
            FirstName = firstName;
            LastName = lastName;
        }
    }
}