using System;
using System.Diagnostics;
using System.Threading.Tasks;
using CQRSDemo.Query.Handlers.EventData.Employee;

namespace CQRSDemo.Query.Handlers
{
    public interface IDenormalizer
    {
        string TargetCategory { get; }

        Task Process(object eventData);
    }

    public class EmployeeCountDenormalizer : IDenormalizer
    {
        public string TargetCategory => "Employee";

        public async Task Process(object eventData)
        {
            return;
        }
    }

    public class EmployeeViewDenormalizer : IDenormalizer
    {
        public string TargetCategory => "Employee";

        public async Task Process(object eventData)
        {
            Process(eventData as dynamic);
        }

        private static async Task Process(CreateEmployee e)
        {
            Trace.WriteLine($"Processed {e}");
        }

        private static async Task Process(ActivateEmployee e)
        {
            Trace.WriteLine($"Processed {e}");
        }

        private static async Task Process(DeactivateEmployee e)
        {
            Trace.WriteLine($"Processed {e}");
        }
    }
}
