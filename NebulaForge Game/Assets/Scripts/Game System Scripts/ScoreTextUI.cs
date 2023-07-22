using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreTextUI : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public bool stop;
    public int timeTaken;

    // Start is called before the first frame update
    void Start()
    {
        stop = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!stop) {
            stop = true;
            timeTaken = (int)LevelManager.instance.timeTaken;
        } else {
            scoreText.text = "Upgrades: " + (PlayerStats.instance.GetplayerLv() - 1) + ", Time taken: " + timeTaken;
        }
    }
}
