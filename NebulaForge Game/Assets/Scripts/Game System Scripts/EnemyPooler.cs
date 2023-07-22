using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPooler : MonoBehaviour
{
    [SerializeField]
    private float spawnDistanceFromPlayer = 40.0f; // Should be far enough to not be visible
    [SerializeField]
    private float spawnTime;
    [SerializeField]
    private float spawnTimer;

    public static EnemyPooler instance;

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
    public class EnemyPool
    {
        public string poolTag;
        public GameObject poolPrefab;
        public int poolSize;
    }
    public Transform parent;
    public List<EnemyPool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    // Start is called before the first frame update
    void Start()
    {
        spawnTimer = 0.0f;

        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (EnemyPool p in pools) {
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
            Debug.Log("Invalid pool tag @ EnemyPooler.cs");
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
        TrySpawn("BasicEnemy");

        if (spawnTime >= 0.1f) {
            spawnTime = 1.0f - PlayerStats.instance.GetplayerLv() * 0.1f;
        } else {
            spawnTime = 0.1f;
        }

        if (FPSCameraShift.instance.startShift) {
            parent.gameObject.SetActive(false);
        }
    }

    void TrySpawn(string _poolTag) {
        if (spawnTimer >= spawnTime) {
            spawnTimer = 0;

            // Randomly generate a point on the circumference then offset by adding player pos
            float angle = Random.Range(0, 2 * Mathf.PI);
            float x = spawnDistanceFromPlayer * Mathf.Cos(angle) + PlayerStats.instance.transform.position.x;
            float z = spawnDistanceFromPlayer * Mathf.Sin(angle) + PlayerStats.instance.transform.position.z;
            
            Vector3 pos = new Vector3(x, 0, z);
            SpawnFromPool(_poolTag, pos, Quaternion.identity);
        }
        else {
            spawnTimer += Time.deltaTime;
        }
    }
}
