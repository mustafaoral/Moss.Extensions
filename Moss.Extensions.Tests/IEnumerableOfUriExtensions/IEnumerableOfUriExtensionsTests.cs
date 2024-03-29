﻿using System.Net;
using System.Text;
using MoreLinq;

namespace Moss.Extensions.Tests.IEnumerableOfUriExtensions;

public abstract class IEnumerableOfUriExtensionsTests
{
    protected class TestData
    {
        public Guid Guid { get; set; }
        public int Index { get; set; }
        public Uri Uri { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public Guid ResponseContent { get; set; }
        public int ResponseIndex { get; set; }
    }

    protected static readonly Random Random = new Random();

    protected readonly Mock<HttpMessageHandler> HttpHandlerMock = new Mock<HttpMessageHandler>();

    protected readonly TestData[] TestDatas = CreateTestData();
    protected readonly List<string> Events = new List<string>();

    protected void SetUpHttpHandlerMockForSuccess()
    {
        foreach (var td in TestDatas)
        {
            HttpHandlerMock.Protected()
                           .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.Is<HttpRequestMessage>(x => x.RequestUri == td.Uri), ItExpr.IsAny<CancellationToken>())
                           .Callback(() => Events.Add("start"))
                           .ReturnsAsync(new HttpResponseMessage()
                           {
                               StatusCode = HttpStatusCode.OK,
                               Content = new ByteArrayContent(Encoding.UTF8.GetBytes(td.Guid.ToString("N")))
                           }, TimeSpan.FromSeconds(1) + TimeSpan.FromSeconds(Random.NextDouble()));
        }
    }

    protected void SetUpHttpHandlerMockForFailure()
    {
        foreach (var td in TestDatas)
        {
            HttpHandlerMock.Protected()
                           .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.Is<HttpRequestMessage>(x => x.RequestUri == td.Uri), ItExpr.IsAny<CancellationToken>())
                           .Callback(() => Events.Add("start"))
                           .ReturnsAsync(new HttpResponseMessage()
                           {
                               StatusCode = HttpStatusCode.NotFound,
                               RequestMessage = new HttpRequestMessage
                               {
                                   RequestUri = td.Uri
                               }
                           }, TimeSpan.FromSeconds(1) + TimeSpan.FromSeconds(Random.NextDouble()));
        }
    }

    protected void AssertTestDatas()
    {
        foreach (var td in TestDatas)
        {
            td.ResponseContent.ShouldBe(td.Guid);
            td.ResponseIndex.ShouldBe(td.Index);
        }
    }

    protected void AssertEventSequence(int maxDownloadsInParallel)
    {
        Events.Take(maxDownloadsInParallel).ShouldAllBe(x => x == "start", $"{Events.JoinWithComma()} does not have expected start sequence. {nameof(maxDownloadsInParallel)}: {maxDownloadsInParallel}");

        var longestSequence = MoreEnumerable.MaxBy(Events.GroupAdjacent(x => x), x => x.Count()).First();
        longestSequence.Count().ShouldBeLessThanOrEqualTo(maxDownloadsInParallel);
    }

    protected async Task<Guid> GetGuidFromResponseStream(Stream stream)
    {
        using (var ms = new MemoryStream())
        {
            await stream.CopyToAsync(ms).ConfigureAwait(false);

            var content = Encoding.UTF8.GetString(ms.ToArray());

            return Guid.Parse(content);
        }
    }

    protected static TestData[] CreateTestData()
    {
        return Enumerable.Range(1, 50)
                         .Select(_ => Guid.NewGuid())
                         .Select((x, i) => new TestData
                         {
                             Guid = x,
                             Index = i,
                             Uri = new Uri($"http://foo.com/{x:N}")
                         })
                         .ToArray();
    }
}
