using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private int currLevel;
    [SerializeField]
    private int numKills;

    // Start is called before the first frame update
    void Start()
    {
        currLevel = 0;
        numKills = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
