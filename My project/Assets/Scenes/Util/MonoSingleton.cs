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
                // Scene ���� �̹� �ν��Ͻ��� �ִ��� Ȯ��
                instance = FindObjectOfType<T>();

                if (instance == null)
                {
                    // �ν��Ͻ��� ���ٸ� ���� ����
                    GameObject obj = new GameObject();
                    instance = obj.AddComponent<T>();
                    obj.name = typeof(T).Name + " (Singleton)";

                    // �� ��ȯ �� �ı����� �ʵ��� ����
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
