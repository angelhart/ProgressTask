using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AsyncSubmit.Providers
{
    public static class JsonProvider<T>
    {
        private static readonly JsonSerializerOptions jsonOptions = new JsonSerializerOptions { DictionaryKeyPolicy = JsonNamingPolicy.CamelCase };

        public static StringContent Serialize(T model)
        {
            return new StringContent(
                JsonSerializer.Serialize(model, jsonOptions),
                Encoding.UTF8,
                "application/json");
        }
    }
}
