using System;

namespace CQRSDemo.Shared.Employee
{
    public class DeactivateEmployee : EventInfo
    {
        public DateTimeOffset DeactivationDate { get; set; }

        public DeactivateEmployee()
        {
            
        }
        public DeactivateEmployee(Guid employeeId, DateTimeOffset deactivationDate)
            : base(employeeId, "Domain request")
        {
            DeactivationDate = deactivationDate;
        }
    }
}