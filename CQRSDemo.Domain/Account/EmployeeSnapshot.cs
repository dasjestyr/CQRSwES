using System;
using System.Collections.Generic;
using DAS.Infrastructure.Practices.EventSourcing;

namespace CQRSDemo.Domain.Account
{
    public class EmployeeSnapshot : EventInfo
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTimeOffset? ActivationDate { get; set; }
        public DateTimeOffset? DeactivationDate { get; set; }
        public List<Employee> DirectReports { get; set; }
    }
}