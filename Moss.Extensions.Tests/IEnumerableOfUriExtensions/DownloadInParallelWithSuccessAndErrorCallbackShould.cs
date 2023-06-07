using System.Net;
using MoreLinq;

namespace Moss.Extensions.Tests.IEnumerableOfUriExtensions;

public class DownloadInParallelWithSuccessAndErrorCallbackShould : IEnumerableOfUriExtensionsTests
{
    [Fact]
    public async Task DownloadContentUsingMaxDownloadsInParallel_AndCallSuccessCallback()
    {
        // arrange
        var maxDownloadsInParallel = Random.Next(5, 20);

        SetUpHttpHandlerMockForSuccess();

        // act
        await TestDatas.Select(x => x.Uri).DownloadInParallel(new HttpClient(HttpHandlerMock.Object), async (uri, i, stream) =>
        {
            Events.Add("end");

            var matchingTestData = TestDatas.Single(x => x.Uri == uri);

            matchingTestData.ResponseIndex = i;
            matchingTestData.ResponseContent = await GetGuidFromResponseStream(stream);
        }, null, maxDownloadsInParallel, CancellationToken.None);

        // assert
        AssertEventSequence(maxDownloadsInParallel);
        AssertTestDatas();
    }

    [Fact]
    public async Task DownloadContentUsingMaxDownloadsInParallel_AndCallErrorCallback()
    {
        // arrange
        var maxDownloadsInParallel = Random.Next(5, 20);

        SetUpHttpHandlerMockForFailure();

        // act
        await TestDatas.Select(x => x.Uri).DownloadInParallel(new HttpClient(HttpHandlerMock.Object), null, (i, response) =>
        {
            Events.Add("end");

            var matchingTestData = TestDatas.Single(x => x.Uri == response.RequestMessage.RequestUri);

            matchingTestData.ResponseIndex = i;
            matchingTestData.StatusCode = response.StatusCode;

            return Task.CompletedTask;
        }, maxDownloadsInParallel, CancellationToken.None);

        // assert
        AssertEventSequence(maxDownloadsInParallel);

        foreach (var td in TestDatas)
        {
            td.ResponseIndex.ShouldBe(td.Index);
            td.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }
    }
}
