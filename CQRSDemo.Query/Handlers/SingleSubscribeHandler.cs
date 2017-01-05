using System.Diagnostics;
using System.Net;
using EventStore.ClientAPI;
using EventStore.ClientAPI.SystemData;

namespace CQRSDemo.Query.Handlers
{
    public class SingleSubscribeHandler
    {
        private const string StreamCategory = "$ce-Employee";

        private static readonly int _lastSeen = 0;

        public static void Start(IEventStoreConnection connection)
        {
            var subscriptionSettings = CatchUpSubscriptionSettings.Default;
            
            Trace.WriteLine("Subscribing...");
            connection.SubscribeToStreamFrom(StreamCategory, _lastSeen, subscriptionSettings, OnEvent);
        }

        private static void OnEvent(EventStoreCatchUpSubscription sub, ResolvedEvent e)
        {
            var data = e.Event.EventType;
            Trace.WriteLine($"Received event {data}. Position {e.OriginalEventNumber}");
        }
    }
}
