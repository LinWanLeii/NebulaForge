using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;
    public Vector3 offSet;
    public Quaternion rotationSet;

    private void Update()
    {
        transform.position = player.position + offSet;
        transform.rotation = rotationSet;
    }
}
