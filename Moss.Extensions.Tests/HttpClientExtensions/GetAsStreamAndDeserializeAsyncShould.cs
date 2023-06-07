using System.Net;
using System.Text;
using System.Text.Json;

namespace Moss.Extensions.Tests.HttpClientExtensions;

public class GetAsStreamAndDeserializeAsyncShould
{
    [Fact]
    public async Task DeserializeContentAsStreamUsingDefaultJsonSerializerSettings()
    {
        await DeserializeContentAsStream(null).ConfigureAwait(false);
    }

    [Fact]
    public async Task DeserializeContentAsStreamUsingProvidedJsonSerializerSettings()
    {
        var jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        await DeserializeContentAsStream(jsonSerializerOptions).ConfigureAwait(false);
    }

    private async Task DeserializeContentAsStream(JsonSerializerOptions jsonSerializerOptions)
    {
        var id = Guid.NewGuid();
        var name = Guid.NewGuid().ToString("N");

        var dto = new Dto
        {
            Id = id,
            Name = name
        };

        var requestUri = "http://test.com";

        var handlerMock = new Mock<HttpMessageHandler>();

        handlerMock.Protected()
                   .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.Is<HttpRequestMessage>(x => x.RequestUri == new Uri(requestUri)), ItExpr.IsAny<CancellationToken>())
                   .ReturnsAsync(new HttpResponseMessage()
                   {
                       StatusCode = HttpStatusCode.OK,
                       Content = new ByteArrayContent(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(dto, jsonSerializerOptions))),
                   });

        var httpClient = new HttpClient(handlerMock.Object);

        var result = await httpClient.GetAsStreamAndDeserializeAsync<Dto>(requestUri, jsonSerializerOptions).ConfigureAwait(false);

        result.Id.ShouldBe(id);
        result.Name.ShouldBe(name);
    }

    private class Dto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
