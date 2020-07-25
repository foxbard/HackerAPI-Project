using hackerAPIClient;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace HackerAPI.Client.Integration.Test
{
    public class TestClientProviderA : IDisposable
    {
        private readonly TestServer nServer;
        public HttpClient Client { get; private set; }
        public TestClientProviderA()
        {
            nServer = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            Client = nServer.CreateClient();
        }

        public void Dispose()
        {
            //dispose of test server/client
            nServer?.Dispose();
            Client?.Dispose();
        }
    }
}
