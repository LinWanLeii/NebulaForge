using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneTest : MonoBehaviour
{
    public Transform joint1;
    public Transform joint2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W)) {
            transform.position += new Vector3 (0, 0 ,1);
        }
        if (Input.GetKey(KeyCode.S)) {
            transform.position += new Vector3 (0, 0 ,-1);
        }
        if (Input.GetKey(KeyCode.A)) {
            transform.position += new Vector3 (0, 1 ,0);
        }
        if (Input.GetKey(KeyCode.D)) {
            transform.position += new Vector3 (0, -1 ,0);
        }
    }
}
