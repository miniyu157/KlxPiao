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

        ////带有Cookie示例：
        //public static async Task<string> GetRawData(string accessToken)
        //{
        //    HttpClient httpClient = new()
        //    {
        //        BaseAddress = new Uri("https://cloud.bs-iot.com/")
        //    };

        //    httpClient.DefaultRequestHeaders.Add("Cookie", $"ACCESS_TOKEN={accessToken}; Path=/; Domain=cloud.bs-iot.com");

        //    var response = await httpClient.GetAsync("ufi");

        //    response.EnsureSuccessStatusCode();

        //    var responseText = await response.Content.ReadAsStringAsync();
        //    return responseText;
        //}
    }
}