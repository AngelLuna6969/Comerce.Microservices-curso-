using System.Text.Json;

namespace Service.Common.Mapping
{
    public static class DTOMapperExtension
    {
        public static T Mapto<T>(this object value)
        {
            return JsonSerializer.Deserialize<T>(JsonSerializer.Serialize(value));
        }
    }
}
