using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// 오브젝트 풀링을 담당
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

        // 비활성화된 오브젝트 찾기
        GameObject objectToSpawn = null;
        foreach (var obj in poolDictionary[tag])
        {
            // 비활성화된 오브젝트가 있다면
            if (!obj.activeInHierarchy)
            {
                objectToSpawn = obj;
                break;
            }
        }

        // 비활성화된 오브젝트가 없다면 새로운 오브젝트 생성
        if (objectToSpawn == null)
        {
            Pool pool = pools.Find(x => x.tag == tag);
            if (pool != null)
            {
                objectToSpawn = CreateNewObject(pool.prefab);

                // 새로 생성된 오브젝트를 풀에 추가
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
            // 비활성화된 오브젝트를 풀에서 제거
            poolDictionary[tag] = new Queue<GameObject>(poolDictionary[tag].Where(x => x != objectToSpawn));
        }

        // 오브젝트 활성화 및 위치와 회전 설정
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        // IPooledObject 인터페이스가 있을 경우 OnObjectSpawn 호출 (오브젝트를 꺼내기 전에 필요한 작업을 수행)
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

        // IPooledObject 인터페이스가 있을 경우 OnObjectReturn 호출 (오브젝트가 풀로 반환되기 전에 필요한 작업을 수행)
        if (pooledObj != null)
        {
            pooledObj.OnObjectReturn(); 
        }

        objectToReturn.SetActive(false);
        poolDictionary[tag].Enqueue(objectToReturn);
    }
}
