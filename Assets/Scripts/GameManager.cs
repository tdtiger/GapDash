using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    // 制限時間
    private float timeLimit = 60.0f;
    public float TimeLimit {
        get {
            return timeLimit;
        }
        set {
            timeLimit = Mathf.Max(0, value);
        }
    }

    // 残り時間
    private float timeRemaining;

    [SerializeField]
    private GameObject timerText;
    [SerializeField]
    private GameObject countText;

    [SerializeField]
    private GameObject gameOverUI;

    [SerializeField]
    private GameObject resultText;

    [SerializeField]
    private Wall wall;

    // Start is called before the first frame update
    void Start()
    {
        // 残り時間を初期化し，結果表示画面を非アクティブ化
        timeRemaining = timeLimit;
        gameOverUI.SetActive(false);
        Coroutine cr = StartCoroutine(CountDown());
    }

    // Update is called once per frame
    void Update()
    {
        if(wall.IsMoving == true){
            UpdateTimer();
        }

        // 残り時間0でクリア
        if (timeRemaining <= 0){
            GameClear();
        }
    }

    private IEnumerator CountDown(){
        countText.GetComponent<TextMeshProUGUI>().text = "5";
        yield return new WaitForSeconds(1f);
        countText.GetComponent<TextMeshProUGUI>().text = "4";
        yield return new WaitForSeconds(1f);
        countText.GetComponent<TextMeshProUGUI>().text = "3";
        yield return new WaitForSeconds(1f);
        countText.GetComponent<TextMeshProUGUI>().text = "2";
        yield return new WaitForSeconds(1f);
        countText.GetComponent<TextMeshProUGUI>().text = "1";
        yield return new WaitForSeconds(1f);
        countText.GetComponent<TextMeshProUGUI>().text = "START!!";
        yield return new WaitForSeconds(0.5f);
        countText.SetActive(false);

        wall.SpawnProtrusion();
        wall.IsMoving = true;
        yield return null;
    }

    private void UpdateTimer(){
        // 時間を進め，タイマーを更新
        timeRemaining -= Time.deltaTime;
        timerText.GetComponent<TextMeshProUGUI>().text = "Time : " + Mathf.CeilToInt(timeRemaining).ToString();
    }
    private void GameClear(){
        // 結果表示画面をアクティブにし，"GAME CLEAR!"と表示
        gameOverUI.SetActive(true);
        resultText.SetActive(true);
        resultText.GetComponent<TextMeshProUGUI>().text = "GAME CLEAR!";
        Time.timeScale = 0;
    }

    public void GameOver(){
        // 結果表示画面をアクティブにし，"GAME OVER..."と表示
        gameOverUI.SetActive(true);
        resultText.GetComponent<TextMeshProUGUI>().text = "GAME OVER...";
        Time.timeScale = 0;
    }
}