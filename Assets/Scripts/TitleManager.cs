using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    // 遷移のためのボタン群
    [SerializeField]
    private Button startButton;

    [SerializeField]
    private Button helpButton;

    [SerializeField]
    private Button exitButton;

    // チュートリアル(遊び方)時の画像群
    [SerializeField]
    private List<GameObject> slides;

    // 現時点でのページ
    private int page = 0;

    // Start is called before the first frame update
    void Start()
    {
        startButton.onClick.AddListener(StartGame);
        helpButton.onClick.AddListener(ShowTutorial);
        exitButton.onClick.AddListener(ExitGame);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void StartGame(){
        // ゲーム開始
        SceneManager.LoadScene("PlayScene");
    }

    private void ShowTutorial(){
        // チュートリアル開始
        Debug.Log("Tutorial");
    }

    private void ExitGame(){
        // ゲーム終了
        Application.Quit();
        Debug.Log("Game Exit");
    }
}
