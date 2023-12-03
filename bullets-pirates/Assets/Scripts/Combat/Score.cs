using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] private GameObject scoreUI;
    [SerializeField] private TMP_Text scoreText;
    public int scorePoints;

    void Update()
    {
        scoreText.text = "Score: " + scorePoints.ToString();
    }
}
