using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using MoreLinq;
using Shouldly;
using Xunit;

namespace Moss.Extensions.Tests.IEnumerableOfUriExtensions
{
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
            await TestDatas.Select(x => x.Uri).DownloadInParallel(new HttpClient(HttpHandlerMock.Object), null, (uri, i, responseCode) =>
            {
                Events.Add("end");

                var matchingTestData = TestDatas.Single(x => x.Uri == uri);

                matchingTestData.ResponseIndex = i;
                matchingTestData.StatusCode = responseCode;

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
}
