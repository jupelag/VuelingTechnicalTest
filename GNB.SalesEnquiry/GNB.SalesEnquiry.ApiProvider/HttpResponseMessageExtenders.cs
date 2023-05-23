using System.Text.Json;

namespace GNB.SalesEnquiry.ApiProvider
{
    public static class HttpResponseMessageExtenders
    {
        public static async Task<T?> DeserializeAsync<T>(this HttpResponseMessage result)
        {
            try
            {
                return await JsonSerializer.DeserializeAsync<T>(await result.Content.ReadAsStreamAsync());
            }
            catch (ArgumentNullException e)
            {
                throw new InvalidOperationException($"Error deserializing: {e.Message}", e);
            }
            catch (JsonException e)
            {
                throw new InvalidOperationException($"Error deserializing: {e.Message}", e);
            }
            catch (NotSupportedException e)
            {
                throw new InvalidOperationException($"Error deserializing: {e.Message}", e);
            }
        }
    }
}
