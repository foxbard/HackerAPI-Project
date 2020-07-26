using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HackerAPI.Client.Integration.Test
{
    public class ItemsXUnitTest
    {

        [Fact]
        public async Task Test_GetNewestStoriesNoCache()
        {
            using var client = new TestClientProviderA().Client;
            var response = await client.GetAsync("/api/items/getNewStoriesids");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.RequestMessage.Method.Should().Be(response.RequestMessage.Method);
            response.Content.Headers.ContentType.Should().Be(response.Content.Headers.ContentType);


        }

        [Fact]
        public async Task Test_GetNewestStories()
        {
            using var client = new TestClientProviderA().Client;
            var response = await client.GetAsync("/api/items/getNewStories");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.RequestMessage.Method.Should().Be(response.RequestMessage.Method);
            response.Content.Headers.ContentType.Should().Be(response.Content.Headers.ContentType);


        }

        [Fact]
        public async Task Test_GetTopStories()
        {
            using var client = new TestClientProviderA().Client;
            var response = await client.GetAsync("/api/items/getTopStories");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.RequestMessage.Method.Should().Be(response.RequestMessage.Method);
            response.Content.Headers.ContentType.Should().Be(response.Content.Headers.ContentType);



        }

        [Fact]
        public async Task Test_GetBestStories()
        {
            using var client = new TestClientProviderA().Client;
            var response = await client.GetAsync("/api/items/getBestStories");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.RequestMessage.Method.Should().Be(response.RequestMessage.Method);
            response.Content.Headers.ContentType.Should().Be(response.Content.Headers.ContentType);


        }

        [Fact]
        public async Task Test_GetStoryById()
        {
            using var client = new TestClientProviderA().Client;
            var response = await client.GetAsync("/api/items/getStoryById/23941710");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.RequestMessage.Method.Should().Be(response.RequestMessage.Method);
            response.Content.Headers.ContentType.Should().Be(response.Content.Headers.ContentType);


        }
    }
}
