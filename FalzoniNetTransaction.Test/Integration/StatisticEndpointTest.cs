using FalzoniNetTransaction.Model;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;

namespace FalzoniNetTransaction.Test.Integration
{
    public class StatisticEndpointTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public StatisticEndpointTest(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Statistic_GetStatistics_ReturnsOk()
        {
            // arrange
            HttpClient client = _factory.CreateClient();

            // act
            var result = await client.GetAsync("/api/estatistica");
            var content = await result.Content.ReadFromJsonAsync<SummaryStatistics>();

            //assert
            result.EnsureSuccessStatusCode();
            Assert.Equal(System.Net.HttpStatusCode.OK, result.StatusCode);
            Assert.Equal(typeof(SummaryStatistics), content.GetType());
        }
    }
}
