using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{   
    [Header("Menu")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private GameObject menuUI;
    [SerializeField] private GameObject optionsUI;
    [SerializeField] private GameObject menuCanvas;
    [Header("Options")]
    [SerializeField] private TMP_Dropdown sessionTimerDropdown;
    [SerializeField] private TMP_Dropdown enemySpawnTimeDropdown;
    [SerializeField] private Button apllySettingsButton;
    [SerializeField] private Button backToMenuButton;

    private int sessionTimeMin = 1;
    private int sessionTimeSec = 0;
    private int enemySpawnTime = 7;

    private void Awake()
    {
        playButton.onClick.AddListener(() => {
            Settings();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
        });

        optionsButton.onClick.AddListener(() => {
            optionsUI.SetActive(true);
            menuUI.SetActive(false);
        });

        apllySettingsButton.onClick.AddListener(() => {
            string[] timeString = sessionTimerDropdown.options[sessionTimerDropdown.value].text.Split(':');
            sessionTimeMin = int.Parse(timeString[0]);
            sessionTimeSec = int.Parse(timeString[1]);
            enemySpawnTime = int.Parse(enemySpawnTimeDropdown.options[enemySpawnTimeDropdown.value].text);
            optionsUI.SetActive(false);
            menuUI.SetActive(true);
        });

        backToMenuButton.onClick.AddListener(() => {
            optionsUI.SetActive(false);
            menuUI.SetActive(true);
        });
    }

    private void Settings()
    {
        GameManager.Instance.SetOptions(sessionTimeMin, sessionTimeSec, enemySpawnTime);
    }


}
