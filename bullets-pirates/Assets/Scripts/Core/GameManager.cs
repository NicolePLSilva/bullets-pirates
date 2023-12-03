using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{   
    public static GameManager Instance { get; private set; }
    

    public int sessionTimerMin = 1;
    public int sessionTimerSec = 0;
    public int enemySpawnTimer = 7;

    private void Awake()
    {
        Instance = this;
    }
    
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    public void SetOptions(int sessionTimeMin, int sessionTimeSec, int enemySpawnTime)
    {
        sessionTimerMin = sessionTimeMin;
        sessionTimerSec = sessionTimeSec;
        enemySpawnTimer = enemySpawnTime;
    }
}
