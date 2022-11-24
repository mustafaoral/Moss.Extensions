using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Moss.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="IEnumerable{T}"/> of <see cref="Uri"/>
    /// </summary>
    public static partial class IEnumerableOfUriExtensions
    {
        /// <summary>
        /// Downloads resources expressed in the URIs in parallel using a supplied <see cref="HttpClient"/> and invokes a callback passing the URI and response stream.
        /// </summary>
        /// <param name="values">Values.</param>
        /// <param name="httpClient">HTTP client.</param>
        /// <param name="callback">Callback to invoke.</param>
        /// <param name="maxDownloadsInParallel">Number of maximum downloads to execute in parallel.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <remarks>A 200 Success response is assumed.</remarks>
        public static async Task DownloadInParallel(this IEnumerable<Uri> values, HttpClient httpClient, Func<Uri, Stream, Task> callback, int maxDownloadsInParallel, CancellationToken cancellationToken)
        {
            var throttler = new SemaphoreSlim(initialCount: maxDownloadsInParallel);

            var tasks = values.Select(async uri =>
            {
                await throttler.WaitAsync(cancellationToken).ConfigureAwait(false);

                try
                {
                    using (var response = await httpClient.GetAsync(uri, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false))
                    using (var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                    {
                        await callback(uri, stream).ConfigureAwait(false);
                    }
                }
                finally
                {
                    throttler.Release();
                }
            });

            await Task.WhenAll(tasks).ConfigureAwait(false);
        }

        /// <summary>
        /// Downloads resources expressed in the URIs in parallel using a supplied <see cref="HttpClient"/> and invokes a callback passing the URI, index, and response stream.
        /// </summary>
        /// <param name="values">Values.</param>
        /// <param name="httpClient">HTTP client.</param>
        /// <param name="callback">Callback to invoke.</param>
        /// <param name="maxDownloadsInParallel">Number of maximum downloads to execute in parallel.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <remarks>A 200 Success response is assumed.</remarks>
        public static async Task DownloadInParallel(this IEnumerable<Uri> values, HttpClient httpClient, Func<Uri, int, Stream, Task> callback, int maxDownloadsInParallel, CancellationToken cancellationToken)
        {
            var throttler = new SemaphoreSlim(initialCount: maxDownloadsInParallel);

            var tasks = values.Select(async (uri, i) =>
            {
                await throttler.WaitAsync(cancellationToken).ConfigureAwait(false);

                try
                {
                    using (var response = await httpClient.GetAsync(uri, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false))
                    using (var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                    {
                        await callback(uri, i, stream).ConfigureAwait(false);
                    }
                }
                finally
                {
                    throttler.Release();
                }
            });

            await Task.WhenAll(tasks).ConfigureAwait(false);
        }

        /// <summary>
        /// Downloads resources expressed in the URIs in parallel using a supplied <see cref="HttpClient"/>.
        /// If the status code of the HTTP response is in the range of 200-299, invokes a success callback passing the URI, index, and response stream.
        /// For other status codes, invokes an error callback passing in the URI, index, and response.
        /// </summary>
        /// <param name="values">Values</param>
        /// <param name="httpClient">HTTP client.</param>
        /// <param name="successcallback">Success callback to invoke.</param>
        /// <param name="errorCallback">Error callback to invoke.</param>
        /// <param name="maxDownloadsInParallel">Number of maximum downloads to execute in parallel.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        public static async Task DownloadInParallel(this IEnumerable<Uri> values, HttpClient httpClient, Func<Uri, int, Stream, Task> successcallback, Func<int, HttpResponseMessage, Task> errorCallback, int maxDownloadsInParallel, CancellationToken cancellationToken)
        {
            var throttler = new SemaphoreSlim(initialCount: maxDownloadsInParallel);

            var tasks = values.Select(async (uri, i) =>
            {
                await throttler.WaitAsync(cancellationToken).ConfigureAwait(false);

                try
                {
                    using (var response = await httpClient.GetAsync(uri, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false))
                    {
                        if (!response.IsSuccessStatusCode)
                        {
                            await errorCallback(i, response).ConfigureAwait(false);

                            return;
                        }

                        using (var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                        {
                            await successcallback(uri, i, stream).ConfigureAwait(false);
                        }
                    }
                }
                finally
                {
                    throttler.Release();
                }
            });

            await Task.WhenAll(tasks).ConfigureAwait(false);
        }
    }
}
