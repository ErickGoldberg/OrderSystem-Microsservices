namespace OrderSystem.Common.Docker
{
    public class HealthCheckHelper
    {
        public static async Task<bool> CheckServiceHealthAsync(string serviceUrl)
        {
            using var httpClient = new HttpClient();

            try
            {
                var response = await httpClient.GetAsync(serviceUrl);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
    }
}
