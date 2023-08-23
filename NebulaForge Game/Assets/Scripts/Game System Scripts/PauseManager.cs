using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public static PauseManager instance;

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

    public bool isPaused = false;

    public GameObject pauseMenuUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (LevelManager.instance.currLevel == -1) { return; }

        if (Input.GetKeyDown(PlayerKeybindsManager.instance.GetKeyForAction(PlayerKeybindsManager.KEYBINDINGS.KB_PAUSE))) {
            if (isPaused) {
                Resume();
            } else {
                Pause();
            }
        }        
    }

    public void Resume() {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1.0f;
        isPaused = false;
        if (FPSCamera.instance.isFPS) {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void Pause() {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0.0f;
        isPaused = true;
        if (FPSCamera.instance.isFPS) {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void LoadMainMenu() {
        SceneManager.LoadScene(0);
        Time.timeScale = 1.0f;
        isPaused = false;
    }
}
