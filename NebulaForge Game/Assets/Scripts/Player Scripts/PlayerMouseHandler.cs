using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouseHandler : MonoBehaviour
{
    public static PlayerMouseHandler instance;

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

    [SerializeField]
    private Vector3 dir;
    [SerializeField]
    private float distanceFromPlayer;

    public Vector3 GetMousePos() { return transform.position; }
    public Vector3 GetMouseDir() { return dir; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (FPSCamera.instance.isFPS) {
            dir = Camera.main.transform.forward.normalized;
        } else {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            int layerMask = 1 << 8; // Cast rays only against colliders in layer 8 aka ground

            if (Physics.Raycast(ray, out RaycastHit raycastHit, 1000.0f, layerMask)) {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * raycastHit.distance, Color.yellow);
                transform.position = raycastHit.point;
            }

            
            dir = transform.position - PlayerControls.instance.gameObject.transform.position;
            dir = new Vector3(dir.x, 0, dir.z).normalized;
            transform.position = dir * distanceFromPlayer + PlayerControls.instance.gameObject.transform.position;
        }
    }
}
