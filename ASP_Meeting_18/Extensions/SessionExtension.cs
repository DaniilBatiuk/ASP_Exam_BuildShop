using System.Text.Json.Serialization;
using System.Text.Json;

namespace ASP_Meeting_18.Extensions
{
    public static class SessionExtension
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                ReferenceHandler = ReferenceHandler.Preserve
            };
            session.SetString(key, JsonSerializer.Serialize(value, options));
        }

        public static T? Get<T>(this ISession session, string key)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                ReferenceHandler = ReferenceHandler.Preserve
            };
            string? str = session.GetString(key);
            return str != null ? JsonSerializer.Deserialize<T>(str, options) : default;
        }
    }
}
