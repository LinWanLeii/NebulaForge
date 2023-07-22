using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObjectsPooler : MonoBehaviour
{
    public static PlayerObjectsPooler instance;

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
    public class PlayerPool
    {
        public string poolTag;
        public GameObject poolPrefab;
        public int poolSize;
    }

    public Transform parent;
    public List<PlayerPool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    // Start is called before the first frame update
    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (PlayerPool p in pools) {
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
            Debug.Log("Invalid pool tag @ PlayerObjectsPooler.cs");
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

        return obj;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
