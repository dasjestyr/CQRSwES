using System;
using DAS.Infrastructure.Practices.EventSourcing;

namespace CQRSDemo.Domain.Account
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