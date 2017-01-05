using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Web.Http;
using CQRSDemo.Domain.Account;
using CQRSDemo.Domain.Data.Repository;
using DAS.Infrastructure.EventStore;
using DAS.Infrastructure.Practices.EventSourcing;
using EventStore.ClientAPI;
using EventStore.ClientAPI.Common.Log;

namespace CQRSDemo.Host.Controllers
{
    [RoutePrefix("employee")]
    public class EmployeeController : ApiController
    {
        private readonly EventStoreRepository<Employee> _repo;
        
        public EmployeeController()
        {
            // TODO: tempf
            var settings = ConnectionSettings.Create()
                .EnableVerboseLogging()
                .UseCustomLogger(new ConsoleLogger())
                .UseDebugLogger();

            var connection = EventStoreConnection.Create(settings, new IPEndPoint(IPAddress.Parse("192.168.1.209"), 1113));
            _repo = new EmployeeStore(connection);
            _repo.ConnectAsync().Wait();
        }

        [HttpPost]
        public async Task<IHttpActionResult> CreateEmployee(FormDataCollection data)
        {
            var firstName = data.Get("fname");
            var lastName = data.Get("lname");

            var employee = Employee.Create(firstName, lastName);
            employee.Activate();

            try
            {
                await _repo.Save(employee);
            }
            catch (Exception ex)
            {
                Debug.Write(ex.Message);
                throw;
            }

            return Created($"/employee/{employee.Id}", employee);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IHttpActionResult> DeactivateEmployee(Guid id)
        {
            var employee = await _repo.GetById(id, true);
            if (employee == null)
                return NotFound();

            employee.Deactivate();
            await _repo.Save(employee);

            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpPost]
        [Route("{id:Guid}/reactivate")]
        public async Task<IHttpActionResult> ReactivateEmployee(Guid id)
        {
            var employee = await _repo.GetById(id, true);
            if (employee == null)
                return NotFound();

            employee.Activate();
            await _repo.Save(employee);

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}