using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HackerAPI.Client.Integration.Test
{
    public class APIxUnitTest
    {
        [Fact]
        public async Task Test_GET()
        {
            using var client = new TestClientProviderA().Client;
            var response = await client.GetAsync("/api");

            response.StatusCode.Should().Be(HttpStatusCode.OK); 

        }
    }
}
