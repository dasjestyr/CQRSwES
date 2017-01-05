using System;

namespace CQRSDemo.Query.Handlers.EventData.Employee
{
    public class CreateEmployee
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}