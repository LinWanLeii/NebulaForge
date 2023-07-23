using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectilePooler : MonoBehaviour
{
    public static BossProjectilePooler instance;

    private void Awake()
    {
        if (instance == null) {
            instance = this;
        }
        else if (instance != null) {
            Destroy(this);
        }
        //DontDestroyOnLoad(this);
    }

    [System.Serializable]
    public class BossProjectilePool
    {
        public string poolTag;
        public GameObject poolPrefab;
        public int poolSize;
    }

    public Transform parent;
    public List<BossProjectilePool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    // Start is called before the first frame update
    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (BossProjectilePool p in pools) {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < p.poolSize; i++)
            {
                GameObject o = Instantiate(p.poolPrefab, parent);
                o.SetActive(false);
                objectPool.Enqueue(o);
            }

            poolDictionary.Add(p.poolTag, objectPool);
        }
    }

    public GameObject SpawnFromPool (string _poolTag, Vector3 _pos, Quaternion _rotation) {
        if (!poolDictionary.ContainsKey(_poolTag)) {
            Debug.Log("Invalid pool tag @ BossProjectilePooler.cs");
            return null;
        }

        GameObject obj = poolDictionary[_poolTag].Dequeue();
        
        obj.SetActive(true);
        obj.transform.position = _pos;
        obj.transform.rotation = _rotation;

        IPooledObject pooledObject = obj.GetComponent<IPooledObject>();

        if (pooledObject != null) {
            pooledObject.OnObjectSpawn();
        }

        poolDictionary[_poolTag].Enqueue(obj);


        Debug.Log(obj.name + ", " + obj.transform.position);

        return obj;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}