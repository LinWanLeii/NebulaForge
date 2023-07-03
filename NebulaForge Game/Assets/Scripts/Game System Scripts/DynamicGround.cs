using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicGround : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    [SerializeField]
    private float widthOfGround = 80.0f; // Width of each grid

    // These two are fixed based on a 3x3 grid of grounds
    // Draw a diagram out to visual easier
    private const float ratioMax = 1.75f;
    private const float ratioMoveDist = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Check and move in the x direction if player is out of range
        if (Mathf.Abs(player.position.x - transform.position.x) > widthOfGround * ratioMax) {
            if (player.position.x > transform.position.x) {
                transform.position += new Vector3(widthOfGround * ratioMoveDist, 0, 0);
            }
            else {
                transform.position -= new Vector3(widthOfGround * ratioMoveDist, 0, 0);
            }
        }
        if (Mathf.Abs(player.position.z - transform.position.z) > widthOfGround * ratioMax) {
            if (player.position.z > transform.position.z) {
                transform.position += new Vector3(0, 0, widthOfGround * ratioMoveDist);
            }
            else {
                transform.position -= new Vector3(0, 0, widthOfGround * ratioMoveDist);
            }
        }
    }
}
