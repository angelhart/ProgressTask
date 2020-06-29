using AsyncSubmit.Providers.Contracts;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace AsyncSubmit.Providers
{
    public class JsonProvider : IDataParser
    {
        private static readonly JsonSerializerOptions jsonOptions = new JsonSerializerOptions { DictionaryKeyPolicy = JsonNamingPolicy.CamelCase };

        public StringContent Serialize<T>(T model)
        {
            return new StringContent(
                JsonSerializer.Serialize(model, jsonOptions),
                Encoding.UTF8,
                "application/json");
        }
    }
}
