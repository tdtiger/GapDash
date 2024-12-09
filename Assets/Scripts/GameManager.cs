using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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

    // 残り時間用のテキスト
    [SerializeField]
    private GameObject timerText;

    // カウントダウン用のテキスト
    [SerializeField]
    private GameObject countText;

    private List<string> s = new List<string>();

    // ゲームオーバー用のUI
    [SerializeField]
    private GameObject gameOverUI;

    // 結果表示用のテキスト
    [SerializeField]
    private GameObject resultText;

    // 天井(壁)
    [SerializeField]
    private Wall wall;

    // タイトルボタン
    [SerializeField]
    private Button titleButton;

    private Coroutine cr;

    // Start is called before the first frame update
    void Start()
    {
        // いろいろ準備
        Time.timeScale = 1;
        titleButton.onClick.AddListener(MoveToTitle);
        s.Add("5");
        s.Add("4");
        s.Add("3");
        s.Add("2");
        s.Add("1");
        s.Add("START!!");

        // 残り時間を初期化し，結果表示画面を非アクティブ化
        timeRemaining = timeLimit;
        gameOverUI.SetActive(false);
        cr = StartCoroutine(CountDown());
    }

    // Update is called once per frame
    void Update()
    {
        // 動作中ならば，残り時間を更新
        if(wall.IsMoving == true){
            UpdateTimer();
        }

        // 残り時間0でクリア
        if (timeRemaining <= 0){
            GameClear();
        }
    }

    private IEnumerator CountDown(){
        // 1秒ごとにカウントダウン
        for(int i = 0;i < 6; ++i){
            countText.GetComponent<TextMeshProUGUI>().text = s[i];
            yield return new WaitForSeconds(1f);
        }
        countText.SetActive(false);

        // 凸部分を生成し，動作開始
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
        // 結果表示画面をアクティブにし，"GAME CLEAR!"と表示，時間停止
        gameOverUI.SetActive(true);
        resultText.SetActive(true);
        resultText.GetComponent<TextMeshProUGUI>().text = "GAME CLEAR!";
        Time.timeScale = 0;
        StopCoroutine(cr);
    }

    public void GameOver(){
        // 結果表示画面をアクティブにし，"GAME OVER..."と表示，時間停止
        gameOverUI.SetActive(true);
        resultText.GetComponent<TextMeshProUGUI>().text = "GAME OVER...";
        Time.timeScale = 0;
        StopCoroutine(cr);
    }

    private void MoveToTitle(){
        // タイトル画面に遷移
        SceneManager.LoadScene("TitleScene");
    }
}