using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneVisualizer : MonoBehaviour
{
    
    public Transform parentTransform;
    public Transform childTransform;
    public Vector3 parentPos;
    public Vector3 childPos;
    public Vector3 direction;
    public float scale;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        parentPos = parentTransform.position;
        childPos = childTransform.position;
        direction = (parentPos - childPos).normalized;

        transform.position = (parentPos + childPos) * 0.5f;
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, Vector3.Distance(parentPos, childPos));
        transform.rotation = Quaternion.LookRotation(direction, transform.up);
    }
}
