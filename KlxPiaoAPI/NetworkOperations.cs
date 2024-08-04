namespace KlxPiaoAPI
{
    /// <summary>
    /// 提供网络相关操作的功能。
    /// </summary>
    public class NetworkOperations
    {
        /// <summary>
        /// 异步获取指定 URL 的页面内容。
        /// </summary>
        /// <param name="url">要获取内容的 URL 地址。</param>
        /// <returns>页面内容的字符串表示。</returns>
        public static async Task<string> GetHTMLContentAsync(string url)
        {
            HttpClient client = new();

            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            return responseBody;
        }

        /// <summary>
        /// 尝试异步获取指定 URL 的页面内容，返回操作结果和错误信息（如有）。
        /// </summary>
        /// <param name="url">要获取内容的 URL 地址。</param>
        /// <returns>
        /// <para>如果请求成功，元组的第一个元素为 true，第二个元素为页面内容，第三个元素为空字符串</para>
        /// <para>如果请求失败，元组的第一个元素为 false，第二个元素为空字符串，第三个元素为异常消息</para>
        /// </returns>
        public static async Task<(bool success, string content, string failMessage)> TryGetHTMLContentAsync(string url)
        {
            HttpClient client = new();
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                return (true, responseBody, string.Empty);
            }
            catch (Exception ex)
            {
                return (false, string.Empty, ex.Message);

            }
        }

        /// <summary>
        /// 下载文件并保存到指定路径。
        /// </summary>
        /// <param name="fileUrl">文件的 URL。</param>
        /// <param name="destinationPath">下载后文件的保存路径。</param>
        /// <param name="bufferSize">用于读取文件内容的缓冲区大小，单位为字节（默认为 4096）。</param>
        /// <returns>一个表示异步操作的任务。</returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="HttpRequestException"></exception>
        public static async Task DownloadFileAsync(string fileUrl, string destinationPath, int bufferSize = 4096)
        {
            if (bufferSize <= 0)
            {
                throw new ArgumentException("缓冲区大小必须大于 0。", nameof(bufferSize));
            }

            using HttpClient client = new();
            using var response = await client.GetAsync(fileUrl, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();
            using Stream contentStream = await response.Content.ReadAsStreamAsync(),
                   fileStream = new FileStream(destinationPath, FileMode.Create, FileAccess.Write, FileShare.None, bufferSize, true);
            await contentStream.CopyToAsync(fileStream);
        }

        /// <summary>
        /// 从指定的 URL 获取图像并将其转换为 Bitmap 对象。
        /// 如果指定了大小，则调整图像到该大小。
        /// </summary>
        /// <param name="url">图像的 URL。</param>
        /// <param name="size">可选参数，指定返回图像的大小。如果未提供，则返回原始大小的图像。</param>
        /// <returns>返回的 Bitmap 对象。</returns>
        public static async Task<Bitmap> GetImageFromUrlAsync(string url, Size? size = null)
        {
            using HttpClient client = new();
            byte[] imageBytes = await client.GetByteArrayAsync(url);

            using MemoryStream ms = new(imageBytes);
            Bitmap originalBitmap = new(ms);

            if (size.HasValue)
            {
                return new Bitmap(originalBitmap, size.Value);
            }
            else
            {
                return originalBitmap;
            }
        }
    }
}