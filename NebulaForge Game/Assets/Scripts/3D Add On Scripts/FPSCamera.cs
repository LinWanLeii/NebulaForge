using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCamera : MonoBehaviour
{
    public static FPSCamera instance;

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

    public bool isFPS = false;
    public float mouseSense = 100.0f;
    public Transform playerBody;
    float xRotation = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        isFPS = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isFPS) { return; }
        
        float mouseX = Input.GetAxis("Mouse X") * mouseSense * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSense * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90.0f, 90.0f);

        transform.localRotation = Quaternion.Euler(xRotation, 0.0f, 0.0f);
        playerBody.Rotate(Vector3.up * mouseX);

    }
}
