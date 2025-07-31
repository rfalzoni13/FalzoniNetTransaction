using FalzoniNetTransaction.Model;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;

namespace FalzoniNetTransaction.Test.Integration;

public class TransactionEndpointTest : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public TransactionEndpointTest(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task Transaction_Receive_ReturnsCreated()
    {
        // arrange
        HttpClient client = _factory.CreateClient();
        Transaction transaction = new Transaction
        {
            Valor = 1000,
            DataHora = new DateTime(2025, 7, 29, 10, 11, 5)
        };

        // act
        var result = await client.PostAsJsonAsync("/api/transacao", transaction);

        //assert
        result.EnsureSuccessStatusCode();
        Assert.Equal(System.Net.HttpStatusCode.Created, result.StatusCode);
    }

    [Fact]
    public async Task Transaction_Receive_ReturnsUnprocessableEntity()
    {
        // arrange
        HttpClient client = _factory.CreateClient();

        // act
        var result = await client.PostAsJsonAsync("/api/transacao", new
        {
            Valor = -1000,
            DataHora = new DateTime(2025, 7, 29, 10, 11, 5),
        });

        //assert
        Assert.False(result.IsSuccessStatusCode);
        Assert.Equal(System.Net.HttpStatusCode.UnprocessableEntity, result.StatusCode);
    }

    [Fact]
    public async Task Transaction_Receive_BadRequest()
    {
        // arrange
        HttpClient client = _factory.CreateClient();

        // act
        var transcation = null as object;

        var result = await client.PostAsJsonAsync("/api/transacao", transcation);

        //assert
        Assert.False(result.IsSuccessStatusCode);
        Assert.Equal(System.Net.HttpStatusCode.BadRequest, result.StatusCode);
    }

    [Fact]
    public async Task Transaction_Delete_ReturnsOk()
    {
        // arrange
        HttpClient client = _factory.CreateClient();

        // act
        var result = await client.DeleteAsync("/api/transacao");

        //assert
        result.EnsureSuccessStatusCode();
        Assert.Equal(System.Net.HttpStatusCode.OK, result.StatusCode);
    }
}
