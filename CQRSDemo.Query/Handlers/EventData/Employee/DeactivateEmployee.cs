using System;

namespace CQRSDemo.Query.Handlers.EventData.Employee
{
    public class DeactivateEmployee
    {
        public DateTimeOffset DeactivationDate { get; set; }
    }
}