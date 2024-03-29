using FluentAssertions;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace HackerAPI.Client.Integration.Test
{
    public class UsersxUnitTest
    {
        [Fact]
        public async Task Test_GetUserByName()
        {
            using var client = new TestClientProviderA().Client;
            var response = await client.GetAsync("/api/users/getUserByName/fipar");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.RequestMessage.Method.Should().Be(response.RequestMessage.Method);
            response.Content.Headers.ContentType.Should().Be(response.Content.Headers.ContentType);


        }

        [Fact]
        public async Task Test_GetUserSubmittedByName()
        {
            using var client = new TestClientProviderA().Client;
            var response = await client.GetAsync("/api/users/getUserItemsByName/fipar");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.RequestMessage.Method.Should().Be(response.RequestMessage.Method);
            response.Content.Headers.ContentType.Should().Be(response.Content.Headers.ContentType);

        }
    }
}
