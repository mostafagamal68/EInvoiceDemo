namespace EInvoiceDemo.Shared.Helpers;

public static class ObjectExtensions
{
    public static T CastTo<T>(this object value)
        => (T)value;

    public static T? CastAs<T>(this object value) where T : class
        => value as T;

    //public static T CloneJson<T>(this T source)
    //    => JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(source),
    //        new JsonSerializerSettings { ObjectCreationHandling = ObjectCreationHandling.Replace })!;
}
