using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CQRSDemo.Query.Handlers.EventData.Employee;
using EventStore.ClientAPI;
using Newtonsoft.Json;

namespace CQRSDemo.Query.Handlers
{
    public class EventDispatcher
    {
        private static readonly Dictionary<string, Type> TypeMappings;
        private static readonly List<IDenormalizer> Denormalizers;

        static EventDispatcher()
        {
            // TODO: make this injectable

            TypeMappings = new Dictionary<string, Type>
            {
                { "ActivateEmployee", typeof(ActivateEmployee) },
                { "CreateEmployee", typeof(CreateEmployee) },
                { "DeactivateEmployee", typeof(DeactivateEmployee) }
            };

            // TODO: load automatically?
            Denormalizers = new List<IDenormalizer>
            {
                new EmployeeViewDenormalizer(),
                new EmployeeCountDenormalizer()
            };
        }

        public static async Task Dispatch(ResolvedEvent e)
        {
            if (!TypeMappings.ContainsKey(e.Event.EventType))
                return;

            var handlers = Denormalizers
                .Where(d => d.TargetCategory.Equals(ParseCategory(e.OriginalStreamId)))
                .ToList();

            if (!handlers.Any())
                return;
            
            var deserializedEvent = Deserialize(e);

            // TODO: queue
            var tasks = handlers
                .Select(h => h.Process(deserializedEvent))
                .ToList();

            await Task.WhenAll(tasks);
        }

        private static string ParseCategory(string streamId)
        {
            var randomString = Guid.NewGuid().ToString("N");

            if (string.IsNullOrEmpty(streamId))
                return randomString;

            var parts = streamId.Split(new[] {'-'}, StringSplitOptions.RemoveEmptyEntries);

            return parts.Length < 1 
                ? randomString
                : parts[0];
        }

        private static object Deserialize(ResolvedEvent e)
        {
            var obj = JsonConvert.DeserializeObject(
                Encoding.UTF8.GetString(e.Event.Data),
                TypeMappings[e.Event.EventType]);

            return obj;
        }
    }
}
