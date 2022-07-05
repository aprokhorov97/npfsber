using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace CurrencyRate.Infrastructure.Helpers.Serializators
{
    public class JsonHelper<T>
    {
        public static string Serialize(T data)
        {
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping

            };
            return JsonSerializer.Serialize<T>(data, options);
        }
    }
}
