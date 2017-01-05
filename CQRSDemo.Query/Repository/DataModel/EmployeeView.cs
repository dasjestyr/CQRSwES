using System;
using MongoRepository;

namespace CQRSDemo.Query.Repository.DataModel
{
    internal class EmployeeView : Entity
    {
        public Guid Identifier { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool Active { get; set; }
    }
}
