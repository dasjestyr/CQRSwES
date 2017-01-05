using CQRSDemo.Domain.Account;
using DAS.Infrastructure.EventStore;
using EventStore.ClientAPI;

namespace CQRSDemo.Domain.Data.Repository
{
    public class EmployeeStore : EventStoreRepository<Employee>
    {
        public EmployeeStore(IEventStoreConnection connection, int maxFetchSize = 1000) 
            : base("Employee", connection, maxFetchSize)
        {

        }
    }
}