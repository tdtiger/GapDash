using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    private float gameDuration = 60.0f;
    public float GameDuration {
        get {
            return gameDuration;
        }
        set {
            gameDuration = Mathf.Max(0, value);
        }
    }

    private float timeRemaining;
    public GameObject timerText;
    public GameObject gameOverUI;
    public GameObject resultText;

    // Start is called before the first frame update
    void Start()
    {
        timeRemaining = gameDuration;
        gameOverUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        timeRemaining -= Time.deltaTime;
        timerText.GetComponent<TextMeshProUGUI>().text = "Time : " + Mathf.CeilToInt(timeRemaining).ToString();

        if (timeRemaining <= 0){
            GameClear();
        }
    }

    public void GameOver(){
        gameOverUI.SetActive(true);
        Time.timeScale = 0;
    }

    private void GameClear(){
        gameOverUI.SetActive(true);
        resultText.SetActive(true);
        resultText.GetComponent<TextMeshProUGUI>().text = "You Win!";
        Time.timeScale = 0;
    }

}
