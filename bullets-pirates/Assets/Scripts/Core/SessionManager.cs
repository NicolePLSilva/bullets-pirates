using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SessionManager : MonoBehaviour
{   
    public static SessionManager Instance { get; private set; }

    [Header("Enemy")]
    [SerializeField] public GameObject[] enemyPrefab;
    [SerializeField] public int enemyAmount;
    [SerializeField] public Transform[] spawnPoints;

    [SerializeField] private float enemySpawnTimer = 7f;

    [Header("Session")]
    [SerializeField] private float sessionTimerMin = 1f;
    [SerializeField] private float sessionTimerSec = 0f;
    [SerializeField] private TMP_Text sessionTimerText;

    [Header("Canvas")]
    [SerializeField] private GameObject sessionCanvasUI;
    [SerializeField] private Button playAgainButton;
    [SerializeField] private Button mainMenuButton;

    private float sessionTimer;
    private float initSessionTimerMin;
    private float initSessionTimerSec;
    private float initSpawnTimer = 7f;

    private ObjectPool enemyPool;

    private void Awake()
    {
        Instance = this;
        enemyPool = GetComponentInChildren<ObjectPool>();
        enemyPool.PopulatePool(enemyPrefab, this.transform, enemyAmount);

        playAgainButton.onClick.AddListener(() => {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        });

        mainMenuButton.onClick.AddListener(() => {
            SceneManager.LoadScene(0);
        });
        
    }
    private void OnEnable()
    {
        Time.timeScale = 1f;
        if (GameManager.Instance == null) { return; }
        sessionTimerMin = (float)GameManager.Instance.sessionTimerMin;
        sessionTimerSec = (float)GameManager.Instance.sessionTimerSec;
        initSpawnTimer = (float)GameManager.Instance.enemySpawnTimer;
    }

    private void Start()
    {
        enemySpawnTimer = 1f;
    }

    void Update()
    {
        TimerCooldown();
        SpawnEnemy();
    }

    private void TimerCooldown()
    {
        if (sessionTimerMin >= 0)
        {
            if (sessionTimerSec >= 0)
            { 
                sessionTimerSec -= Time.deltaTime;
            }
            else
            {
                sessionTimerSec = 59;
                sessionTimerMin--;
            }
        }
        else
        {   
            SessionOver();
        }

        sessionTimerText.text = string.Format("{0}:{1}", sessionTimerMin.ToString("00"),
                                                        sessionTimerSec.ToString("00"));
    }

    public void SessionOver()
    {
        sessionTimerMin = 0f;
        sessionTimerSec = 0f;
        Time.timeScale = 0f;
        sessionCanvasUI.SetActive(true);
    }

    private void SpawnEnemy()
    {
        enemySpawnTimer -= Time.deltaTime;

        if (enemySpawnTimer <= 0)
        {
            enemyPool.RequestObjectInPool(spawnPoints[Random.Range(0, spawnPoints.Length)]);
            enemySpawnTimer = initSpawnTimer;
        }
    }

}
