using Newtonsoft.Json.Linq;

namespace ExtremeSkins.Converter.Core.Extension;

public static class JTokenExtension
{
    public static string GetStringValue(this JToken token, object key)
    {
        string value = token.Value<string>(key);
        
        return string.IsNullOrEmpty(value) ? string.Empty : value;
    }
    public static bool TryGetValue<T>(this JToken token, object key, out T outValue)
    {
        outValue = token.Value<T>(key);
        return outValue is not null;
    }
}
