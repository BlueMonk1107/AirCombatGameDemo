using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _single;

    public static T Single
    {
        get
        {
            if (_single == null)
            {
                var go = new GameObject(typeof(T).Name);
                DontDestroyOnLoad(go);
                _single = go.AddComponent<T>();
            }

            return _single;
        }
    }
}