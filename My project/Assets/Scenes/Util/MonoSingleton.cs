using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    public static T I
    {
        get
        {
            if (instance == null)
            {
                // Scene 내에 이미 인스턴스가 있는지 확인
                instance = FindObjectOfType<T>();

                if (instance == null)
                {
                    // 인스턴스가 없다면 새로 생성
                    GameObject obj = new GameObject();
                    instance = obj.AddComponent<T>();
                    obj.name = typeof(T).Name + " (Singleton)";

                    // 씬 전환 시 파괴되지 않도록 설정
                    DontDestroyOnLoad(obj);
                }
            }
            return instance;
        }
    }

    protected virtual void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            if (this != instance)
                Destroy(gameObject);
        }
    }
}
