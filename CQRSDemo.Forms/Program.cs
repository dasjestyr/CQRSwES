using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;
using CQRSDemo.Query.Handlers;
using EventStore.ClientAPI;
using EventStore.ClientAPI.Common.Log;
using EventStore.ClientAPI.SystemData;

namespace CQRSDemo.Forms
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var connection = GetConnection().Result;
            //SingleSubscribeHandler.Start(connection);
            SubscribeAllHandler.Start(connection);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(new TestForm(connection));
        }

        private static async Task<IEventStoreConnection> GetConnection()
        {
            var settings = ConnectionSettings.Create()
                .EnableVerboseLogging()
                .UseCustomLogger(new ConsoleLogger())
                .UseDebugLogger()
                .SetDefaultUserCredentials(new UserCredentials("admin", "changeit"));

            var connection = EventStoreConnection.Create(settings, new IPEndPoint(IPAddress.Parse("192.168.1.209"), 1113));
            await connection.ConnectAsync();
            return connection;
        }
    }
}
