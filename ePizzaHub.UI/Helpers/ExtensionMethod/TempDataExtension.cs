using Microsoft.AspNetCore.Mvc.ViewFeatures;

using System.Text.Json;

namespace ePizzaHub.UI.Helpers.ExtensionMethod
{
    public static class TempDataExtension
    {

        public static void Set<T>(this ITempDataDictionary tempdata,string key,T value) where T : class
        {
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles
            };
            tempdata[key] = System.Text.Json.JsonSerializer.Serialize(value,options);
        }
        public static T Peek<T>(this ITempDataDictionary tempdata, string key) where T : class
        {
            object o=tempdata.Peek(key);
            return 0==null?null : JsonSerializer.Deserialize<T>((string)o);

        }
        public static T Get<T>(this ITempDataDictionary tempdata, string key,T value) where T : class
        {
            tempdata.TryGetValue(key,out var obj);
            return 0 == null ? null : JsonSerializer.Deserialize<T>((string)obj);
        }
    }
}
