public interface IDataMemory
{
    T Get<T>(string key);
    void Set<T>(string key, T value);
    void Clear(string key);
    void ClearAll();
    bool Contains(string key);

    void SetObject(string key, object value);
    object GetObject(string key);
}