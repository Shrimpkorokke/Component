using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// ������Ʈ Ǯ���� ���
public class ObjectPoolManager : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (var pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
                obj.transform.SetParent(this.transform);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    GameObject CreateNewObject(GameObject prefab)
    {
        GameObject obj = Instantiate(prefab);
        obj.SetActive(false);
        obj.transform.SetParent(this.transform);
        return obj;
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning($"Pool with tag {tag} doesn't exist.");
            return null;
        }

        // ��Ȱ��ȭ�� ������Ʈ ã��
        GameObject objectToSpawn = null;
        foreach (var obj in poolDictionary[tag])
        {
            // ��Ȱ��ȭ�� ������Ʈ�� �ִٸ�
            if (!obj.activeInHierarchy)
            {
                objectToSpawn = obj;
                break;
            }
        }

        // ��Ȱ��ȭ�� ������Ʈ�� ���ٸ� ���ο� ������Ʈ ����
        if (objectToSpawn == null)
        {
            Pool pool = pools.Find(x => x.tag == tag);
            if (pool != null)
            {
                objectToSpawn = CreateNewObject(pool.prefab);

                // ���� ������ ������Ʈ�� Ǯ�� �߰�
                poolDictionary[tag].Enqueue(objectToSpawn); 
            }
            else
            {
                Debug.LogWarning($"No pool found with tag: {tag}");
                return null;
            }
        }
        else
        {
            // ��Ȱ��ȭ�� ������Ʈ�� Ǯ���� ����
            poolDictionary[tag] = new Queue<GameObject>(poolDictionary[tag].Where(x => x != objectToSpawn));
        }

        // ������Ʈ Ȱ��ȭ �� ��ġ�� ȸ�� ����
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        // IPooledObject �������̽��� ���� ��� OnObjectSpawn ȣ�� (������Ʈ�� ������ ���� �ʿ��� �۾��� ����)
        IPooledObject pooledObj = objectToSpawn.GetComponentInChildren<IPooledObject>();

        if (pooledObj != null)
        {
            pooledObj.OnObjectSpawn();
        }

        return objectToSpawn;
    }

    public void ReturnToPool(string tag, GameObject objectToReturn)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning($"Pool with tag {tag} doesn't exist.");
            return;
        }

        IPooledObject pooledObj = objectToReturn.GetComponent<IPooledObject>();

        // IPooledObject �������̽��� ���� ��� OnObjectReturn ȣ�� (������Ʈ�� Ǯ�� ��ȯ�Ǳ� ���� �ʿ��� �۾��� ����)
        if (pooledObj != null)
        {
            pooledObj.OnObjectReturn(); 
        }

        objectToReturn.SetActive(false);
        poolDictionary[tag].Enqueue(objectToReturn);
    }
}
