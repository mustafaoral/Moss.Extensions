using MoreLinq;

namespace Moss.Extensions.Tests.IEnumerableOfUriExtensions;

public class DownloadInParallelWithUriStreamSuccessCallbackShould : IEnumerableOfUriExtensionsTests
{
    [Fact]
    public async Task DownloadContentUsingMaxDownloadsInParallel_AndCallSuccessCallback()
    {
        // arrange
        var maxDownloadsInParallel = Random.Next(5, 20);

        SetUpHttpHandlerMockForSuccess();

        // act
        await TestDatas.Select(x => x.Uri).DownloadInParallel(new HttpClient(HttpHandlerMock.Object), async (uri, stream) =>
        {
            Events.Add("end");

            var matchingTestData = TestDatas.Single(x => x.Uri == uri);

            matchingTestData.ResponseContent = await GetGuidFromResponseStream(stream);
        }, maxDownloadsInParallel, CancellationToken.None).ConfigureAwait(false);

        // assert
        AssertEventSequence(maxDownloadsInParallel);

        foreach (var td in TestDatas)
        {
            td.ResponseContent.ShouldBe(td.Guid);
        }
    }
}
