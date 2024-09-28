using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Utilities.Request
{
    public class RequestBuilder
    {
        //var postResponse = await new RequestBuilder()
        //    .WithMethod(HttpMethod.Post)
        //    .WithUrl("https://test.com/resource")
        //    .WithToken("your_jwt_token")
        //    .WithHeaders(new Dictionary<string, string> { { "X-Correlation-ID", Guid.NewGuid().ToString() } })
        //    .WithBody(new { Name = "Goldberg test", Age = 30 })
        //    .SendAsync();

        //await RequestBuilder.LogResponse(postResponse);

        private HttpMethod? _method;
        private string? _url;
        private string? _token;
        private readonly IDictionary<string, string> _headers = new Dictionary<string, string>();
        private object? _body;

        private static readonly HttpClient HttpClient = new();

        public RequestBuilder WithMethod(HttpMethod? method)
        {
            _method = method;
            return this;
        }

        public RequestBuilder WithUrl(string? url)
        {
            _url = url;
            return this;
        }

        public RequestBuilder WithToken(string? token)
        {
            _token = token;
            return this;
        }

        public RequestBuilder WithHeaders(IDictionary<string, string> headers)
        {
            foreach (var header in headers)
            {
                _headers.Add(header.Key, header.Value);
            }

            return this;
        }

        public RequestBuilder WithBody(object? body)
        {
            _body = body;
            return this;
        }

        private HttpRequestMessage BuildRequest()
        {
            var request = new HttpRequestMessage(_method, _url);

            if (!string.IsNullOrWhiteSpace(_token))
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            
            foreach (var header in _headers)
            {
                request.Headers.Add(header.Key, header.Value);
            }

            if (_body != null && (_method == HttpMethod.Post || _method == HttpMethod.Put))
            {
                var json = JsonSerializer.Serialize(_body);
                request.Content = new StringContent(json, Encoding.UTF8, "application/json");
            }

            return request;
        }

        public async Task<HttpResponseMessage> SendAsync()
        {
            var request = BuildRequest();
            return await HttpClient.SendAsync(request);
        }

        public async Task<T?> SendAsync<T>()
        {
            var request = BuildRequest();
            var response = await HttpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException($"Request failed with status code: {response.StatusCode}");
           
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(json);
        }

        public static async Task LogResponse(HttpResponseMessage response)
        {
            var responseBody = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Status Code: {response.StatusCode}");
            Console.WriteLine($"Response Body: {responseBody}");
        }
    }

}
