using System;

namespace CQRSDemo.Shared.Employee
{
    public class ActivateEmployee : EventInfo
    {
        public DateTimeOffset ActivationDate { get; set; }

        public ActivateEmployee()
        {
        }

        public ActivateEmployee(Guid employeeId, DateTimeOffset activationDate)
            : base(employeeId, "Domain request")
        {
            ActivationDate = activationDate;
        }
    }
}