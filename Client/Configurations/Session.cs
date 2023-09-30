namespace EInvoiceDemo.Client.Configurations;

public static class Session
{
    private static readonly Dictionary<string, object> SessionDictionary = new();
    public static event Action OnChange;

    public static void Set(string key, object value)
    {
        if (SessionDictionary.ContainsKey(key))
            SessionDictionary[key] = value;
        else
            SessionDictionary.Add(key, value);
        OnChange?.Invoke();
    }

    public static object? Get(string key) => SessionDictionary.ContainsKey(key) ? SessionDictionary[key] : null;
    

    public static void Delete(string key) => SessionDictionary.Remove(key);

    public static void Clear() => SessionDictionary.Clear();
    
    //public void Set(ComponentBase source, string key, object value)
    //{
    //    if (Session.ContainsKey(key))
    //        Session[key] = value;
    //    else
    //        Session.Add(key, value);
    //    //NotifyStateChanged(source, key);
    //    OnChange?.Invoke();
    //}

    //public T Get<T>(string key)
    //{
    //    return (T)Get(key);
    //}

    //public event Action<ComponentBase, string> OnStateChange;

    //public void NotifyStateChanged(ComponentBase source, string key)
    //{
    //    OnStateChange?.Invoke(source, key);
    //}
}
