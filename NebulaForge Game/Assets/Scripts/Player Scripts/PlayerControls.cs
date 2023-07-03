using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public static PlayerControls instance;

    private void Awake()
    {
        if (instance == null) {
            instance = this;
        }
        else if (instance != null) {
            Destroy(this);
        }
        DontDestroyOnLoad(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        //DebugingControls();
    }

    void DebugingControls() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            PlayerStats.instance.LoseHealth(10);
        }
    }

    // Handles the 4 direction input for movement
    void Move() {
        if (Input.GetKey(PlayerKeybindsManager.instance.GetKeyForAction(PlayerKeybindsManager.KEYBINDINGS.KB_UP))) {
            transform.position += Vector3.forward * PlayerStats.instance.GetSpd() * Time.deltaTime;
        }
        if (Input.GetKey(PlayerKeybindsManager.instance.GetKeyForAction(PlayerKeybindsManager.KEYBINDINGS.KB_DOWN))) {
            transform.position -= Vector3.forward * PlayerStats.instance.GetSpd() * Time.deltaTime;
        }
        if (Input.GetKey(PlayerKeybindsManager.instance.GetKeyForAction(PlayerKeybindsManager.KEYBINDINGS.KB_LEFT))) {
            transform.position -= Vector3.right * PlayerStats.instance.GetSpd() * Time.deltaTime;
        }
        if (Input.GetKey(PlayerKeybindsManager.instance.GetKeyForAction(PlayerKeybindsManager.KEYBINDINGS.KB_RIGHT))) {
            transform.position += Vector3.right * PlayerStats.instance.GetSpd() * Time.deltaTime;
        }
    }
}
