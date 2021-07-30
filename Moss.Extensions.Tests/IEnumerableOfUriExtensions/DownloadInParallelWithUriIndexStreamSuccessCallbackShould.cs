using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using MoreLinq;
using Xunit;

namespace Moss.Extensions.Tests.IEnumerableOfUriExtensions
{
    public class DownloadInParallelWithUriIndexStreamSuccessCallbackShould : IEnumerableOfUriExtensionsTests
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
            }, maxDownloadsInParallel, CancellationToken.None).ConfigureAwait(false);

            // assert
            AssertEventSequence(maxDownloadsInParallel);

            AssertTestDatas();
        }
    }
}
