using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

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

    public int currLevel;
    public int numKills;
    public float timeTaken;
    public GameObject defeatUI;
    public GameObject victoryUI;
    public AudioSource gameOverSound;

    // Start is called before the first frame update
    void Start()
    {
        currLevel = 0;
        numKills = 0;
        timeTaken = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timeTaken += Time.deltaTime;   
    }

    public void Victory() {
        Cursor.lockState = CursorLockMode.None;
        victoryUI.SetActive(true);
        Time.timeScale = 0.0f;
        gameOverSound.Play();
    }
    public void Defeat() {
        Cursor.lockState = CursorLockMode.None;
        defeatUI.SetActive(true);
        Time.timeScale = 0.0f;
        gameOverSound.Play();
    }
}
