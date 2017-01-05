using System;
using System.Diagnostics;
using EventStore.ClientAPI;

namespace CQRSDemo.Query.Handlers
{
    public class SubscribeAllHandler
    {
        /*
         
        Load the handlers before the the subscription
        (Save the position AND the originalevent number in the database per projection)
        When the handler loads, load the last seen position and originalevent number as static numbers 
        In all of the handlers look for the lowest position number and request that as the starting point
        As events come in, dispatch them to the correct handler. Be sure to send position and event number as arguments
        If the event number of the event is lower than the last seen, ignore it, else save it and update the projection 
            record in the database as a single transaction. Could be complicated in Mongo since there are no txns
        
        */

        public static void Start(IEventStoreConnection connection)
        {
            // BUG: for some reason, the callback gets called 4 times for each event
            connection.SubscribeToAllFrom(
                Position.Start,
                CatchUpSubscriptionSettings.Default, async (_, x) =>
                {
                    // OriginalEventNumber is the numer of the event in the entire stream (not the version of the specific stream)
                    // OriginalPosition is the logical position in the event log
                    await EventDispatcher.Dispatch(x);
                    //Console.WriteLine($"Original: {x.OriginalPosition}, Event Position: {x.OriginalEventNumber}");
                });
        }
    }
}